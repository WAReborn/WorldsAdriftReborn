using WorldsAdriftServer.Helpers;

namespace WorldsAdriftServer.Objects.DataObjects
{
    [Serializable]
    internal class PlayerData : Data
    {
        public PlayerData() { }
        internal PlayerData( string name ) => Name = name;
        public List<string> CharacterGUIDs { get; set; } = new List<string>();
        public bool HavenFinished { get; set; } = false;
        public bool HasMainCharacter()
        {
            foreach(string character in CharacterGUIDs)
            {
                CharacterData characterData = DataStore.Instance.CharacterDataDictionary[character];
                if(characterData.Name == Name)
                { return true; }
            }
            return false;
        }
    }
}
