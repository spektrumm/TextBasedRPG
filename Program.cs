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

            int randomNum = numGenerator(); // call rng num method

            switch (userClass) {
                case "mage":
                    Mage playerMage = new Mage(charName, randomNum); // call new mage constructor
                    
                    // print a line to the user describing the new Mage they have just created.
                    Console.WriteLine($"{playerMage.mageName}, your new Mage, has just been created.\nThey have {playerMage.Mana} mana points, and are using {playerMage.rngWeaponNameMage} as their weapon of choice.");
                    
                    //write to file to save this data for use in the rest of the game.

                    break;
                case "rogue":
                    Rogue playerRogue = new Rogue(charName, randomNum); // call new mage constructor
                    
                    // print a line to the user describing the new Mage they have just created.
                    Console.WriteLine($"{playerRogue.rogueName}, your new Rogue, has just been created.\nThey have {playerRogue.Stealth} stealth points, and are using {playerRogue.rngWeaponNameRogue} as their weapon of choice.");
                    
                    //write to file to save this data for use in the rest of the game.

                    break;
                case "duelist":
                    Duelist playerDuel = new Duelist(charName, randomNum); // call new mage constructor
                    
                    // print a line to the user describing the new Mage they have just created.
                    Console.WriteLine($"{playerDuel.duelName}, your new Duelist, has just been created.\nThey have {playerDuel.Strength} strength points, and are using {playerDuel.rngWeaponNameDuel} as their weapon of choice.");
                    
                    //write to file to save this data for use in the rest of the game.

                    break;
                case "ranger":
                    Ranger playerRanger = new Ranger(charName, randomNum); // call new mage constructor
                    
                    // print a line to the user describing the new Mage they have just created.
                    Console.WriteLine($"{playerRanger.rangerName}, your new Ranger, has just been created.\nThey have {playerRanger.Dexterity} dexterity points, and are using {playerRanger.rngWeaponNameRanger} as their weapon of choice.");
                    
                    //write to file to save this data for use in the rest of the game.

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
            "Programmer's Blessing", // special item (tooltip: "a tome of ancient knowledge with the words 'Lunnaya Vspyshka' ethched into the cover... Wonder what that could mean.")
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
