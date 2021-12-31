using Newtonsoft.Json;

namespace SSCConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Stack Size Controller Datafile / Configuration Converter v4.0");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Before continuing, please ensure that you have StackSizeController.json datafile in the same directory as this\n    executable.\n");
            Console.WriteLine("If you are converting from v3 to v4 a seperate file appended with '_config' will be generated with default settings\n    and instructions will be provided.");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("To continue enter the corresponding number for the conversion you wish to run-");
            Console.WriteLine("1. v3 to v4");
            Console.WriteLine("2. v2 to v4 (Unimplemented)");

            string conversion = Console.ReadLine() ?? string.Empty;

            if (conversion != string.Empty)
            {
                if (!File.Exists("StackSizeController.json"))
                {
                    Console.WriteLine("StackSizeController.json is not present. Please include the datafile in the same directory as this executable.\nPress any key to exit.");

                    Console.ReadKey(true);

                    return;
                }

                switch (conversion)
                {
                    case "1":
                        ConvertV3V4();

                        break;

                    case "2":
                        ConvertV2V4();

                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Press any key to exit.");
            }

            Console.ReadKey(true);
        }

        static void ConvertV3V4()
        {
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Beginning conversion of v3 datafile to v4 configuration file.");

            ItemIndexV3 v3Data = JsonConvert.DeserializeObject<ItemIndexV3>(File.ReadAllText("StackSizeController.json"));
            V4Configuration v4Config = new V4Configuration();

            foreach (KeyValuePair<string, List<ItemInfoV3>> itemCategory in v3Data.ItemCategories)
            {
                foreach (ItemInfoV3 itemInfo in itemCategory.Value)
                {
                    int stacksize = itemInfo.CustomStackSize;

                    if (stacksize == 0)
                    {
                        stacksize = itemInfo.VanillaStackSize;
                    }

                    v4Config.IndividualItemStackSize.Add(itemInfo.Shortname, stacksize);
                }
            }

            File.WriteAllText("StackSizeController_config.json", JsonConvert.SerializeObject(v4Config.IndividualItemStackSize, Formatting.Indented));

            Console.WriteLine("Conversion complete. Configuration file generated with the name StackSizeController_config.json.\n");
            Console.WriteLine("Open this file and copy the IndividualItemStackSize configuration values and paste them\ninto the new" +
                "configuration and reload the plugin.\n");
            Console.WriteLine("Press any key to exit.");
            Console.WriteLine("----------------------------------------");
        }

        static void ConvertV2V4()
        {
            Console.WriteLine("Unimplemented.");
        }

        private class V4Configuration
        {
            public bool RevertStackSizesToVanillaOnUnload = true;
            public bool AllowStackingItemsWithDurability = true;
            public bool HidePrefixWithPluginNameInMessages;

            public float GlobalStackMultiplier = 1;
            public Dictionary<string, float> CategoryStackMultipliers = new Dictionary<string, float>();
            public Dictionary<string, float> IndividualItemStackMultipliers = new Dictionary<string, float>();

            public SortedDictionary<string, int> IndividualItemStackSize = new SortedDictionary<string, int>();

            public VersionNumber VersionNumber = new VersionNumber();
        }

        private class ItemIndexV3
        {
            public Dictionary<string, List<ItemInfoV3>> ItemCategories;
            public VersionNumber VersionNumber;
        }

        private class VersionNumber
        {
            public int Major;
            public int Minor;
            public int Patch;
        }

        private class ItemInfoV3
        {
            public int ItemId;
            public string Shortname;
            public bool HasDurability;
            public int VanillaStackSize;
            public int CustomStackSize;
        }
    }
}