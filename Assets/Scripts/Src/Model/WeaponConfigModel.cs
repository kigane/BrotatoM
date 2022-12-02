using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace BrotatoM
{
    public interface IWeaponConfigModel : IModel
    {
        WeaponConfigItem GetWeaponConfigByName(string name);
    }

    public class WeaponConfigItem : IConfigItem
    {
        public string Name { get; set; }
        public string[] Class { get; set; }
        public int[] Damage { get; set; }
        public float[] AttackSpeed { get; set; }
        public int[] Range { get; set; }
        public int[] Knockback { get; set; }
        public float[] Lifesteal { get; set; }
        public string SpacialEffects { get; set; }
        public int[] BasePrice { get; set; }
        public string UnlockedBy { get; set; }
        public string Path { get; set; }
        public float[] DamageModifier { get; set; }
        public float[] CritChance { get; set; }
        public float[] CritMultiplicator { get; set; }
    }

    public class WeaponConfigModel0 : AbstractModel, IWeaponConfigModel
    {
        private readonly Dictionary<string, WeaponConfigItem> mWeaponItems = new();

        protected override void OnInit()
        {
            this.GetUtility<IJsonSerializer>().ReadJsonToDictionary("Configs/ProcessedWeapons", mWeaponItems);
            // VerifyLogs(name);
        }

        public WeaponConfigItem GetWeaponConfigByName(string name)
        {
            return mWeaponItems[name];
        }

        private void VerifyLogs(string name)
        {
            Debug.Log(mWeaponItems[name].Name);
            Debug.Log(mWeaponItems[name].Class[0]);
            Debug.Log(mWeaponItems[name].Class[1]);
            LogArr4(mWeaponItems[name].AttackSpeed);
            LogArr4(mWeaponItems[name].BasePrice);
            LogArr4(mWeaponItems[name].Range);
            LogArr4(mWeaponItems[name].Knockback);
            LogArr4(mWeaponItems[name].Damage);
            LogArr4(mWeaponItems[name].Lifesteal);
            LogArr4(mWeaponItems[name].BasePrice);
            LogArr4(mWeaponItems[name].DamageModifier);
            LogArr4(mWeaponItems[name].CritChance);
            LogArr4(mWeaponItems[name].CritMultiplicator);
            Debug.Log(mWeaponItems[name].Path);
        }

        private void LogArr4<T>(T[] arr)
        {
            Debug.Log(arr[0] + ", " + arr[1] + ", " + arr[2] + ", " + arr[3]);
        }
    }

    public class WeaponConfigModel : ConfigModel<WeaponConfigItem>
    {
        public WeaponConfigModel(string path) : base(path)
        {
            mConfigPath = path;
            Debug.Log(mConfigPath);
        }

        protected override void OnInit()
        {
            base.OnInit();
            // VerifyLogs("Chopper");
        }

        private void VerifyLogs(string name)
        {
            WeaponConfigItem item = mDict[name];
            Debug.Log(item.Name);
            Debug.Log(item.Class[0]);
            LogArr4(item.AttackSpeed);
            LogArr4(item.BasePrice);
            LogArr4(item.Range);
            LogArr4(item.Knockback);
            LogArr4(item.Damage);
            LogArr4(item.Lifesteal);
            LogArr4(item.BasePrice);
            LogArr4(item.DamageModifier);
            LogArr4(item.CritChance);
            LogArr4(item.CritMultiplicator);
            Debug.Log(item.Path);
        }

        private void LogArr4<T>(T[] arr)
        {
            Debug.Log(arr[0] + ", " + arr[1] + ", " + arr[2] + ", " + arr[3]);
        }
    }
}
