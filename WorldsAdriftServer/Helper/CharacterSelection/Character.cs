using WorldsAdriftServer.Objects.CharacterSelection;

namespace WorldsAdriftServer.Helper.CharacterSelection
{
    internal static class Character
    {
        internal static CharacterCreationData GenerateRandomCharacter(string serverIdentifier, string characterName)
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

            //Guid.NewGuid().ToString() Is a temp fix will be fixed when adding charcter saving
            return new CharacterCreationData(1, Guid.NewGuid().ToString(), characterName, "serverName?", serverIdentifier, cosmetics, colors, true, false, false);
        }
        /*
         * generates a character without cosmetics which reflects as an empty slot in the character select screen.
         * characterName is not applied, instead the SteamAuthRequestToken.screenName is used.
         */
        internal static CharacterCreationData GenerateNewCharacter(string serverIdentifier, string characterName)
        {
            Dictionary<CharacterSlotType, ItemData> cosmetics = new Dictionary<CharacterSlotType, ItemData>();
            CharacterUniversalColors colors = new CharacterUniversalColors();

            Random r = new Random();
            int num = r.Next(0, CustomisationSettings.skinColors.Length);

            colors.SkinColor = CustomisationSettings.skinColors[num];
            colors.LipColor = CustomisationSettings.lipColors[num];
            colors.HairColor = CustomisationSettings.hairColors[r.Next(0, CustomisationSettings.hairColors.Length)];

            return new CharacterCreationData(1, "UID", characterName, "serverName?", serverIdentifier, null, colors, true, false, false);
        }
    }
}
