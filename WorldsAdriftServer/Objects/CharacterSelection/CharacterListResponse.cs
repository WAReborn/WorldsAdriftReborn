using Newtonsoft.Json;
using WorldsAdriftServer.Helper.Data;
using WorldsAdriftServer.Objects.DataObjects;

namespace WorldsAdriftServer.Objects.CharacterSelection
{
    internal class CharacterListResponse
    {
        public CharacterListResponse() { }
        internal CharacterListResponse( PlayerData playerData )
        {
            foreach (string chracterGuid in playerData.CharacterGUIDs)
            {
                CharacterData characterData = DataStore.Instance.CharacterDataDictionary[chracterGuid];
                CharacterList.Add(characterData.characterCreationData);

                if (characterData.Name == playerData.Name)
                { HasMainCharacter = true; }
            }

            UnlockedSlots = playerData.CharacterGUIDs.Count;
            HavenFinished = playerData.HavenFinished;

            if (UnlockedSlots < 6)
            { UnlockedSlots++; }
        }

        [JsonProperty("characterList")]
        public List<CharacterCreationData> CharacterList { get; set; } = new List<CharacterCreationData>();

        [JsonProperty("unlockedSlots")]
        public int UnlockedSlots { get; set; } = 0;

        [JsonProperty("hasMainCharacter")]
        public bool HasMainCharacter { get; set; } = false;

        [JsonProperty("havenFinished")]
        public bool HavenFinished { get; set; } = false;
    }
}
