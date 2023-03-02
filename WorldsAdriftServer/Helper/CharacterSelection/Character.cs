using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WorldsAdriftServer.Helper.Data;
using WorldsAdriftServer.Objects.CharacterSelection;

namespace WorldsAdriftServer.Helper.CharacterSelection
{
    internal static class Character
    {
        internal static CharacterCreationData GenerateRandomCharacter(int id, string serverIdentifier, string characterName)
        {
            Dictionary<CharacterSlotType, ItemData> cosmetics = new Dictionary<CharacterSlotType, ItemData>();
            CharacterUniversalColors colors = new CharacterUniversalColors();

            Random r = new Random();
            int num = r.Next(0, CustomisationSettings.skinColors.Length);

            colors.SkinColor = CustomisationSettings.skinColors[num];
            colors.LipColor = CustomisationSettings.lipColors[num];
            colors.HairColor = CustomisationSettings.hairColors[r.Next(0, CustomisationSettings.hairColors.Length)];

            cosmetics.Add(CharacterSlotType.Head, new ItemData(
                                                                "1",
                                                                CustomisationSettings.starterHeadItems.Keys.ToList<string>()[r.Next(0, CustomisationSettings.starterHeadItems.Keys.Count)],
                                                                new ColorProperties(
                                                                                    CustomisationSettings.clothingColors[r.Next(0, 7)],
                                                                                    CustomisationSettings.clothingColors[r.Next(0, 7)]),
                                                                100f));
            cosmetics.Add(CharacterSlotType.Body, new ItemData(
                                                                "2",
                                                                CustomisationSettings.starterTorsoItems.Keys.ToList<string>()[r.Next(0, CustomisationSettings.starterTorsoItems.Keys.Count)],
                                                                new ColorProperties(
                                                                                    CustomisationSettings.clothingColors[r.Next(0, 7)],
                                                                                    CustomisationSettings.clothingColors[r.Next(0, 7)]),
                                                                100f));
            cosmetics.Add(CharacterSlotType.Feet, new ItemData(
                                                                "3",
                                                                CustomisationSettings.starterLegItems.Keys.ToList<string>()[r.Next(0, CustomisationSettings.starterLegItems.Keys.Count)],
                                                                new ColorProperties(
                                                                                    CustomisationSettings.clothingColors[r.Next(0, 7)],
                                                                                    CustomisationSettings.clothingColors[r.Next(0, 7)]),
                                                                100f));
            cosmetics.Add(CharacterSlotType.Face, new ItemData(
                                                                "4",
                                                                CustomisationSettings.starterFaceItems.Keys.ToList<string>()[r.Next(0, CustomisationSettings.starterFaceItems.Keys.Count)],
                                                                default(ColorProperties),
                                                                100f));
            cosmetics.Add(CharacterSlotType.FacialHair, new ItemData(
                                                                "5",
                                                                CustomisationSettings.starterFacialHairItems.Keys.ToList<string>()[r.Next(0, CustomisationSettings.starterFacialHairItems.Keys.Count)],
                                                                default(ColorProperties),
                                                                100f));

            return new CharacterCreationData(id, Guid.NewGuid().ToString(), characterName, "awesome community server", serverIdentifier, cosmetics, colors, true, false, false);
        }
        /*
         * generates a character without cosmetics which reflects as an empty slot in the character select screen.
         * characterName is not applied, instead the SteamAuthRequestToken.screenName is used.
         */
        internal static CharacterCreationData GenerateNewCharacter(int id, string serverIdentifier, string characterName)
        {
            Dictionary<CharacterSlotType, ItemData> cosmetics = new Dictionary<CharacterSlotType, ItemData>();
            CharacterUniversalColors colors = new CharacterUniversalColors();

            Random r = new Random();
            int num = r.Next(0, CustomisationSettings.skinColors.Length);

            colors.SkinColor = CustomisationSettings.skinColors[num];
            colors.LipColor = CustomisationSettings.lipColors[num];
            colors.HairColor = CustomisationSettings.hairColors[r.Next(0, CustomisationSettings.hairColors.Length)];

            return new CharacterCreationData(id, Guid.NewGuid().ToString(), characterName, "awesome community server", serverIdentifier, null, colors, true, false, false);
        }

        internal static List<CharacterCreationData> GetCharacterList( GameContext dbContext, string serverIdentifier )
        {
            var charactersList = dbContext.Characters.ToListAsync().GetAwaiter().GetResult();

            List<CharacterCreationData> characters = new List<CharacterCreationData>();
            if (charactersList.Count > 0)
            {
                foreach (var character in charactersList.Select(( value, index ) => new { value, index }))
                {
                    characters.Add(new CharacterCreationData(character.index + 1, character.value.Id.ToString(), character.value.Name, character.value.Server, character.value.serverIdentifier, JsonConvert.DeserializeObject<Dictionary<CharacterSlotType, ItemData>>(character.value.Cosmetics), JsonConvert.DeserializeObject<CharacterUniversalColors>(character.value.UniversalColors), character.value.IsMale, character.value.SeenIntro, character.value.SkippedTutorial));
                }
            }
            characters.Add(Character.GenerateNewCharacter(characters.Count + 1, serverIdentifier, "Billy Jones")); //Add GenerateNewCharacter() to end of list
            return characters;
        }
    }
}
