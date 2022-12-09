using System.Collections.Generic;
using UnityEngine;

namespace BrotatoM
{
    public class UIColor
    {
        public static Color WHITE = new(1f, 1f, 1f, 0.8f);
        public static Color BLACK = new(0, 0, 0, 0.8f);
        public static Color GRAY = new(0.3f, 0.3f, 0.3f, 1f);
        public static Color DARKER_GRAY = new(0.15f, 0.15f, 0.15f, 1f);
        public static Color LOCKED = new(1f, 0.5f, 0, 1f);
    }

    public class Params
    {
        public static int ITEM_AMOUNT = 152;
        public static List<string> PERCENT_VALUES = new() {
            "Damage",
            "LifeSteal",
            "AttackSpeed",
            "CritChance",
            "Dodge",
            "Speed"
        };

        public static int[] WaveLastSeconds = new int[] {
            20, 20, 20, 30, 30,
            30, 30, 30, 30, 30,
            40, 40, 40, 50, 50,
            50, 50, 60, 60, 60
        };

        public static string UpgradeIconPath = "ArtAssets/Characters/60px-Mutant";

        public static float Harvest = 30;
        public static float MaxExp = 20;
        public static float MaxHp = 15;
        public static float HpRegeneration = 0;
        public static float LifeSteal = 0;
        public static float Damage = 0;
        public static float MeleeDamage = 0;
        public static float RangedDamage = 0;
        public static float ElementalDamage = 0;
        public static float AttackSpeed = 0;
        public static float CritChance = 0;
        public static float Engineering = 0;
        public static float Range = 0;
        public static float Armor = 0;
        public static float Dodge = 0;
        public static float Speed = 0;
        public static float Luck = 0;
        public static float Harvesting = 0;
        public static float EnemiesSpawnRate = 0;
        public static float TreeSpawnRate = 0;
    }
}