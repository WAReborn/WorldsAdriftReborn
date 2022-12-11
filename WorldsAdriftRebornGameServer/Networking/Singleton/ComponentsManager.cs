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
    }
}
