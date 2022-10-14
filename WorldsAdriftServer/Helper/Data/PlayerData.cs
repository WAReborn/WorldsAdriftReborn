using System;
using WorldsAdriftServer.Objects.CharacterSelection;

namespace WorldsAdriftServer.Helper.Data
{
    internal class PlayerData
    {
        //key is GUID of the charater
        public CharacterListResponse CharacterListResponse { get; set; }
        public PlayerData( string token )
        {
            CharacterListResponse = new CharacterListResponse(new List<CharacterCreationData>());
            Token = token;
        }

        //Some sort of security needs to built
        public string Token { get; }

    }
}
