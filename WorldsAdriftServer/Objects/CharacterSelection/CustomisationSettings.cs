using WorldsAdriftServer.Objects.UnityObjects;

namespace WorldsAdriftServer.Objects.CharacterSelection
{
    public static class CustomisationSettings
    {
        internal static Dictionary<string, CustomisationSettings.GenderItem> starterHeadItems = new Dictionary<string, CustomisationSettings.GenderItem>
        {
            {
                "hair_bald",
                new CustomisationSettings.GenderItem("male_hair_bald", "female_hair_bald")
            },
            {
                "hair_studded",
                new CustomisationSettings.GenderItem("male_hair_studded", "female_hair_studded")
            },
            {
                "hair_cropped",
                new CustomisationSettings.GenderItem("male_hair_cropped", "female_hair_cropped")
            },
            {
                "hair_squarefringe",
                new CustomisationSettings.GenderItem("male_hair_squarefringe", "female_hair_squarefringe")
            },
            {
                "hair_kilmer",
                new CustomisationSettings.GenderItem("male_hair_kilmer", "female_hair_kilmer")
            },
            {
                "hair_native",
                new CustomisationSettings.GenderItem("male_hair_native", "female_hair_native")
            },
            {
                "hair_bowlcut",
                new CustomisationSettings.GenderItem("male_hair_bowlcut", "female_hair_bowlcut")
            },
            {
                "hair_dreads",
                new CustomisationSettings.GenderItem("male_hair_dreadlocks", "female_hair_dreadlocks")
            },
            {
                "hair_swept",
                new CustomisationSettings.GenderItem("male_hair_swept", "female_hair_swept")
            },
            {
                "hair_uber",
                new CustomisationSettings.GenderItem("male_hair_uber", "female_hair_uber")
            },
            {
                "hair_fringe",
                new CustomisationSettings.GenderItem("male_hair_fringe", "female_hair_fringe")
            },
            {
                "hair_headband",
                new CustomisationSettings.GenderItem("male_hair_headband", "female_hair_headband")
            },
            {
                "hair_hightopdreads",
                new CustomisationSettings.GenderItem("male_hair_hightopdreads", "female_hair_hightopdreads")
            },
            {
                "hair_mohawk",
                new CustomisationSettings.GenderItem("male_hair_mohawk", "female_hair_mohawk")
            },
            {
                "hair_shortspiked",
                new CustomisationSettings.GenderItem("male_hair_shortspiked", "female_hair_shortspiked")
            },
            {
                "hair_braidbun",
                new CustomisationSettings.GenderItem("male_hair_braidbun", "female_hair_braidbun")
            }
        };

        internal static Dictionary<string, CustomisationSettings.GenderItem> starterFacialHairItems = new Dictionary<string, CustomisationSettings.GenderItem>
        {
            {
                string.Empty,
                new CustomisationSettings.GenderItem("male_facialhair_bald", string.Empty)
            },
            {
                "facialHair_hunterBeard",
                new CustomisationSettings.GenderItem("male_facialhair_hunterbeard", string.Empty)
            },
            {
                "facialHair_amishBeard",
                new CustomisationSettings.GenderItem("male_facialhair_amishbeard", string.Empty)
            },
            {
                "facialHair_hunterMoustache",
                new CustomisationSettings.GenderItem("male_facialhair_huntermustache", string.Empty)
            },
            {
                "facialHair_doublebraid_combo",
                new CustomisationSettings.GenderItem("male_facialHair_doublebraid_combo", string.Empty)
            },
            {
                "facialHair_doublebraid_moustache",
                new CustomisationSettings.GenderItem("male_facialHair_doublebraid_moustache", string.Empty)
            },
            {
                "facialHair_doublebraid_beard",
                new CustomisationSettings.GenderItem("male_facialHair_doublebraid_beard", string.Empty)
            },
            {
                "facialhair_vikingbeard",
                new CustomisationSettings.GenderItem("male_facialhair_vikingbeard", string.Empty)
            },
            {
                "facialhair_vikingcombo",
                new CustomisationSettings.GenderItem("male_facialhair_vikingcombo", string.Empty)
            },
            {
                "facialhair_vikingmustache",
                new CustomisationSettings.GenderItem("male_facialhair_vikingmustache", string.Empty)
            }
        };

