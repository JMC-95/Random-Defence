using System;

namespace Type
{
    class Character
    {
        public static int Farmer = 0;
        public static int Knight = 1;
        public static int Max = Knight + 1;

        public static string ToString(int monsterType)
        {
            if (monsterType == 0)
                return "ch001";
            if (monsterType == 1)
                return "ch005";

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