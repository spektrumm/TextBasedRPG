﻿using System;

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
        public string[] weapNamesMage = {"The Spark Stick", "Hand Warmer", "Spark Wand", "Amethyst Staff", "Kiekurakeppi", "Java Hell", "Boolin Boolean", "Eight", "Maximum Effort", "Cool Beans"};

        public Mage(string name, int randomNumber) {

            Mana = 10.0f;
            mXP = 0f;
            mageName = name;
            
            rngWeaponNameMage = weapNamesMage[randomNumber];

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
