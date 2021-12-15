using System;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using System.Linq;


namespace TextBasedRPG
{
    class saveGame
    {

        public string playerName { get; set; }
        public float playerExp { get; set; }
        public string currentScene { get; set; }
        public string weaponName { get; set; }
        public float resourceAmt { get; set; }
        public string playerClass { get; set; }

        /*
        // Environment variable names for default, process, user, and machine targets.
        string defaultEnvVar = nameof(defaultEnvVar);
        string processEnvVar = nameof(processEnvVar);
        string userEnvVar = nameof(userEnvVar);
        string machineEnvVar = nameof(machineEnvVar);

        string dft = nameof(dft);
        string process = nameof(process);
        string user = nameof(user);
        string machine = nameof(machine);

        // Set the environment variable for each target.
        // The default target (the current process).
        Environment.SetEnvironmentVariable(defaultEnvVar, dft);
        // The current process.
        Environment.SetEnvironmentVariable(processEnvVar, process,
                                           EnvironmentVariableTarget.Process);
        // The current user.
        Environment.SetEnvironmentVariable(userEnvVar, user,
                                           EnvironmentVariableTarget.User);
        // The local machine.
        Environment.SetEnvironmentVariable(machineEnvVar, machine,
                                           EnvironmentVariableTarget.Machine);

        // Define an array of environment variables.
        string[] envVars = { defaultEnvVar,processEnvVar, userEnvVar, machineEnvVar };
        */

        public static string mainPath = @"C:\Users\natha\Documents\TextBasedRPG";
        public static string savesPath = @"C:\Users\natha\Documents\TextBasedRPG\saves";

        public saveGame(string name, float xp, string scene, string weapon, float resource, string _class)
        {

            playerName = name;
            playerExp = xp;
            resourceAmt = resource;
            weaponName = weapon;
            currentScene = scene;
            playerClass = _class;
        }


        public static void writeJson(string name, float xp, string scene, string weapon, float resource, string _class)
        {

            var saveFile = new saveGame(name, xp, scene, weapon, resource, _class)
            {

                playerName = name,
                playerExp = xp,
                resourceAmt = resource,
                weaponName = weapon,
                currentScene = scene,
                playerClass = _class

            };

            string fileName = $"{name}-{scene}-{_class}-SaveGame.json";
            var options = new JsonSerializerOptions { WriteIndented = true };
            //string jsonString = JsonSerializer.Serialize(saveFile); // create the json data string
            string jsonString = JsonSerializer.Serialize(saveFile, options); // create the json data string

            File.WriteAllText(fileName, jsonString); // write the data to a file and name it accordingly
            Console.WriteLine(File.ReadAllText(fileName)); // for debug purposes only

            // move file to saves folder
            string sourceFile = System.IO.Path.Combine(mainPath, fileName);
            string destFile = System.IO.Path.Combine(savesPath, fileName);
            System.IO.File.Move(sourceFile, destFile, true);

        }

    }
    class LoadGame
    {

