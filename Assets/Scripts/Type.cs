using System;

namespace Type
{
    class Character
    {
        public static int Max = 27;

        public static string ToString(int characterType)
        {
            if (characterType == 0)
                return "Warrior";
            if (characterType == 1)
                return "Archer";
            if (characterType == 2)
                return "Wizard";
            if (characterType == 3)
                return "Fighter";
            if (characterType == 4)
                return "Lancer";
            if (characterType == 5)
                return "Monk";
            if (characterType == 6)
                return "Thief";
            if (characterType == 7)
                return "Mechanic";
            if (characterType == 8)
                return "Priest";
            if (characterType == 9)
                return "Engineer";
            if (characterType == 10)
                return "Samurai";
            if (characterType == 11)
                return "Assassin";
            if (characterType == 12)
                return "Paladin";
            if (characterType == 13)
                return "Bard";
            if (characterType == 14)
                return "Sorcerer";
            if (characterType == 15)
                return "Sniper";
            if (characterType == 16)
                return "Avenger";
            if (characterType == 17)
                return "Valkyrie";
            if (characterType == 18)
                return "Grappler";
            if (characterType == 19)
                return "Slayer";
            if (characterType == 20)
                return "Hunter";
            if (characterType == 21)
                return "Berserker";
            if (characterType == 22)
                return "RuneKnight";
            if (characterType == 23)
                return "Crusaders";
            if (characterType == 24)
                return "Blackguards";
            if (characterType == 25)
                return "Bishop";
            if (characterType == 26)
                return "DragonKnight";

            return null;
        }
    }

    class Monster
    {
        public static int Goblin = 0;
        public static int GoblinWarrior = 1;
        public static int GoblinMage = 2;
        public static int Golem = 3;
        public static int WoodGolem = 4;
        public static int IronGolem = 5;
        public static int IceGolem = 6;
        public static int Kerberos = 7;
        public static int Minotauros = 8;
        public static int Troll = 9;
        public static int Dragon = 10;
        public static int Max = Dragon + 1;

        public static string ToString(int monsterType)
        {
            if (monsterType == 0)
                return "ch101";
            if (monsterType == 1)
                return "ch102";
            if (monsterType == 2)
                return "ch103";
            if (monsterType == 3)
                return "ch104";
            if (monsterType == 4)
                return "ch105";
            if (monsterType == 5)
                return "ch106";
            if (monsterType == 6)
                return "ch107";
            if (monsterType == 7)
                return "ch108";
            if (monsterType == 8)
                return "ch109";
            if (monsterType == 9)
                return "ch110";
            if (monsterType == 10)
                return "ch111";

            return null;
        }
    }
}