using System;

namespace TextBasedRPG
{
    class charClass {
        public string classIn;
        public string userClass;
        //public string[] weapName = {""};
        public int numGenerator() {

            // random number generator
            int numSelect = 0;
            Random ranNum = new Random();
            numSelect = ranNum.Next(1,11);

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


            switch (userClass) {
                case "mage":
                    
                    int randomNum = numGenerator(); // call rng num method
                    Mage mage01 = new Mage(charName, randomNum); // call new mage constructor
                    
                    // print a line to the user describing the new Mage they have just created.
                    Console.WriteLine($"{mage01.mageName}, your new Mage, has just been created.\nThey have {mage01.Mana} mana points, and are using {mage01.rngWeaponNameMage} as their weapon of choice.");
                    
                    //write to file to save this data for use in the rest of the game.

                    break;
                case "rogue":
                    // call new random rogue weapon assign
                    //call new Rogue(); function w given name from charName
                    Console.WriteLine($"{charName}, your new Rogue, has just been created.");
                    break;
                case "duelist":
                    // call new random duelist weapon assign
                    //call new Duelist(); function w given name from charName
                    Console.WriteLine($"{charName}, your new Duelist, has just been created.");
                    break;
                case "ranger":
                    // call new random ranger weapon assign
                    //call new Ranger(); function w given name from charName
                    Console.WriteLine($"{charName}, your new Ranger, has just been created.");
                    break;
                default:
                    Console.WriteLine("That is not a valid class for your character, try again.");
                    selectClass();
                    break;
            }
        }
    }

    class Mage {
        
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
            "Programmer's Blessing", // special item (tooltip: "a tome of ancient knowledge w the words ethched into the cover: 'Lunnaya Vspyshka'... Wonder what that could mean.")
            "Magic Eight Ball",
            "Rune of Kanai",
            "Enchanted Icicle"};

        public Mage(string name, int randomNumber) {

            Mana = 10.0f;
            mXP = 0f;
            mageName = name;
            
            rngWeaponNameMage = weapNamesMage[randomNumber];

        }
    }

    class Rogue {
        
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

    class Ranger {
        
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

    class Duelist {
        
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
            
            string MenuTitle = "Main Menu";

            titleUpdate(MenuTitle);

            charClass userCharacter = new charClass();
            string cCreateTitle = "Character Creation";
            titleUpdate(cCreateTitle);
            userCharacter.selectClass();

            // open file and set variables for the users character stats here.
            string gameBeginTitle = "Awakening";
            titleUpdate(gameBeginTitle);
        
            Console.ReadKey();
        }
        static void titleUpdate(string newTitlePiece) {
                
                Console.Title = $"Placeholder - {newTitlePiece}";
            }
    }
}
