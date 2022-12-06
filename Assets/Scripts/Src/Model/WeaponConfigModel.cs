using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace BrotatoM
{
    public class WeaponConfigItem : IConfigItem
    {
        public string Name { get; set; }
        public string[] Class { get; set; }
        public int[] Damage { get; set; }
        public float[] AttackSpeed { get; set; }
        public int[] Range { get; set; }
        public int[] Knockback { get; set; }
        public float[] Lifesteal { get; set; }
        public string SpecialEffects { get; set; }
        public int[] BasePrice { get; set; }
        public string UnlockedBy { get; set; }
        public string Path { get; set; }
        public float[] DamageModifier { get; set; }
        public float[] CritChance { get; set; }
        public float[] CritMultiplicator { get; set; }
    }

    public class WeaponConfigModel : BaseConfigModel<WeaponConfigItem>
    {
        public WeaponConfigModel(string path) : base(path)
        {
            // Log.Debug(mConfigPath);
        }

        protected override void OnInit()
        {
            base.OnInit();
            // VerifyLogs("Chopper");
        }

        private void VerifyLogs(string name)
        {
            string msg = "Weapon-" + name + ": (";
            WeaponConfigItem item = mDict[name];
            msg += item.Name + ", ";
            msg += item.Class[0] + ", ";
            msg += ArrLogMsg(item.AttackSpeed) + ", ";
            msg += ArrLogMsg(item.BasePrice) + ", ";
            msg += ArrLogMsg(item.Range) + ", ";
            msg += ArrLogMsg(item.Knockback) + ", ";
            msg += ArrLogMsg(item.Damage) + ", ";
            msg += ArrLogMsg(item.Lifesteal) + ", ";
            msg += ArrLogMsg(item.BasePrice) + ", ";
            msg += ArrLogMsg(item.DamageModifier) + ", ";
            msg += ArrLogMsg(item.CritChance) + ", ";
            msg += ArrLogMsg(item.CritMultiplicator) + ", ";
            msg += item.SpecialEffects + ", ";
            msg += item.Path + ")";
            Log.Debug(msg);
        }
    }
}
