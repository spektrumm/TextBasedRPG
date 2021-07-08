using System;
using System.Text.Json;
using System.IO;
using System.Collections;
using Microsoft.Win32;
using System.Text.Json.Serialization;

namespace TextBasedRPG
{
    class saveGame {

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



        
        public saveGame(string name, float xp, float resource, string weapon, string scene, string _class) {

            playerName = name;
            playerExp = xp;
            resourceAmt = resource;
            weaponName = weapon;
            currentScene = scene;
            playerClass = _class;
        }
        

        public static void writeJson(string name, float xp, float resource, string weapon, string scene, string _class) {
            
            var saveFile = new saveGame(name, xp, resource, weapon, scene, _class) {

                playerName = name,
                playerExp = xp,
                resourceAmt = resource,
                weaponName = weapon,
                currentScene = scene,
                playerClass = _class

            };

            string fileName = $"{name}-{scene}-{_class}-SaveGame.json";
            //var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(saveFile); // create the json data string
            //string jsonString = JsonSerializer.Serialize(saveFile, options); // create the json data string
            
            File.WriteAllText(fileName, jsonString); // write the data to a file and name it accordingly
            Console.WriteLine(File.ReadAllText(fileName)); // for debug purposes only

            // move file to saves folder
            string sourceFile = System.IO.Path.Combine(mainPath, fileName);
            string destFile = System.IO.Path.Combine(savesPath, fileName);
            System.IO.File.Move(sourceFile, destFile, true);
            
        }

    }

    class LoadGame {
        public string PlayerName { get; set; }
        public float PlayerExp { get; set; }
        public string CurrentScene { get; set; }
        public string WeaponName { get; set; }
        public float ResourceAmt { get; set; }
        public string PlayerClass { get; set; }

        public static string mainPath = @"C:\Users\natha\Documents\TextBasedRPG";
        public static string savesPath = @"C:\Users\natha\Documents\TextBasedRPG\saves";

        public LoadGame(string playerName, float playerExp, float resourceAmt, string weaponName, string currentScene, string playerClass) {

            PlayerName = playerName;
            PlayerExp = playerExp;
            ResourceAmt = resourceAmt;
            WeaponName = weaponName;
            CurrentScene = currentScene;
            PlayerClass = playerClass;
        }