        public static string mainPath = @"C:\Users\natha\Documents\TextBasedRPG";
        public static string savesPath = @"C:\Users\natha\Documents\TextBasedRPG\saves";
        public static List<String> readJson()
        {

            if (System.IO.Directory.Exists(savesPath))
            {

                // need a way to index the strings being printed so only the name of the file is displayed, not the entire path.
                string[] dirFiles = System.IO.Directory.GetFiles(savesPath);
                int i = 0;
                foreach (string s in dirFiles)
                {
                    Console.WriteLine($"{i + 1}. {dirFiles[i]}");
                    i++;
                }

                Console.WriteLine("Enter the number of the save file you'd like to load:\n");

                // take user input to select the file and index accordingly
                int saveFileChosen = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine(saveFileChosen);
                int indexValue = saveFileChosen - 1;
                string tempFileName = dirFiles[indexValue];
                string fileName = tempFileName.Remove(0, 44); // temporary hard coded file pathname trimming
                //should try splitting filename on slashes, and then indexing to the last item in the outputted list (ie. the actual filename.json)
                Console.WriteLine(fileName);

                // move the save file to main folder so it can be accessed.
                string sourceFile = System.IO.Path.Combine(savesPath, fileName);
                string destFile = System.IO.Path.Combine(mainPath, fileName);

                System.IO.File.Move(sourceFile, destFile, true);

                string jsonString = File.ReadAllText(destFile);
                player playerData = JsonSerializer.Deserialize<player>(jsonString);

                // type conversion for loaded data
                string expConvert = Convert.ToString(playerData.playerExp);
                string resConvert = Convert.ToString(playerData.resourceAmt);

                List<string> loadedFile = new List<string>();
                loadedFile.Add(playerData.playerName);
                loadedFile.Add(expConvert);
                loadedFile.Add(playerData.currentScene);
                loadedFile.Add(playerData.weaponName);
                loadedFile.Add(resConvert);
                loadedFile.Add(playerData.playerClass);

                // move the file back to the saves folder
                System.IO.File.Move(destFile, sourceFile, true);

                return loadedFile;
            }
            else
            {
                Console.WriteLine($"Error, Save folder path: {savesPath} does not exist.");
                return null;
            }
        }

    }
    public class player
    {
        public string playerName { get; set; }
        public float playerExp { get; set; }
        public string currentScene { get; set; }
        public string weaponName { get; set; }
        public float resourceAmt { get; set; }
        public string playerClass { get; set; }
    }
    public class charClass
    {
        public string classIn;
        public string userClass;
        //public string[] weapName = {""};
        public int numGenerator()
        {

            // random number generator
            int numSelect = 0;
            Random ranNum = new Random();
            numSelect = ranNum.Next(1, 10);

            return numSelect;


        }
        public void selectClass()
        {

            // get character name
            Console.WriteLine("Enter your character's name:");
            string charName = Console.ReadLine();
            // get class
            Console.WriteLine("Choose your class:");
            Console.WriteLine("Mage\nRogue\nDuelist\nRanger");
            classIn = Console.ReadLine();
            userClass = classIn.ToLower();

            string startingScene = "Awakening";

            int randomNum = numGenerator(); // call rng method

            switch (userClass)
            {
                case "mage":
                    Mage playerMage = new Mage(charName, randomNum); // call new mage constructor

                    // print a line to the user describing the new Mage they have just created.
                    Console.WriteLine($"{playerMage.mageName}, your new Mage, has just been created.\nThey have {playerMage.Mana} mana points, and are using {playerMage.rngWeaponNameMage} as their weapon of choice.");

                    //write to file to save this data for use in the rest of the game.
                    string mageClassID = "Mage";
                    saveGame mageInitialSave = new saveGame(playerMage.mageName, playerMage.mXP, startingScene, playerMage.rngWeaponNameMage, playerMage.Mana, mageClassID);
                    saveGame.writeJson(playerMage.mageName, playerMage.mXP, startingScene, playerMage.rngWeaponNameMage, playerMage.Mana, mageClassID);

                    break;
                case "rogue":
                    Rogue playerRogue = new Rogue(charName, randomNum); // call new mage constructor

                    // print a line to the user describing the new Mage they have just created.
                    Console.WriteLine($"{playerRogue.rogueName}, your new Rogue, has just been created.\nThey have {playerRogue.Stealth} stealth points, and are using {playerRogue.rngWeaponNameRogue} as their weapon of choice.");

                    //write to file to save this data for use in the rest of the game.
                    string roClassID = "Rogue";
                    saveGame roInitialSave = new saveGame(playerRogue.rogueName, playerRogue.roXP, startingScene, playerRogue.rngWeaponNameRogue, playerRogue.Stealth, roClassID);
                    saveGame.writeJson(playerRogue.rogueName, playerRogue.roXP, startingScene, playerRogue.rngWeaponNameRogue, playerRogue.Stealth, roClassID);

                    break;
                case "duelist":
                    Duelist playerDuel = new Duelist(charName, randomNum); // call new mage constructor

                    // print a line to the user describing the new Mage they have just created.
                    Console.WriteLine($"{playerDuel.duelName}, your new Duelist, has just been created.\nThey have {playerDuel.Strength} strength points, and are using {playerDuel.rngWeaponNameDuel} as their weapon of choice.");

                    //write to file to save this data for use in the rest of the game.
                    string duelClassID = "Duelist";
                    saveGame rogueInitialSave = new saveGame(playerDuel.duelName, playerDuel.dXP, startingScene, playerDuel.rngWeaponNameDuel, playerDuel.Strength, duelClassID);
                    saveGame.writeJson(playerDuel.duelName, playerDuel.dXP, startingScene, playerDuel.rngWeaponNameDuel, playerDuel.Strength, duelClassID);

                    break;
                case "ranger":
                    Ranger playerRanger = new Ranger(charName, randomNum); // call new mage constructor

                    // print a line to the user describing the new Mage they have just created.
                    Console.WriteLine($"{playerRanger.rangerName}, your new Ranger, has just been created.\nThey have {playerRanger.Dexterity} dexterity points, and are using {playerRanger.rngWeaponNameRanger} as their weapon of choice.");

                    //write to file to save this data for use in the rest of the game.
                    string raClassID = "Ranger";
                    saveGame rangerInitialSave = new saveGame(playerRanger.rangerName, playerRanger.raXP, startingScene, playerRanger.rngWeaponNameRanger, playerRanger.Dexterity, raClassID);
                    saveGame.writeJson(playerRanger.rangerName, playerRanger.raXP, startingScene, playerRanger.rngWeaponNameRanger, playerRanger.Dexterity, raClassID);

                    break;
                default:
                    Console.WriteLine("That is not a valid class for your character, try again.");
                    selectClass();
                    break;
            }
        }
    }
    public class Mage
    {