        internal static Dictionary<string, CustomisationSettings.GenderItem> starterFaceItems = new Dictionary<string, CustomisationSettings.GenderItem>
        {
            {
                "face_A",
                new CustomisationSettings.GenderItem("male_face_1", "female_face_1")
            },
            {
                "face_B",
                new CustomisationSettings.GenderItem("male_face_2", "female_face_2")
            },
            {
                "face_C",
                new CustomisationSettings.GenderItem("male_face_3", "female_face_3")
            },
            {
                "face_D",
                new CustomisationSettings.GenderItem("male_face_4", "female_face_4")
            },
            {
                "face_E",
                new CustomisationSettings.GenderItem("male_face_5", "female_face_5")
            }
        };

        internal static Dictionary<string, CustomisationSettings.GenderItem> starterTorsoItems = new Dictionary<string, CustomisationSettings.GenderItem>
        {
            {
                "torso_wanderer",
                new CustomisationSettings.GenderItem("male_torso_wanderer", "female_torso_wanderer")
            },
            {
                "torso_ponchoVariantB",
                new CustomisationSettings.GenderItem("male_torso_ponchoVariantB", "female_torso_ponchoVariantB")
            },
            {
                "torso_squireVariantA",
                new CustomisationSettings.GenderItem("male_torso_squireVariantA", "female_torso_squireVariantA")
            },
            {
                "torso_engineer",
                new CustomisationSettings.GenderItem("male_torso_engineer", "female_torso_engineer")
            }
        };

        internal static Dictionary<string, CustomisationSettings.GenderItem> starterLegItems = new Dictionary<string, CustomisationSettings.GenderItem>
        {
            {
                "legs_shorts",
                new CustomisationSettings.GenderItem("male_legs_shorts", "female_legs_shorts")
            },
            {
                "legs_boots",
                new CustomisationSettings.GenderItem("male_legs_boots", "female_legs_boots")
            },
            {
                "legs_wrap",
                new CustomisationSettings.GenderItem("male_legs_wrap", "female_legs_wrap")
            }
        };

        internal static UnityColor[] hairColors = new UnityColor[]
        {
            UnityColor.FromHex("eaaa55"),
            UnityColor.FromHex("f1be5c"),
            UnityColor.FromHex("f8de87"),
            UnityColor.FromHex("fff5a9"),
            UnityColor.FromHex("fff9cc"),
            UnityColor.FromHex("5b3215"),
            UnityColor.FromHex("7f4a27"),
            UnityColor.FromHex("a46435"),
            UnityColor.FromHex("ca813f"),
            UnityColor.FromHex("e8774c"),
            UnityColor.FromHex("383838"),
            UnityColor.FromHex("787878"),
            UnityColor.FromHex("b8b8b8"),
            UnityColor.FromHex("fbfbfb"),
            UnityColor.FromHex("c65035")
        };

        internal static UnityColor[] skinColors = new UnityColor[]
        {
            UnityColor.FromHex("ffbe84"),
            UnityColor.FromHex("ffc897"),
            UnityColor.FromHex("ffc591"),
            UnityColor.FromHex("ffd2aa"),
            UnityColor.FromHex("ffdcbd"),
            UnityColor.FromHex("72553b"),
            UnityColor.FromHex("957252"),
            UnityColor.FromHex("b98f69"),
            UnityColor.FromHex("dcab80"),
            UnityColor.FromHex("ffcc9f"),
            UnityColor.FromHex("754e2c"),
            UnityColor.FromHex("96673f"),
            UnityColor.FromHex("b88354"),
            UnityColor.FromHex("d89d68"),
            UnityColor.FromHex("f9b77b")
        };

