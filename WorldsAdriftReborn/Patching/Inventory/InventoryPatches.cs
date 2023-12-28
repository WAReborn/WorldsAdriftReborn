﻿using Bossa.Travellers.Visualisers;
using Bossa.Travellers.Visualisers.Profile;
using HarmonyLib;
using Improbable.Collections;
using Newtonsoft.Json;
using Travellers.UI.Framework;
using Travellers.UI.Models;
using Travellers.UI.PlayerInventory;

namespace WorldsAdriftReborn.Patching.Inventory
{

    [HarmonyPatch(typeof(BaseSheetModule), "SetModuleSelected")]
    public static class SheetSelectFix
    {
        [HarmonyPrefix]
        public static bool Prefix( BaseSheetModule __instance )
        {
            if (__instance.ScreenModule == null)
                return false;
            return true;
        }
    }

    [HarmonyPatch(typeof(InventoryVisualiser), "OnEnable")]
    public static class ReferenceDataFakeLoad
    {
        [HarmonyPostfix]
        public static void Postfix()
        {
            var r = LocalPlayer.Instance.referenceDataVisualiser;
            var c = AccessTools.Method(typeof(ReferenceDataVisualiser), "CheckInit");
            c.Invoke(r, new[] { (object)ReferenceDataVisualiser.ReferenceDataType.InventoryData });
            c.Invoke(r, new[] { (object)ReferenceDataVisualiser.ReferenceDataType.ResourceDescriptions });
            c.Invoke(r, new[] { (object)ReferenceDataVisualiser.ReferenceDataType.ScrapItems });
            c.Invoke(r, new[] { (object)ReferenceDataVisualiser.ReferenceDataType.SteamInvBundles });
            c.Invoke(r, new[] { (object)ReferenceDataVisualiser.ReferenceDataType.Schematics });
        }
    }

