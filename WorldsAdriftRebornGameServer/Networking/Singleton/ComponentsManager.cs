using System.Runtime.InteropServices;
using Improbable.Worker;
using Improbable.Worker.Internal;

namespace WorldsAdriftRebornGameServer.Networking.Singleton
{
    internal class ComponentsManager
    {
        private static ComponentsManager instance = null;
        public ComponentProtocol.ClientComponentVtable[] ClientComponentVtables { get; private set; }
        private ComponentsManager()
        {
            ClientComponentVtables = CreateComponentVtableArray();
        }
        public static ComponentsManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ComponentsManager();
                }
                return instance;
            }
        }
        private ComponentProtocol.ClientComponentVtable[] CreateComponentVtableArray()
        {
            ComponentProtocol.ClientComponentVtable[] array = new ComponentProtocol.ClientComponentVtable[ComponentDatabase.MetaclassMap.Count];
            int num = 0;
            foreach (KeyValuePair<uint, IComponentMetaclass> keyValuePair in ComponentDatabase.MetaclassMap)
            {
                array[num++] = keyValuePair.Value.Vtable;
            }
            return array;
        }
        public ComponentProtocol.ClientSerialize GetSerializerForComponent(uint componentId )
        {
            for(int i = 0; i < ClientComponentVtables.Length; i++)
            {
                if (ClientComponentVtables[i].ComponentId == componentId)
                {
                    return Marshal.GetDelegateForFunctionPointer<ComponentProtocol.ClientSerialize>(ClientComponentVtables[i].Serialize);
                }
            }

            Console.WriteLine("[error] failed to find the correct serializer for component id " + componentId + ", returning the first in the list.");
            Console.WriteLine("[error] this is not what you wanted, following code will most likely fail.");
            return Marshal.GetDelegateForFunctionPointer<ComponentProtocol.ClientSerialize>(ClientComponentVtables[0].Serialize);
        }
    }
}