        internal static UnityColor[] lipColors = new UnityColor[]
        {
            UnityColor.FromHex("ffaf7a"),
            UnityColor.FromHex("ffb98c"),
            UnityColor.FromHex("ffb686"),
            UnityColor.FromHex("ffc39d"),
            UnityColor.FromHex("ffcdae"),
            UnityColor.FromHex("6f4e37"),
            UnityColor.FromHex("8e6a4c"),
            UnityColor.FromHex("af8461"),
            UnityColor.FromHex("d29e76"),
            UnityColor.FromHex("ffbd93"),
            UnityColor.FromHex("714829"),
            UnityColor.FromHex("8f5f3b"),
            UnityColor.FromHex("ae794d"),
            UnityColor.FromHex("ce9160"),
            UnityColor.FromHex("f5a872")
        };

        internal static UnityColor[] clothingColors = new UnityColor[]
        {
            UnityColor.FromHex("fff0c3"),
            UnityColor.FromHex("bdb388"),
            UnityColor.FromHex("836164"),
            UnityColor.FromHex("875648"),
            UnityColor.FromHex("b25252"),
            UnityColor.FromHex("566B8E"),
            UnityColor.FromHex("AC6C58"),
            UnityColor.FromHex("649060"),
            UnityColor.FromHex("556c5e"),
            UnityColor.FromHex("545454")
        };

        internal static string[] allHeads = new string[]
        {
            "Hair_Studded",
            "Hair_Cropped",
            "Hair_Squarefringe",
            "Hair_Kilmer",
            "Hair_Native",
            "Hair_Bowlcut",
            "Hair_Swept",
            "Hair_Uber",
            "hair_fringe",
            "hair_headband",
            "hair_hightopdreads",
            "hair_mohawk",
            "hair_shortspiked",
            "head_hood",
            "head_bandana",
            "head_hoodcap",
            "head_banditmask",
            "head_scarf",
            "head_featherhat",
            "head_wrap",
            "head_cap",
            "head_lallarahelm",
            "head_navigator",
            "head_turban",
            "head_skullcap",
            "head_tank",
            "head_owl",
            "head_rogue",
            "head_banditmaskpekoe",
            "head_metal",
            "head_circlemask",
            "head_godhand",
            "head_kitsunemask",
            "head_scarfhood",
            "head_pilotcap",
            "head_hoodVariantA",
            "head_goggles"
        };

        internal static string[] allTorsos = new string[]
        {
            "torso_naked",
            "torso_wanderer",
            "torso_ponchoVariantB",
            "torso_squireVariantA",
            "torso_engineer",
            "torso_ponchoB",
            "torso_neckguard",
            "torso_pirate",
            "torso_wrap",
            "torso_roman",
            "torso_bandages_ninja",
            "torso_cape",
            "torso_bandages",
            "torso_scales",
            "torso_combo",
            "torso_comboB",
            "torso_wandererVariantA",
            "torso_poncho",
            "torso_squire",
            "torso_engineerVariantA",
            "torso_ponchoBVariantA",
            "torso_ponchoVariantA",
            "torso_pirateVariantA",
            "torso_bandages_scales"
        };

        internal static string[] allLegs = new string[]
        {
            "legs_naked",
            "legs_shorts",
            "legs_boots",
            "legs_wrap",
            "legs_belts",
            "legs_goner",
            "legs_shinpads",
            "legs_baggy",
            "legs_shortsVariantA"
        };

        internal class GenderItem
        {
            public GenderItem( string maleItem, string femaleItem )
            {
                MaleItem = maleItem;
                FemaleItem = femaleItem;
            }

            public string MaleItem;

            public string FemaleItem;
        }
    }
}