    [HarmonyPatch(typeof(ReferenceDataVisualiser), "OnEnable")]
    public static class ReferenceDataPatch
    {
        [HarmonyPrefix]
        public static void Prefix( LazyUIInterface<ISchematicSystem> ____schematicSystem )
        {
            string jsonData = "[{\"stackingMax\": 1, \"itemTypeId\": \"glider\", \"numOfSlotsHeight\": 4, \"numOfSlotsWidth\": 3, \"name\": \"Glider\", \"category\": \"Equipment\", \"iconName\": \"crafted items/3x4_glider\", \"equippable\": true, \"wearable\": \"Utility\", \"metadata\": {\"totalHealth\": \"100\", \"health\": \"100\"}}, {\"stackingMax\": 99, \"itemTypeId\": \"guitar\", \"numOfSlotsHeight\": 2, \"numOfSlotsWidth\": 6, \"name\": \"Guitar\", \"category\": \"Instrument\", \"iconName\": \"crafted items/6x2_guitar\", \"equippable\": true, \"wearable\": \"Tool\", \"metadata\": {\"totalHealth\": \"100\", \"health\": \"100\"}}, {\"stackingMax\": 99, \"itemTypeId\": \"marauder_guitar\", \"numOfSlotsHeight\": 2, \"numOfSlotsWidth\": 6, \"name\": \"MarauderGuitar\", \"category\": \"Instrument\", \"iconName\": \"crafted items/6x2_marauder_guitar\", \"equippable\": true, \"wearable\": \"Tool\", \"metadata\": {\"totalHealth\": \"100\", \"health\": \"100\"}}, {\"stackingMax\": 99, \"itemTypeId\": \"iron\", \"numOfSlotsHeight\": 2, \"numOfSlotsWidth\": 3, \"name\": \"Iron\", \"description\": \"Reasonably tough and heat resistant given its modest weight.\", \"category\": \"Metal\", \"iconName\": \"metals/Metal_Iron\", \"equippable\": false, \"rarity\": 0, \"wearable\": \"None\", \"metadata\": {}}, {\"stackingMax\": 99, \"itemTypeId\": \"lead\", \"numOfSlotsHeight\": 2, \"numOfSlotsWidth\": 3, \"name\": \"Lead\", \"description\": \"Durable and strong but too heavy for many uses.\", \"category\": \"Metal\", \"iconName\": \"metals/Metal_Lead\", \"equippable\": false, \"rarity\": 0, \"wearable\": \"None\", \"metadata\": {}}, {\"stackingMax\": 99, \"itemTypeId\": \"bronze\", \"numOfSlotsHeight\": 2, \"numOfSlotsWidth\": 3, \"name\": \"Bronze\", \"description\": \"Capable of withstanding a lot of stress for its medium weight.\", \"category\": \"Metal\", \"iconName\": \"metals/Metal_Bronze\", \"equippable\": false, \"rarity\": 0, \"wearable\": \"None\", \"metadata\": {}}, {\"stackingMax\": 99, \"itemTypeId\": \"tin\", \"numOfSlotsHeight\": 2, \"numOfSlotsWidth\": 3, \"name\": \"Tin\", \"description\": \"Lightweight but weak, soft and susceptible to heat.\", \"category\": \"Metal\", \"iconName\": \"metals/Metal_Tin\", \"equippable\": false, \"rarity\": 1, \"wearable\": \"None\", \"metadata\": {}}, {\"stackingMax\": 99, \"itemTypeId\": \"orthite\", \"numOfSlotsHeight\": 2, \"numOfSlotsWidth\": 3, \"name\": \"Orthite\", \"description\": \"Moderately conductive, stress resistant, but weak to heat.\", \"category\": \"Metal\", \"iconName\": \"metals/Metal_Orthite\", \"equippable\": false, \"rarity\": 1, \"wearable\": \"None\", \"metadata\": {}}, {\"stackingMax\": 99, \"itemTypeId\": \"steel\", \"numOfSlotsHeight\": 2, \"numOfSlotsWidth\": 3,\"name\": \"Steel\", \"description\": \"Very hard and high performance metal for its weight.\", \"category\": \"Metal\", \"iconName\": \"metals/Metal_Steel\", \"equippable\": false, \"rarity\": 1, \"wearable\": \"None\", \"metadata\": {}}, {\"stackingMax\": 99, \"itemTypeId\": \"copper\", \"numOfSlotsHeight\": 2, \"numOfSlotsWidth\": 3, \"name\": \"Copper\", \"description\": \"A great conductor but otherwise unexceptional.\", \"category\": \"Metal\", \"iconName\": \"metals/Metal_Copper\", \"equippable\": false, \"rarity\": 1, \"wearable\": \"None\", \"metadata\": {}}, {\"stackingMax\": 99, \"itemTypeId\": \"titanium\", \"numOfSlotsHeight\": 2, \"numOfSlotsWidth\": 3, \"name\": \"Titanium\", \"description\": \"Very light for such a hard metal but a bad conductor.\", \"category\": \"Metal\", \"iconName\": \"metals/Metal_Titanium\", \"equippable\": false, \"rarity\": 2, \"wearable\": \"None\", \"metadata\": {}}, {\"stackingMax\": 99, \"itemTypeId\": \"nickel\", \"numOfSlotsHeight\": 2, \"numOfSlotsWidth\": 3, \"name\": \"Nickel\", \"description\": \"Versatile thanks to its medium weight and a few weaknesses.\", \"category\": \"Metal\", \"iconName\": \"metals/Metal_Nickel\", \"equippable\": false, \"rarity\": 2, \"wearable\": \"None\", \"metadata\": {}}, {\"stackingMax\": 99, \"itemTypeId\": \"epilar\", \"numOfSlotsHeight\": 2, \"numOfSlotsWidth\": 3, \"name\": \"Epilar\", \"description\": \"Durable and resistant to stress but not really hard.\", \"category\": \"Metal\", \"iconName\": \"metals/Metal_Epilar\", \"equippable\": false, \"rarity\": 2, \"wearable\": \"None\", \"metadata\": {}}, {\"stackingMax\": 99, \"itemTypeId\": \"silver\", \"numOfSlotsHeight\": 2, \"numOfSlotsWidth\": 3, \"name\": \"Silver\", \"description\": \"A jack of all trades but master of onlyconductivity.\", \"category\": \"Metal\", \"iconName\": \"metals/Metal_Silver\", \"equippable\": false, \"rarity\": 2, \"wearable\": \"None\", \"metadata\": {}}, {\"stackingMax\": 99, \"itemTypeId\": \"aluminium\", \"numOfSlotsHeight\": 2, \"numOfSlotsWidth\": 3, \"name\": \"Aluminium\", \"description\": \"Extremely light without compromising too much on strength\", \"category\": \"Metal\",\"iconName\": \"metals/Metal_Aluminium\", \"equippable\": false, \"rarity\": 3, \"wearable\": \"None\", \"metadata\": {}}, {\"stackingMax\": 99, \"itemTypeId\": \"gold\", \"numOfSlotsHeight\": 2, \"numOfSlotsWidth\": 3, \"name\": \"Gold\", \"description\": \"Dense yet malleable with extremely high conductivity.\", \"category\": \"Metal\", \"iconName\": \"metals/Metal_Gold\", \"equippable\":false, \"rarity\": 3, \"wearable\": \"None\", \"metadata\": {}}, {\"stackingMax\": 99, \"itemTypeId\": \"eternium\", \"numOfSlotsHeight\": 2, \"numOfSlotsWidth\": 3, \"name\": \"Eternium\", \"description\": \"Hard,durable, heat resisantant but poor under stress.\", \"category\": \"Metal\", \"iconName\": \"metals/Metal_Eternium\", \"equippable\": false, \"rarity\": 3, \"wearable\": \"None\", \"metadata\": {}}, {\"stackingMax\": 99, \"itemTypeId\": \"tungsten\", \"numOfSlotsHeight\": 2, \"numOfSlotsWidth\": 3, \"name\": \"Tungsten\", \"description\": \"Unparalleled resistance to the elements but very heavy.\", \"category\": \"Metal\", \"iconName\": \"metals/Metal_Tungsten\", \"equippable\": false, \"rarity\": 3, \"wearable\": \"None\", \"metadata\": {}}, {\"stackingMax\": 99, \"itemTypeId\": \"birch\", \"numOfSlotsHeight\": 2, \"numOfSlotsWidth\": 3, \"name\": \"Birch Wood\", \"category\": \"Wood\", \"iconName\": \"woods/Wood_Birch\", \"equippable\": false, \"wearable\": \"None\", \"metadata\": {}}, {\"stackingMax\": 99, \"itemTypeId\": \"fuel\", \"numOfSlotsHeight\": 2, \"numOfSlotsWidth\": 2, \"name\": \"Fuel\", \"category\": \"Fuel\", \"iconName\": \"misc craft materials/2x2_Fuel_container\", \"equippable\": false, \"wearable\": \"None\", \"metadata\": {}}, {\"stackingMax\": 99, \"itemTypeId\": \"torch\", \"numOfSlotsHeight\": 4, \"numOfSlotsWidth\": 1, \"name\": \"Torch\", \"category\": \"\", \"iconName\":\"crafted items/1x4_Torch\", \"equippable\": false, \"wearable\": \"None\", \"metadata\": {\"totalHealth\": \"100\", \"health\": \"100\"}}, {\"stackingMax\": 99, \"itemTypeId\": \"gauntlet_salvage\", \"numOfSlotsHeight\": 0, \"numOfSlotsWidth\": 0, \"name\": \"Salvage Item\", \"category\": \"\", \"iconName\": \"Gauntlet_Salvage\", \"equippable\": true, \"wearable\": \"Tool\", \"metadata\": {}}, {\"stackingMax\": 99, \"itemTypeId\": \"gauntlet_repair\", \"numOfSlotsHeight\": 0, \"numOfSlotsWidth\": 0, \"name\": \"Repair Item\", \"category\": \"\", \"iconName\": \"Gauntlet_Repair\", \"equippable\": true, \"wearable\": \"Tool\", \"metadata\": {}}, {\"stackingMax\": 99, \"itemTypeId\": \"gauntlet_build\", \"numOfSlotsHeight\": 0, \"numOfSlotsWidth\": 0, \"name\": \"Build Item\", \"category\": \"\", \"iconName\": \"Gauntlet_Build\", \"equippable\": true, \"wearable\": \"Tool\", \"metadata\": {}}, {\"stackingMax\": 99, \"itemTypeId\": \"gauntlet_scanner\", \"numOfSlotsHeight\": 0, \"numOfSlotsWidth\": 0, \"name\": \"Scanner Item\", \"category\": \"\", \"iconName\": \"Scanner_Scan\", \"equippable\": true, \"wearable\": \"Tool\", \"metadata\": {}}, {\"stackingMax\": 99, \"itemTypeId\": \"scrapItem-Founder's Tome\", \"numOfSlotsHeight\": 2, \"numOfSlotsWidth\": 2, \"name\": \"Founder's Tome\", \"category\": \"Founder's Pack\", \"iconName\": \"scrap items/2x2.Book.Of.Entitlement\", \"equippable\": false, \"wearable\": \"None\", \"metadata\": {\"FoundersTomePlayerName\": \"\"}}, {\"stackingMax\": 99, \"itemTypeId\": \"scrapItem-woodenbowl\", \"numOfSlotsHeight\": 2, \"numOfSlotsWidth\": 2, \"iconName\": \"scrap items/2x2_Wooden_bowl\", \"equippable\": false, \"wearable\": \"None\", \"metadata\": {\"title\": \"Wooden Bowl\", \"description\": \"A deep bowl\"}}, {\"stackingMax\": 99, \"itemTypeId\": \"head_christmas\", \"name\": \"Antlers of Kif\", \"category\": \"Founder's Pack\", \"numOfSlotsHeight\": 2, \"numOfSlotsWidth\": 3, \"iconName\": \"clothing/heads/3x2_head_christmas_male\", \"equippable\": true, \"wearable\": \"Head\", \"metadata\": {\"PrimaryColor\": \"#5E4538\", \"SecondaryColor\": \"#595E53\", \"TertiaryColor\": \"#7C8275\"}}, {\"stackingMax\": 99, \"itemTypeId\": \"head_skullmask\", \"numOfSlotsHeight\":2, \"numOfSlotsWidth\": 2, \"name\": \"Barguan Skull Mask\", \"category\": \"Founder's Pack\", \"iconName\": \"clothing/heads/2x2_head_skullmask_male\", \"equippable\": true, \"wearable\": \"Head\", \"metadata\": {\"PrimaryColor\": \"6F6F6FFF\", \"SecondaryColor\": \"83FFF0FF\"}, \"colours\": [{\"PrimaryColor\": \"6F6F6FFF\", \"SecondaryColor\": \"83FFF0FF\"}]}, {\"stackingMax\": 99, \"itemTypeId\": \"head_hoodVariantA\", \"name\": \"Clawscarf of Capulca\", \"category\": \"Founder's Pack\", \"numOfSlotsHeight\": 2, \"numOfSlotsWidth\": 2, \"iconName\": \"clothing/heads/2x2_head_hoodVariantA_male\", \"equippable\": true, \"wearable\": \"Head\", \"metadata\": {\"PrimaryColor\": \"454680\", \"SecondaryColor\": \"FFFFFF\"}, \"colours\": [{\"PrimaryColor\": \"454680\", \"SecondaryColor\": \"FFFFFF\"}]}, {\"stackingMax\": 99, \"itemTypeId\": \"head_devhat\", \"numOfSlotsHeight\": 2, \"numOfSlotsWidth\": 2, \"name\": \"Developer's Cap\", \"category\": \"-\", \"iconName\": \"clothing/heads/2x2_head_devhat_male\", \"equippable\": true, \"wearable\": \"Head\", \"metadata\": {\"PrimaryColor\": \"#15161B\", \"SecondaryColor\": \"#ABB8C4\", \"TertiaryColor\": \"#676E76\"}}, {\"stackingMax\": 99, \"itemTypeId\": \"head_drissiancowl\", \"numOfSlotsHeight\": 2, \"numOfSlotsWidth\": 2, \"name\": \"Drissian Cowl\", \"category\": \"Pioneer Edition\", \"iconName\": \"clothing/heads/2x2_head_pioneer_male\", \"equippable\": true, \"wearable\": \"Head\", \"metadata\": {\"PrimaryColor\": \"#4A4131\", \"SecondaryColor\": \"#302720\", \"TertiaryColor\": \"#73624F\"}}, {\"stackingMax\": 99, \"itemTypeId\": \"head_olk\", \"numOfSlotsHeight\": 2, \"numOfSlotsWidth\": 2, \"category\": \"-\", \"name\": \"Forgotten Horns of the Olk\", \"iconName\": \"clothing/heads/2x2_head_goatmask_male\", \"equippable\": true, \"wearable\": \"Head\", \"metadata\": {\"PrimaryColor\": \"#7A818B\", \"SecondaryColor\": \"#4A4E55\", \"TertiaryColor\": \"#020202\"}}, {\"stackingMax\": 99, \"itemTypeId\": \"head_bargu_mask\", \"name\": \"Mask of the Bargu\", \"category\": \"Steam Items\", \"numOfSlotsHeight\": 3, \"numOfSlotsWidth\": 3, \"iconName\": \"clothing/heads/3x3_head_bargu_mask_male\", \"equippable\": true, \"wearable\": \"Head\", \"metadata\": {\"PrimaryColor\": \"898989\", \"SecondaryColor\": \"FFD76F\", \"TertiaryColor\": \"#B09500\"}, \"colours\": [{\"PrimaryColor\": \"898989\", \"SecondaryColor\": \"FFD76F\"}]}, {\"stackingMax\": 99, \"itemTypeId\": \"head_intucki_mask\", \"name\": \"Mask of the Intucki\", \"category\": \"Steam Items\", \"numOfSlotsHeight\": 2, \"numOfSlotsWidth\": 2, \"iconName\": \"clothing/heads/2x2_head_intucki_mask_male\", \"equippable\": true, \"wearable\": \"Head\", \"metadata\": {\"PrimaryColor\": \"FFF3CC\", \"SecondaryColor\": \"4A97F0\", \"TertiaryColor\": \"#005693\"}, \"colours\": [{\"PrimaryColor\": \"FFF3CC\", \"SecondaryColor\": \"4A97F0\"}]}, {\"stackingMax\": 99, \"itemTypeId\":\"head_tamoe_mask\", \"name\": \"Mask of the Tamoe\", \"category\": \"Steam Items\", \"numOfSlotsHeight\": 2, \"numOfSlotsWidth\": 2, \"iconName\": \"clothing/heads/2x2_head_tamoe_mask_male\", \"equippable\": true, \"wearable\": \"Head\", \"metadata\": {\"PrimaryColor\": \"9C948D\", \"SecondaryColor\": \"E15F45\", \"TertiaryColor\": \"#25201D\"}, \"colours\": [{\"PrimaryColor\": \"9C948D\", \"SecondaryColor\": \"E15F45\"}]}, {\"stackingMax\": 99, \"itemTypeId\": \"head_yharma_mask\", \"name\": \"Mask of the Yharma\", \"category\": \"Steam Items\", \"numOfSlotsHeight\": 2, \"numOfSlotsWidth\": 2, \"iconName\": \"clothing/heads/2x2_head_yharma_mask_male\", \"equippable\": true, \"wearable\": \"Head\", \"metadata\": {\"PrimaryColor\": \"C7A094\", \"SecondaryColor\": \"FFF3CC\", \"TertiaryColor\": \"#8D6953\"}, \"colours\": [{\"PrimaryColor\": \"C7A094\", \"SecondaryColor\": \"FFF3CC\"}]}, {\"stackingMax\": 99, \"itemTypeId\": \"head_mask_natiq\", \"name\": \"Natiq Mask\", \"category\": \"Steam Items\", \"numOfSlotsHeight\": 2, \"numOfSlotsWidth\": 2, \"iconName\": \"clothing/heads/2x2_head_christmas_2018_male\", \"equippable\": true, \"wearable\": \"Head\", \"metadata\": {\"PrimaryColor\": \"#26120D\", \"SecondaryColor\": \"#7A7872\", \"TertiaryColor\": \"#373632\"}}, {\"stackingMax\": 99, \"itemTypeId\": \"torso_tribal_skeleton\", \"numOfSlotsHeight\": 3, \"numOfSlotsWidth\": 2, \"name\": \"Barguan Skeleton Paint\", \"category\": \"Founder's Pack\", \"iconName\": \"clothing/torsos/2x3_torso_tribal_skeleton_male\", \"equippable\": true,\"wearable\": \"Body\", \"metadata\": {\"PrimaryColor\": \"6F6F6FFF\", \"SecondaryColor\": \"83FFF0FF\", \"TertiaryColor\": \"FFA056\"}, \"colours\": [{\"PrimaryColor\": \"6F6F6FFF\", \"SecondaryColor\": \"83FFF0FF\", \"TertiaryColor\": \"FFA056\"}]}, {\"stackingMax\": 99, \"itemTypeId\": \"female_torso_tribal_skeleton\", \"numOfSlotsHeight\": 3, \"numOfSlotsWidth\": 2, \"category\": \"Founder's Pack\", \"iconName\": \"clothing/torsos/2x3_torso_tribal_skeleton_female\", \"equippable\": true, \"wearable\": \"Body\", \"metadata\": {\"PrimaryColor\": \"#3C4045\",\"SecondaryColor\": \"#462C22\", \"TertiaryColor\": \"#6A5A49\"}}, {\"stackingMax\": 99, \"itemTypeId\": \"torso_devjacket\", \"numOfSlotsHeight\": 3, \"numOfSlotsWidth\": 2, \"name\": \"Developer's Jacket\", \"category\": \"-\", \"iconName\": \"clothing/torsos/2x3_torso_devjacket_male\", \"equippable\": true, \"wearable\": \"Body\", \"metadata\": {\"PrimaryColor\": \"FFFFFF\", \"SecondaryColor\": \"FF892D\", \"TertiaryColor\": \"383838\"}, \"colours\": [{\"PrimaryColor\": \"FFFFFF\", \"SecondaryColor\": \"FF892D\", \"TertiaryColor\": \"383838\"}]}, {\"stackingMax\": 99, \"itemTypeId\":\"torso_kahi\", \"name\": \"Kahi Ceremony Parka\", \"category\": \"Steam Items\", \"numOfSlotsHeight\": 3, \"numOfSlotsWidth\": 2, \"iconName\": \"clothing/torsos/2x3_torso_christmas_2018_male\", \"equippable\": true, \"wearable\": \"Body\", \"metadata\": {\"PrimaryColor\": \"#261511\", \"SecondaryColor\": \"#5C3229\", \"TertiaryColor\": \"#000000\"}}, {\"stackingMax\": 99, \"itemTypeId\": \"torso_tamoe\", \"name\": \"Tamoe War Belt\", \"category\": \"Steam Items\", \"numOfSlotsHeight\": 3, \"numOfSlotsWidth\": 2, \"iconName\": \"clothing/torsos/2x3_torso_tribal_tamoe_male\", \"equippable\": true, \"wearable\": \"Body\", \"metadata\": {\"PrimaryColor\": \"#3C4045\", \"SecondaryColor\": \"#5F4C3A\", \"TertiaryColor\": \"#5D5E60\"}}, {\"stackingMax\": 99, \"itemTypeId\": \"torso_tamoe_female\", \"name\": \"Tamoe War Belt\", \"category\": \"Steam Items\", \"numOfSlotsHeight\": 3, \"numOfSlotsWidth\": 2, \"iconName\": \"clothing/torsos/2x3_torso_tribal_tamoe_female\", \"equippable\": true, \"wearable\": \"Body\", \"metadata\": {\"PrimaryColor\": \"#3A3E43\", \"SecondaryColor\": \"#5D4B39\", \"TertiaryColor\": \"#5E5F61\"}}, {\"stackingMax\": 99, \"itemTypeId\": \"torso_vinicoti\", \"name\": \"Vinicoti Holiday Beads\", \"category\": \"Steam Items\", \"numOfSlotsHeight\": 3, \"numOfSlotsWidth\": 2, \"iconName\": \"clothing/torsos/2x3_torso_summer_male\", \"equippable\": true, \"wearable\": \"Body\", \"metadata\": {\"PrimaryColor\": \"#3C4045\", \"SecondaryColor\": \"#85382D\", \"TertiaryColor\": \"#D2583E\"}}, {\"stackingMax\": 99, \"itemTypeId\": \"torso_vinicoti_female\", \"name\": \"Vinicoti Holiday Beads\", \"category\": \"Steam Items\", \"numOfSlotsHeight\": 3, \"numOfSlotsWidth\": 2, \"iconName\": \"clothing/torsos/2x3_torso_summer_female\", \"equippable\": true, \"wearable\": \"Body\", \"metadata\": {\"PrimaryColor\": \"#3D4146\", \"SecondaryColor\": \"#4A2A20\", \"TertiaryColor\": \"#CB543C\"}}, {\"stackingMax\": 99, \"itemTypeId\": \"legs_tribal_skeleton\", \"numOfSlotsHeight\": 4, \"numOfSlotsWidth\": 2, \"name\": \"Barguan Skeleton Skirt\", \"category\": \"Founder's Pack\", \"iconName\": \"clothing/legs/2x4_legs_tribal_skeleton_male\", \"equippable\": true, \"wearable\": \"Feet\", \"metadata\": {\"PrimaryColor\": \"6F6F6FFF\", \"SecondaryColor\": \"83FFF0FF\", \"TertiaryColor\": \"#2A2C2D\"}, \"colours\": [{\"PrimaryColor\": \"6F6F6FFF\", \"SecondaryColor\": \"83FFF0FF\"}]}, {\"stackingMax\": 99, \"itemTypeId\": \"female_legs_tribal_skeleton\", \"numOfSlotsHeight\": 4, \"numOfSlotsWidth\": 2, \"name\": \"BarguanSkeleton Skirt\", \"category\": \"Founder's Pack\", \"iconName\": \"clothing/legs/2x4_legs_tribal_skeleton_female\", \"equippable\": true, \"wearable\": \"Feet\", \"metadata\": {\"PrimaryColor\": \"#635742\", \"SecondaryColor\": \"#2D3032\", \"TertiaryColor\": \"#B5FFFF\"}}, {\"stackingMax\": 99, \"itemTypeId\": \"legs_kahi\", \"name\": \"Kahi Ceremony Leggings\", \"category\": \"Steam Items\", \"numOfSlotsHeight\": 4, \"numOfSlotsWidth\": 2, \"iconName\": \"clothing/legs/2x4_legs_christmas_2018_male\", \"equippable\": true, \"wearable\": \"Feet\", \"metadata\":{\"PrimaryColor\": \"#A2A09F\", \"SecondaryColor\": \"#311B17\", \"TertiaryColor\": \"#7D7C78\"}}, {\"stackingMax\": 99, \"itemTypeId\": \"legs_kahi_female\", \"name\": \"Kahi Ceremony Leggings\", \"category\": \"Steam Items\", \"numOfSlotsHeight\": 4, \"numOfSlotsWidth\": 2, \"iconName\": \"clothing/legs/2x4_legs_christmas_2018_female\", \"equippable\": true, \"wearable\": \"Feet\", \"metadata\": {\"PrimaryColor\": \"#949290\", \"SecondaryColor\": \"#311C18\", \"TertiaryColor\": \"#424542\"}}, {\"stackingMax\": 99, \"itemTypeId\": \"legs_tamoe\", \"name\": \"Tamoe Skirt\", \"category\": \"Steam Items\", \"numOfSlotsHeight\": 4, \"numOfSlotsWidth\": 2, \"iconName\": \"clothing/legs/2x4_legs_tribal_tamoe_male\", \"equippable\": true, \"wearable\": \"Feet\", \"metadata\": {\"PrimaryColor\": \"#3C3F44\", \"SecondaryColor\": \"#60553F\", \"TertiaryColor\": \"#B44000\"}}, {\"stackingMax\": 99, \"itemTypeId\": \"legs_tamoe_female\", \"name\": \"Tamoe Skirt\", \"category\": \"Steam Items\", \"numOfSlotsHeight\": 4, \"numOfSlotsWidth\": 2, \"iconName\": \"clothing/legs/2x4_legs_tribal_tamoe_female\", \"equippable\": true, \"wearable\": \"Feet\", \"metadata\": {\"PrimaryColor\": \"#635742\", \"SecondaryColor\": \"#3C3F44\", \"TertiaryColor\": \"#836D4A\"}}, {\"stackingMax\": 99, \"itemTypeId\": \"legs_vinicoti\", \"name\": \"Vinicoti Holiday Wrap\", \"category\": \"Steam Items\", \"numOfSlotsHeight\": 4, \"numOfSlotsWidth\": 2, \"iconName\": \"clothing/legs/2x4_legs_summer_male\", \"equippable\": true, \"wearable\": \"Feet\", \"metadata\": {\"PrimaryColor\":\"#3C3F44\", \"SecondaryColor\": \"#315699\", \"TertiaryColor\": \"#4790E1\"}}, {\"stackingMax\": 99, \"itemTypeId\": \"legs_vinicoti_female\", \"name\": \"Vinicoti Holiday Wrap\", \"category\": \"Steam Items\", \"numOfSlotsHeight\": 4, \"numOfSlotsWidth\": 2, \"iconName\": \"clothing/legs/2x4_legs_summer_female\", \"equippable\": true, \"wearable\": \"Feet\", \"metadata\": {\"PrimaryColor\": \"#3C3F44\", \"SecondaryColor\": \"#315595\", \"TertiaryColor\": \"#4A96E7\"}}, {\"stackingMax\": 99, \"itemTypeId\": \"head_horns\", \"numOfSlotsHeight\": 2, \"numOfSlotsWidth\": 4, \"category\": \"\", \"name\": \"Horns\", \"iconName\": \"clothing/heads/4x2_head_horned_male\", \"equippable\": true, \"wearable\": \"Head\", \"metadata\": {\"PrimaryColor\": \"#B2BCCB\", \"SecondaryColor\":\"#4A4C40\", \"TertiaryColor\": \"#747464\"}}, {\"stackingMax\": 99, \"itemTypeId\": \"head_saborianhood\", \"numOfSlotsHeight\": 2, \"numOfSlotsWidth\": 2, \"name\": \"Saborian Hood\", \"category\": \"\", \"iconName\":\"clothing/heads/2x2_head_saborianmask_hoodscarf_male\", \"equippable\": true, \"wearable\": \"Head\", \"metadata\": {\"PrimaryColor\": \"#727B82\", \"SecondaryColor\": \"#545A5E\", \"TertiaryColor\": \"#B5BECE\"}}, {\"stackingMax\": 99, \"itemTypeId\": \"torso_poncho\", \"numOfSlotsHeight\": 3, \"numOfSlotsWidth\": 2, \"name\": \"Poncho\", \"category\": \"Clothing\", \"iconName\": \"clothing/torsos/2x3_torso_poncho_male\", \"equippable\": true, \"wearable\": \"Body\", \"metadata\": {\"PrimaryColor\": \"566B8E\", \"SecondaryColor\": \"E15F45\", \"TertiaryColor\": \"FFFFFF\"}, \"colours\": [{\"PrimaryColor\": \"566B8E\", \"SecondaryColor\": \"E15F45\", \"TertiaryColor\": \"FFFFFF\"}, {\"PrimaryColor\": \"545454\", \"SecondaryColor\": \"5672C3\", \"TertiaryColor\": \"fff0c3\"}, {\"PrimaryColor\": \"b25252\", \"SecondaryColor\": \"DB7A2F\", \"TertiaryColor\": \"FFD76F\"}]}]";
            InventoryItemManager.Instance.DeserialiseJson(jsonData);
            InventoryItemManager.Instance.ScrapItemDescriptions = new Map<string, string>
            {
                {"scrapItem-woodenbowl", "A deep bowl."}
            };
            InventoryItemManager.Instance.SteamInventoryBundlesDescriptions = new Map<string, string>
            {
                {"steamInvBundle-xmas_present", "An ancient and unobtainable treasure..."}
            };
            InventoryItemManager.Instance.ResourcesDescriptions = new Map<string, string>
            {
                {"gold", "Dense yet malleable with extremely high conductivity."}
            };
            // var schematic = "{\"wing-default\":{\"rarity\":0,\"itemType\":\"fixed\",\"tags\":[],\"prefabId\":{\"prefabName\":\"ModularWing\",\"prefabModules\":{\"Aileron\":\"Wing\\/Airleon\\/Wing_Airleon_003\",\"Body\":\"Wing\\/Body\\/Wing_Body_003\",\"Connector\":\"Wing\\/Connector\\/Wing_Connector_002\",\"Tip\":\"Wing\\/Tip\\/Wing_Tip_002\"}},\"attachType\":\"side\",\"title\":\"Procedural Wing\",\"iconId\":\"ship_wing\",\"description\":\"Allows the helmsman to better steer, pitch and yaw their ship.\",\"timeToCraft\":7,\"baseHp\":13,\"baseStats\":{\"power\":15.085238456726,\"pivotSpeed\":16.35845375061},\"cypherSlots\":[]},\"category\":\"CraftingStation\",\"title\":\"Procedural Wing\",\"iconId\":\"ship_wing\",\"description\":\"Allows the helmsman to better steer, pitch and yaw their ship.\",\"timeToCraft\":7,\"amountToCraft\":1,\"idOfCreation\":\"\",\"craftingRequirements\":[{\"id\":0,\"name\":\"Metal\",\"iconId\":\"item_metal_large\",\"component\":\"Casing\",\"description\":\"The delicate mechanisms of the wing are housed in this protective sheath.\",\"amountRequired\":56},{\"id\":1,\"name\":\"Metal\",\"iconId\":\"item_metal_large\",\"component\":\"Aileron\",\"description\":\"A flap along the back edge of the wing, which is positioned to exert torque.\",\"amountRequired\":30},{\"id\":2,\"name\":\"Metal\",\"iconId\":\"item_metal_large\",\"component\":\"Mechanical Internals\",\"description\":\"The cogs and levers inside the wing, which rotate the aileron.\",\"amountRequired\":33}],\"baseHp\":0.12999999523163,\"baseStats\":{\"power\":0.15085238218307,\"pivotSpeed\":0.16358453035355},\"rarity\":0,\"itemType\":\"fixed\"}";
            var glider = "{\"glider\":{\"SchematicType\":0,\"uUID\":\"glider\",\"schematicId\":\"glider\",\"referenceData\":\"glider\",\"category\":\"Personal\",\"title\":\"cool glider\",\"iconId\":\"crafted items/3x4_glider\",\"description\":\"wolo\",\"timeToCraft\":10,\"amountToCraft\":1,\"itemType\":\"hmm\",\"craftingRequirements\":[],\"baseHp\":100.0,\"baseStats\":{},\"rarity\":1,\"cipherSlots\":[],\"unlearnable\":false,\"modules\":{},\"hullData\":\"hullData\",\"OrderedStats\":[],\"UniqueID\":\"glider\",\"CraftingCategoryEnum\":1,\"HumanReadableItemType\":\"Hmm\",\"rarityParsed\":1,\"HullDataBytes\":\"hullData\",\"IsProcedural\":false,\"IsShip\":false,\"cipherSlotParsed\":[]}}";
            ____schematicSystem.Value.DeserialiseJson(glider);
        }
    }
}