        public static string[] readJson() {
            
            

            if (System.IO.Directory.Exists(savesPath)) {
                
                
                
                // need a way to index the strings being printed so only the name of the file is displayed, not the entire path.
                string[] dirFiles = System.IO.Directory.GetFiles(savesPath);
                int i = 0;
                foreach (string s in dirFiles) {
                    Console.WriteLine($"{i+1}. {dirFiles[i]}");
                    i++;
                }

                Console.WriteLine("Enter the number of the save file you'd like to load:\n");
                
                // take user input to select the file and index accordingly
                int saveFileChosen = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine(saveFileChosen);
                int indexValue = saveFileChosen - 1;
                string tempFileName = dirFiles[indexValue]; 
                string fileName = tempFileName.Remove(0,44); // temporary hard coded file pathname trimming
                Console.WriteLine(fileName);

                // move the save file to main folder so it can be accessed.
                string sourceFile = System.IO.Path.Combine(savesPath, fileName);
                string destFile = System.IO.Path.Combine(mainPath, fileName);

                System.IO.File.Move(sourceFile, destFile, true);

                string jsonString = File.ReadAllText(fileName);
                LoadGame loadGame = JsonSerializer.Deserialize<LoadGame>(jsonString); // returns null values if its not the float values, and returns 0 for the floats despite their values, why?

                // C:\Users\natha\Documents\TextBasedRPG\saves
                
                

                string playerName = loadGame.PlayerName;
                float playerExp = loadGame.PlayerExp;
                string currentScene = loadGame.CurrentScene;
                string weaponName = loadGame.WeaponName;
                float resourceAmt = loadGame.ResourceAmt;
                string playerClass = loadGame.PlayerClass;

                //Console.WriteLine(playerName, playerExp, currentScene, weaponName, resourceAmt, playerClass); for debug

                string[] loadedFile = new string[6];
                loadedFile[0] = playerName;
                loadedFile[1] = Convert.ToString(playerExp);
                loadedFile[2] = currentScene;
                loadedFile[3] = weaponName;
                loadedFile[4] = Convert.ToString(resourceAmt);
                loadedFile[5] = playerClass;

                //Console.WriteLine(loadedFile); for debug
                // move the file back to the saves folder
                System.IO.File.Move(destFile, sourceFile, true);
                return loadedFile;
            }
            else {
                Console.WriteLine($"Error, Save folder path: {savesPath} does not exist.");
                return null;
            }
        }
        /*
        public class jsonDataConverter : JsonCreationConverter<LoadGame> {
            protected override LoadGame Create(Type objectType, JObject jObject)
            {
                if (FieldExists("playerName", jObject)) {
                    return new playerName();
                }

                
            }
        }

        public abstract class JsonCreationConverter<T> : JsonConverter
        {
        /// <summary>
        /// Create an instance of objectType, based properties in the JSON object
        /// </summary>
        /// <param name="objectType">type of object expected</param>
        /// <param name="jObject">
        /// contents of JSON object that will be deserialized
        /// </param>
        /// <returns></returns>
        protected abstract T Create(Type objectType, JObject jObject);

        public override bool CanConvert(Type objectType)
        {
            return typeof(T).IsAssignableFrom(objectType);
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override object ReadJson(JsonReader reader, 
                                    Type objectType, 
                                     object existingValue, 
                                     JsonSerializer serializer)
        {
            // Load JObject from stream
            JObject jObject = JObject.Load(reader);

            // Create target object based on JObject
            T target = Create(objectType, jObject);

            // Populate the object properties
            serializer.Populate(jObject.CreateReader(), target);

            return target;
        }
        }
    */
    }
    
    public class charClass {
        public string classIn;
        public string userClass;
        //public string[] weapName = {""};
        public int numGenerator() {

            // random number generator
            int numSelect = 0;
            Random ranNum = new Random();
            numSelect = ranNum.Next(1,10);

            return numSelect;

            
        }
        public void selectClass() {

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

            switch (userClass) {
                case "mage":
                    Mage playerMage = new Mage(charName, randomNum); // call new mage constructor
                    
                    // print a line to the user describing the new Mage they have just created.
                    Console.WriteLine($"{playerMage.mageName}, your new Mage, has just been created.\nThey have {playerMage.Mana} mana points, and are using {playerMage.rngWeaponNameMage} as their weapon of choice.");
                    
                    //write to file to save this data for use in the rest of the game.
                    string mageClassID = "Mage";
                    saveGame mageInitialSave = new saveGame(playerMage.mageName, playerMage.mXP, playerMage.Mana, playerMage.rngWeaponNameMage, startingScene, mageClassID);
                    saveGame.writeJson(playerMage.mageName, playerMage.mXP, playerMage.Mana, playerMage.rngWeaponNameMage, startingScene, mageClassID);

                    break;
                case "rogue":
                    Rogue playerRogue = new Rogue(charName, randomNum); // call new mage constructor
                    
                    // print a line to the user describing the new Mage they have just created.
                    Console.WriteLine($"{playerRogue.rogueName}, your new Rogue, has just been created.\nThey have {playerRogue.Stealth} stealth points, and are using {playerRogue.rngWeaponNameRogue} as their weapon of choice.");
                    
                    //write to file to save this data for use in the rest of the game.
                    string roClassID = "Rogue";
                    saveGame roInitialSave = new saveGame(playerRogue.rogueName, playerRogue.roXP, playerRogue.Stealth, playerRogue.rngWeaponNameRogue, startingScene, roClassID);
                    saveGame.writeJson(playerRogue.rogueName, playerRogue.roXP, playerRogue.Stealth, playerRogue.rngWeaponNameRogue, startingScene, roClassID);

                    break;
                case "duelist":
                    Duelist playerDuel = new Duelist(charName, randomNum); // call new mage constructor
                    
                    // print a line to the user describing the new Mage they have just created.
                    Console.WriteLine($"{playerDuel.duelName}, your new Duelist, has just been created.\nThey have {playerDuel.Strength} strength points, and are using {playerDuel.rngWeaponNameDuel} as their weapon of choice.");
                    
                    //write to file to save this data for use in the rest of the game.
                    string duelClassID = "Duelist";
                    saveGame rogueInitialSave = new saveGame(playerDuel.duelName, playerDuel.dXP, playerDuel.Strength, playerDuel.rngWeaponNameDuel, startingScene, duelClassID);
                    saveGame.writeJson(playerDuel.duelName, playerDuel.dXP, playerDuel.Strength, playerDuel.rngWeaponNameDuel, startingScene, duelClassID);

                    break;
                case "ranger":
                    Ranger playerRanger = new Ranger(charName, randomNum); // call new mage constructor
                    
                    // print a line to the user describing the new Mage they have just created.
                    Console.WriteLine($"{playerRanger.rangerName}, your new Ranger, has just been created.\nThey have {playerRanger.Dexterity} dexterity points, and are using {playerRanger.rngWeaponNameRanger} as their weapon of choice.");
                    
                    //write to file to save this data for use in the rest of the game.
                    string raClassID = "Ranger";
                    saveGame rangerInitialSave = new saveGame(playerRanger.rangerName, playerRanger.raXP, playerRanger.Dexterity, playerRanger.rngWeaponNameRanger, startingScene, raClassID);

                    break;
                default:
                    Console.WriteLine("That is not a valid class for your character, try again.");
                    selectClass();
                    break;
            }
        }
    }

