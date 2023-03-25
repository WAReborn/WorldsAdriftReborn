using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Improbable.Entity.Component;
using Improbable.Worker;
using Improbable.Worker.Internal;
using WorldsAdriftRebornGameServer.DLLCommunication;
using WorldsAdriftRebornGameServer.Game.Components.Update.Handlers;
using WorldsAdriftRebornGameServer.Networking.Singleton;
using Bossa.Travellers.Inventory;
using Bossa.Travellers.Craftingstation;

namespace WorldsAdriftRebornGameServer.Game.Components.Update
{
    internal class ComponentUpdateManager
    {
        private static ComponentUpdateManager instance { get; set; }
        public static ComponentUpdateManager Instance
        {
            get
            {
                return instance ?? (instance = new ComponentUpdateManager());
            }
        }
        private static class HashCache<T>
        {
            public static bool Initialized;
            public static ulong Id;
        }

        protected delegate void RegisterDelegate(ENetPeerHandle player, long entityId, object clientComponentUpdate, object serverComponentData);
        private readonly Dictionary<ulong, RegisterDelegate> _handlers = new Dictionary<ulong, RegisterDelegate>();

        //FNV-1 64 bit hash
        protected virtual ulong GetHash<T>()
        {
            if (HashCache<T>.Initialized)
            {
                return HashCache<T>.Id;
            }

            ulong hash = 14695981039346656037UL; //offset
            string typeName = typeof(T).FullName;
            for (int i = 0; i < typeName.Length; i++)
            {
                hash ^= typeName[i];
                hash *= 1099511628211UL; //prime
            }
            HashCache<T>.Initialized = true;
            HashCache<T>.Id = hash;
            return hash;
        }
        private ComponentUpdateManager()
        {
            foreach(Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                RegisterAllComponentUpdateHandlers(assembly);
            }
        }
        private static bool IsSubclassOfRawGeneric( Type generic, Type toCheck )
        {
            while (toCheck != null && toCheck != typeof(object))
            {
                Type cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == cur)
                {
                    return true;
                }
                toCheck = toCheck.BaseType;
            }
            return false;
        }
        private void RegisterAllComponentUpdateHandlers(Assembly assembly)
        {
            IEnumerable<Type> definedHandlers = assembly.GetTypes()
                .Where(t => t.GetCustomAttributes(typeof(RegisterComponentUpdateHandler), true).Length > 0);

            MethodInfo registerMethod = this.GetType().GetMethods()
                .Where(m => m.Name == nameof(RegisterComponentUpdateHandler))
                .Where(m => m.IsGenericMethod)
                .FirstOrDefault();

            foreach(Type type in definedHandlers)
            {
                if(IsSubclassOfRawGeneric(typeof(IComponentUpdateHandler<,>), type))
                {
                    Type type_clientComponentUpdate = type.BaseType.GetGenericArguments()[0];
                    Type type_serverComponentData = type.BaseType.GetGenericArguments()[1];

                    Console.WriteLine("[info] trying to register ComponentUpdate handler for types " + type_clientComponentUpdate + " " + type_serverComponentData);

                    // dynamically create instance of handler
                    Type handlerMethodArgTypes = typeof(Action<,,,>).MakeGenericType(typeof(ENetPeerHandle), typeof(long), type_clientComponentUpdate, type_serverComponentData);
                    object handler = Activator.CreateInstance(type);
                    Delegate handlerMethod = Delegate.CreateDelegate(handlerMethodArgTypes, handler, type.GetMethod("HandleUpdate", new Type[] { typeof(ENetPeerHandle), typeof(long), type_clientComponentUpdate, type_serverComponentData }));

                    // register created handler
                    MethodInfo genericRegisterComponent = registerMethod.MakeGenericMethod(type_clientComponentUpdate, type_serverComponentData);
                    genericRegisterComponent.Invoke(this, new object[] { handlerMethod });

                    Console.WriteLine("[success] registered ComponentUpdate handler for types " + type_clientComponentUpdate + " " + type_serverComponentData);
                }
            }
        }
        public void RegisterComponentUpdateHandler<TClient, TServer>(Action<ENetPeerHandle, long, TClient, TServer> onProcess)
        {
            ulong hash = GetHash<TServer>();
            if (!_handlers.ContainsKey(hash))
            {
                _handlers.Add(hash, null);
            }

            _handlers[hash] = ( ENetPeerHandle player, long entityId, object clientComponentUpdate, object serverComponentData ) =>
            {
                onProcess(player, entityId, (TClient)clientComponentUpdate, (TServer)serverComponentData);
            };
        }

        public unsafe bool HandleComponentUpdate(ENetPeerHandle player, long entityId, uint componentId, byte* componentData, int componentDataLength)
        {
            bool success = false;
            Console.WriteLine("[info] trying to handle a ComponentUpdateOp for " + componentId);

            for (int i = 0; i < ComponentsManager.Instance.ClientComponentVtables.Length; i++)
            {
                if (ComponentsManager.Instance.ClientComponentVtables[i].ComponentId == componentId)
                {
                    if (GameState.Instance.ComponentMap.ContainsKey(player) && GameState.Instance.ComponentMap[player].ContainsKey(entityId) && GameState.Instance.ComponentMap[player][entityId].ContainsKey(componentId))
                    {

                        ComponentProtocol.ClientObject* wrapper = ClientObjects.ObjectAlloc();
                        ComponentProtocol.ClientDeserialize deserialize = Marshal.GetDelegateForFunctionPointer<ComponentProtocol.ClientDeserialize>(ComponentsManager.Instance.ClientComponentVtables[i].Deserialize);

                        if (deserialize(componentId, 1, componentData, (uint)componentDataLength, &wrapper))
                        {
                            // now we got a reference to the deserialized component, we can use it to update the component that we already have for the player.
                            object storedComponent = ClientObjects.Instance.Dereference(GameState.Instance.ComponentMap[player][entityId][componentId]);
                            object newComponent = ClientObjects.Instance.Dereference(wrapper->Reference);

                            // todo: remove this increasing if else with something cleaner
                            ulong hash = 0;
                            if(componentId == 1003)
                            {
                                hash = GetHash<PlayerCraftingInteractionState.Data>();
                            }
                            else
                            {
                                hash = GetHash<InventoryModificationState.Data>();
                            }

                            if(_handlers.TryGetValue(hash, out RegisterDelegate handler))
                            {
                                handler(player, entityId, newComponent, storedComponent);
                                success = true;
                            }

                            if (!success)
                            {
                                Console.WriteLine("[warning] could not find a handler for component update on " + componentId);
                            }

                            ClientObjects.Instance.DestroyReference(wrapper->Reference);
                        }
                        else
                        {
                            Console.WriteLine("[error] failed to deserialize ComponentUpdateOp data for id " + componentId);
                        }

                        ClientObjects.ObjectFree(componentId, 1, wrapper);

                    }
                    else
                    {
                        Console.WriteLine("[warning] could not match requested ComponentUpdate with local stored values.");
                    }

                    break;
                }
            }

            if (!success)
            {
                Console.WriteLine("[error] if no other error above, no matching component for id " + componentId + " defined in the game.");
            }
            return success;
        }
    }
}
