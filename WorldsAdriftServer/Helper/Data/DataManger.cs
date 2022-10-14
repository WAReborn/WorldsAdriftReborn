using System.Text.Json;

namespace WorldsAdriftServer.Helper.Data
{
    internal class DataManger
    {

        public static DataStore GlobleDataStore { get; set; } = ReadData();
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
                dataStore = JsonSerializer.Deserialize(json, typeof(DataStore)) as DataStore;
            }

            dataStore ??= new();
            return dataStore;
        }
        internal static void WriteData(DataStore dataStore)
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
    [Serializable]
    internal class DataStore
    {
        public Dictionary<string, PlayerData> PlayerDataDictionary { get; set; } = new Dictionary<string, PlayerData>();
    }
}
