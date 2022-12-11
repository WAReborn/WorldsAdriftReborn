using WorldsAdriftServer.Helper.CharacterSelection;
using WorldsAdriftServer.Objects.CharacterSelection;

namespace WorldsAdriftServer.Objects.DataObjects
{
    [Serializable]
    internal class CharacterData : Data
    {
        public CharacterData() { }
        internal CharacterData( string name )
        {
            Name = name;
            characterCreationData.characterUid = Guid;
            characterCreationData.Name = Name;
        }
        public CharacterCreationData characterCreationData { get; set; } = Character.GenerateNewCharacter();
        public string CrewGuid { get; set; } = string.Empty;
        public string AllianceGuid { get; set; } = string.Empty;
        public string AllianceRankGuid { get; set; } = string.Empty;
        public string AlliancePublicNotes { get; set; } = string.Empty;
        public string AlliancePrivateNotes { get; set; } = string.Empty;
    }
}
