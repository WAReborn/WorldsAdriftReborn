namespace WorldsAdriftServer.Objects.CharacterSelection
{
    internal class CharacterListResponse
    {
        public List<CharacterCreationData> characterList { get; set; }
        public int unlockedSlots { get; set; }
        public bool hasMainCharacter { get; set; }
        public bool havenFinished { get; set; }

        public CharacterListResponse(List<CharacterCreationData> characterList )
        {
            this.characterList = characterList;
        }
    }
}
