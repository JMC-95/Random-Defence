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

    class Effect
    {
        public static int DeathSoul = 0;
        public static int RespawnCircle = 1;
        public static int CombineBlast = 2;
        public static int WarriorSlash = 3;
        public static int FighterSlash = 4;
        public static int PaladinSlash = 5;
        public static int SamuraiSlash = 6;
        public static int RuneKnightSlash = 7;
        public static int ValkyrieSlash = 8;
        public static int BerserkerSlash = 9;
        public static int BlackguardsSlash = 10;
        public static int CrusadersSlash = 11;
        public static int ThiefSlash = 12;
        public static int AssassinSlash = 13;
        public static int AvengerSlash = 14;
        public static int GrapplerSlash = 15;
        public static int MechanicFire = 16;
        public static int EngineerFire = 17;
        public static int WizardFire = 18;
        public static int PriestFire = 19;
        public static int SorcererFire = 20;
        public static int MonkFire = 21;
        public static int BardFire = 22;
        public static int BishopFire = 23;
        public static int ArcherFire = 24;
        public static int SniperFire = 25;
        public static int HunterFire = 26;
        public static int LancerSlash = 27;
        public static int SlayerSlash = 28;
        public static int DragonKnightSlash = 29;
        public static int Max = DragonKnightSlash + 1;

        public static string ToString(int effectType)
        {
            if (effectType == 0)
                return "DeathSoul";
            if (effectType == 1)
                return "RespawnCircle";
            if (effectType == 2)
                return "CombineBlast";
            if (effectType == 3)
                return "WarriorSlash";
            if (effectType == 4)
                return "FighterSlash";
            if (effectType == 5)
                return "PaladinSlash";
            if (effectType == 6)
                return "SamuraiSlash";
            if (effectType == 7)
                return "RuneKnightSlash";
            if (effectType == 8)
                return "ValkyrieSlash";
            if (effectType == 9)
                return "BerserkerSlash";
            if (effectType == 10)
                return "BlackguardsSlash";
            if (effectType == 11)
                return "CrusadersSlash";
            if (effectType == 12)
                return "ThiefSlash";
            if (effectType == 13)
                return "AssassinSlash";
            if (effectType == 14)
                return "AvengerSlash";
            if (effectType == 15)
                return "GrapplerSlash";
            if (effectType == 16)
                return "MechanicFire";
            if (effectType == 17)
                return "EngineerFire";
            if (effectType == 18)
                return "WizardFire";
            if (effectType == 19)
                return "PriestFire";
            if (effectType == 20)
                return "SorcererFire";
            if (effectType == 21)
                return "MonkFire";
            if (effectType == 22)
                return "BardFire";
            if (effectType == 23)
                return "BishopFire";
            if (effectType == 24)
                return "ArcherFire";
            if (effectType == 25)
                return "SniperFire";
            if (effectType == 26)
                return "HunterFire";
            if (effectType == 27)
                return "LancerSlash";
            if (effectType == 28)
                return "SlayerSlash";
            if (effectType == 29)
                return "DragonKnightSlash";

            return null;
        }
    }
}