    public class Mage {
        
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

        public Mage(string name, int randomNumber) {

            Mana = 10.0f;
            mXP = 0f;
            mageName = name;
            
            rngWeaponNameMage = weapNamesMage[randomNumber];

        }
    }

    public class Rogue {
        
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

        public Rogue(string name, int randomNumber) {

            Stealth = 10.0f;
            roXP = 0f;
            rogueName = name;
            
            rngWeaponNameRogue = weapNamesRogue[randomNumber];

        }
    }

    public class Ranger {
        
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

        public Ranger(string name, int randomNumber) {

            Dexterity = 10.0f;
            raXP = 0f;
            rangerName = name;
            
            rngWeaponNameRanger = weapNamesRanger[randomNumber];

        }
    }

    public class Duelist {
        
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

        public Duelist(string name, int randomNumber) {

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
        static void titleUpdate(string newTitlePiece) {
                
                Console.Title = $"Placeholder - {newTitlePiece}";
            }
        static void menuSelect() {
            Console.WriteLine("Welcome to placeholder.\n\nNew Adventure\nLoad Adventure\nCredits");
        
            string userChoice = Console.ReadLine().ToLower();

            switch (userChoice) {
                case "new adventure":
                    charClass userCharacter = new charClass();
                    string cCreateTitle = "Character Creation";
                    titleUpdate(cCreateTitle);
                    userCharacter.selectClass();

                    Awakening();

                    break;
                case "load adventure":

                    // print save file directory filenames and have user select a save file
                    string[] loadedFileData = LoadGame.readJson();

                    Console.WriteLine(loadedFileData);
                    //switch statement to call different scenes in the game based off of a readline string var from save file.

                    break;
                case "credits":
                    
                    Console.WriteLine("Placeholder, devloped by Nate 'spekky' Hare.\nWritten in C# as a personal project to help learn the language.\nView the source code at 'github.com/spektrumm'.\n");
                    menuSelect();
                    break;
                default:
                    
                    Console.WriteLine("Invalid Menu Selection, try again.");
                    menuSelect();

                    break;
            }
        }
        static void Awakening() {
            string gameBeginTitle = "Awakening";
            titleUpdate(gameBeginTitle);
        }
    }
}