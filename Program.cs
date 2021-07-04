using System;

namespace TextBasedRPG
{
    class charClass {

        public string name;
        public string classIn;
        public string userClass;
        private float experience;
        private float resource;
        public string rngWeaponName;
        public string[] eqWeapon = {" "};

        public string[] weapName = {""};
        
        //public string rngWeapon(string rngWeaponName) {}
        

        public void Mage(string charName) {

            // random number generator
            int numSelect = 0;
            Random ranNum = new Random();
            numSelect = ranNum.Next(1,11);

            experience = 0f; // set char xp to 0
            rngWeaponName = weapName[numSelect]; // taking random number to index a list of pre generated weapon names for starter weapon

            name = charName;
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
                    // call new random mage weapon assign
                    //call new Mage(); function w given name from charName
                    Console.WriteLine($"{charName}, your new Mage, has just been created.");
                    break;
                case "rogue":
                    // call new random rogue weapon assign
                    //call new Rogue(); function w given name from charName
                    break;
                case "duelist":
                    // call new random duelist weapon assign
                    //call new Duelist(); function w given name from charName
                    break;
                case "ranger":
                    // call new random ranger weapon assign
                    //call new Ranger(); function w given name from charName
                    break;
                default:
                    Console.WriteLine("That is not a valid class for your character, try again.");
                    selectClass();
                    break;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            charClass userCharacter = new charClass();
            userCharacter.selectClass();

            Console.ReadKey();
        }
    }
}
