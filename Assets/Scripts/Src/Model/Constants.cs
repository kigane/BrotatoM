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
    }
}