        public float mXP;
        public float Mana;
        public string mageName;
        public string rngWeaponNameMage;
        public string[] weapNamesMage = {
            "Lightning in a Bottle",
            "Wand of Flames",
            "Wand of Air",
            "Rudamentary Staff",
            "Kiekurakeppi", // special item
            "Programmer's Blessing", // special item (tooltip: "a tome of ancient knowledge with the words 'Lunnaya Vspyshka' ethched into the cover... Wonder what that could mean.")
            "Magic Eight Ball",
            "Rune of Kanai",
            "Enchanted Icicle",
            "Basic Wand"};

        public Mage(string name, int randomNumber)
        {

            Mana = 10.0f;
            mXP = 0f;
            mageName = name;

            rngWeaponNameMage = weapNamesMage[randomNumber];

        }
    }
    public class Rogue
    {

        public float roXP;
        public float Stealth;
        public string rogueName;
        public string rngWeaponNameRogue;
        public string[] weapNamesRogue = {
            "The Butter Knife",
            "Vampire Stake",
            "Duel Iron Daggers",
            "Hardened Staff",
            "Unbalanced Kunai",
            "Farmers Sickle", // special item (tooltip: "all you need now is a hammer!")
            "Copper Short Sword",
            "Weathered Dagger",
            "Carving Knife",
            "Brass Knuckles"};

        public Rogue(string name, int randomNumber)
        {

            Stealth = 10.0f;
            roXP = 0f;
            rogueName = name;

            rngWeaponNameRogue = weapNamesRogue[randomNumber];

        }
    }
    public class Ranger
    {

        public float raXP;
        public float Dexterity;
        public string rangerName;
        public string rngWeaponNameRanger;
        public string[] weapNamesRanger = {
            "Bent Shortbow",
            "Makeshift Hunting Bow",
            "Rusty Revolver",
            "Damaged Crossbow",
            "Basic Blowpipe",
            "Childhood Slingshot",
            "Town Guard Longbow",
            "Enchanted Throwing Dagger", // special item
            "Roman Candle",
            "Family Heirloom Longbow"}; // special item

        public Ranger(string name, int randomNumber)
        {

            Dexterity = 10.0f;
            raXP = 0f;
            rangerName = name;

            rngWeaponNameRanger = weapNamesRanger[randomNumber];

        }
    }
    public class Duelist
    {

        public float dXP;
        public float Strength;
        public string duelName;
        public string rngWeaponNameDuel;
        public string[] weapNamesDuel = {
            "Militia Short Sword",
            "Crude Mace",
            "Trusty Spear",
            "Guard's Longsword",
            "Firewood Axe",
            "Rusty Two Handed Scythe",
            "Father's Gift", //special item
            "Crude Blacksmith's Hammer",
            "Goblin Curveblade",
            "Orc Ripper"}; // special item

        public Duelist(string name, int randomNumber)
        {

            Strength = 10.0f;
            dXP = 0f;
            duelName = name;

            rngWeaponNameDuel = weapNamesDuel[randomNumber];

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // --- MAIN MENU --- //
            string MenuTitle = "Main Menu";
            titleUpdate(MenuTitle);
            menuSelect();

            // --- EXITING GAME --- //
            Console.WriteLine("Press any key to close the game.");
            Console.ReadKey();
        }
        static void titleUpdate(string newTitlePiece)
        {

            Console.Title = $"Placeholder - {newTitlePiece}";
        }

        static void menuCall()
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("MENU\n");
            Console.ResetColor();
            Console.WriteLine("1. Save Game\n2. Load Game\n3. Return to menu");

            string menuInput = Console.ReadLine().ToLower();

