using System.Text.Json;
using WorldsAdriftServer.Objects.DataObjects;

namespace WorldsAdriftServer.Helper.Data
{
    [Serializable]
    internal class DataStore
    {
        public Dictionary<string, NameData> PlayerCharacterNameData { get; set; } = new Dictionary<string, NameData>();
        public Dictionary<string, PlayerData> PlayerDataDictionary { get; set; } = new Dictionary<string, PlayerData>();
        public Dictionary<string, CharacterData> CharacterDataDictionary { get; set; } = new Dictionary<string, CharacterData>();
        public Dictionary<string, AllianceData> AllianceDataDictionary { get; set; } = new Dictionary<string, AllianceData>();
        public Dictionary<string, CrewData> CrewDataDictionary { get; set; } = new Dictionary<string, CrewData>();
        public Dictionary<string, InviteData> InviteDataDictionary { get; set; } = new Dictionary<string, InviteData>();

        public static DataStore Instance { get; set; } = new DataStore();
        internal static DataStore ReadData()
        {
            string path = "./data.json";
            string json = string.Empty;
            DataStore? dataStore = new DataStore();

            if (File.Exists(path))
            {
                json = File.ReadAllText(path);
            }
            if (json != null && json != string.Empty)
            {
                dataStore = JsonSerializer.Deserialize<DataStore>(json);
            }

            dataStore ??= new();
            return dataStore;
        }
        internal static void WriteData( DataStore dataStore )
        {
            if (dataStore != null)
            {
                JsonSerializerOptions options = new JsonSerializerOptions();
                options.WriteIndented = true;
                string json = JsonSerializer.Serialize(dataStore, options);
                File.WriteAllText("./data.json", json);
            }
        }
    }
}