            switch (menuInput)
            {
                case "1":
                    //saveGame.writeJson();
                    Console.WriteLine("Save the game WIP");
                    break;
                case "2":
                    //LoadGame.readJson();
                    Console.WriteLine("Load a save, WIP");
                    break;
                case "3":
                    menuSelect();
                    break;
                default:
                    menuCall();
                    break;
            }
        }
        static void menuSelect()
        {
            //Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Welcome to placeholder.\n");
            //Console.ResetColor();
            SlowType(60);
            System.Threading.Thread.Sleep(160);
            TypeWriter("\n1. New Adventure\n2. Load Adventure\n3. Credits\n");

            string userChoice = Console.ReadLine().ToLower();

            switch (userChoice)
            {
                case "1":
                    charClass userCharacter = new charClass();
                    string cCreateTitle = "Character Creation";
                    titleUpdate(cCreateTitle);
                    userCharacter.selectClass();

                    Awakening();

                    break;
                case "2":
                    string loadedPlayerName;
                    string tempPlayerExp;
                    string loadedCurrentScene;
                    string loadedWeaponName;
                    string tempResourceAmt;
                    string loadedPlayerClass;


                    // print save file directory filenames and have user select a save file
                    List<string> loadedFileData = LoadGame.readJson();

                    // assign predeclared variables to values of the list at given indices
                    loadedPlayerName = loadedFileData.ElementAt(0);
                    tempPlayerExp = loadedFileData.ElementAt(1);
                    loadedCurrentScene = loadedFileData.ElementAt(2);
                    loadedWeaponName = loadedFileData.ElementAt(3);
                    tempResourceAmt = loadedFileData.ElementAt(4);
                    loadedPlayerClass = loadedFileData.ElementAt(5);

                    // convert temp variables to type float
                    float loadedPlayerExp = float.Parse(tempPlayerExp);
                    float loadedResourceAmt = float.Parse(tempResourceAmt);

                    //switch statement to call different scenes in the game based off of a readline string var from save file.
                    //TODO
                    break;

                case "3":

                    TypeWriter("Placeholder, devloped by Nate 'spekky' Hare.\nWritten in C# as a personal project to help learn the language.\nView the source code at 'github.com/spektrumm'.\n");
                    menuSelect();
                    break;

                default:

                    Console.WriteLine("Invalid Menu Selection, try again.");
                    menuSelect();
                    break;

            }
        }
        static void starterTown()
        {
            TypeWriter("You arrive at a town, with various townsfolk bustling about.\n");
            SlowType(90);
        }
        static void TypeWriter(string msg)
        {
            for (int count = 0; count < msg.Length; count++)
            {
                Console.Write(msg[count]);
                System.Threading.Thread.Sleep(20);
            }
        }
        static void SlowType(int dTime)
        {
            string dots = ". . .\n";
            for (int count = 0; count < dots.Length; count++)
            {
                Console.Write(dots[count]);
                System.Threading.Thread.Sleep(dTime);
            }
        }
        static void Awakening()
        {
            string gameBeginTitle = "Awakening";
            titleUpdate(gameBeginTitle);
            TypeWriter("You awaken in a tent, in the middle of a forest.\n");
            SlowType(60);
            TypeWriter("You smell the dew in the air, with the faint sound of a cart on a nearby road.\n");
            SlowType(90);
            System.Threading.Thread.Sleep(300);
            TypeWriter("What do you choose?\n1. Follow the noise\n2. Make a morning meal\n");
            System.Threading.Thread.Sleep(160);
            string awakeChoice = Console.ReadLine();
            if (awakeChoice == "1")
            {
                System.Threading.Thread.Sleep(300);
                TypeWriter("You arrive at the road, turn left or right?\n1. Left\n2. Right\n");
                System.Threading.Thread.Sleep(160);
                string roadChoice = Console.ReadLine();
                if (roadChoice == "1")
                {
                    System.Threading.Thread.Sleep(300);
                    starterTown(); //placeholder function to call the next operation
                    //Console.WriteLine("startertown function not written");
                }
                else if (roadChoice == "2")
                {
                    System.Threading.Thread.Sleep(300);
                    //huntingGrounds(); //placeholder function
                    Console.WriteLine("huntingGrounds function not written");
                }
                else if (roadChoice == "menu")
                {
                    menuCall(); //placeholder function to call menu
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid selection...\nPlease try again.");
                    Console.ResetColor();
                    Awakening();
                }
            }
            else if (awakeChoice == "2")
            {
                TypeWriter("Items consumed");
                SlowType(40);
                TypeWriter("You've made a morning meal: Chicken and Vegetable Skewer.\n");
                SlowType(60);
                TypeWriter("It has been added to your inventory");
                SlowType(40);
                // remove food items from inventory, chicken breast, potato, tomato - output vegetable skewer
                System.Threading.Thread.Sleep(300);
                TypeWriter("\nYou get up and decide to follow the noise.\nYou come to the road, which direction do you choose?\n1. Left\n2. Right\n");
                System.Threading.Thread.Sleep(300);
                string roadChoice = Console.ReadLine();
                if (roadChoice == "1")
                {
                    starterTown(); //placeholder function to call the next operation
                    //Console.WriteLine("startertown function not written");
                }
                else if (roadChoice == "2")
                {
                    //huntingGrounds(); //placeholder function
                    Console.WriteLine("huntingGrounds function not written");
                }
                else if (roadChoice == "menu" || roadChoice == "Menu")
                {
                    menuCall(); //placeholder function to call menu
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid selection...\nPlease try again.");
                    Console.ResetColor();
                    Awakening();
                }
            }
            else if (awakeChoice == "menu" || awakeChoice == "Menu")
            {
                menuCall();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid selection...\nPlease try again.");
                Console.ResetColor();
                Awakening();
            }
        }
    }
}