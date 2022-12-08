using System;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace BrotatoM
{
    public interface IPlayerSystem : ISystem
    {
        // 状态栏
        public BindableProperty<float> HP { get; }
        public BindableProperty<float> Exp { get; }

        public BindableProperty<float> CurrMaxExp { get; }
        public BindableProperty<int> Level { get; }
        /// <summary>
        /// 状态栏的收获
        /// </summary>
        /// <value></value>
        public BindableProperty<float> Harvest { get; }
        public BindableProperty<float> HarvestBag { get; }

        // 属性栏
        public BindableProperty<float> MaxHp { get; }
        public BindableProperty<float> HpRegeneration { get; }
        public BindableProperty<float> LifeSteal { get; }
        public BindableProperty<float> Damage { get; }
        public BindableProperty<float> MeleeDamage { get; }
        public BindableProperty<float> RangedDamage { get; }
        public BindableProperty<float> ElementalDamage { get; }
        public BindableProperty<float> AttackSpeed { get; }
        public BindableProperty<float> CritChance { get; }
        public BindableProperty<float> Engineering { get; }
        public BindableProperty<float> Range { get; }
        public BindableProperty<float> Armor { get; }
        public BindableProperty<float> Dodge { get; }
        public BindableProperty<float> Speed { get; }
        public BindableProperty<float> Luck { get; }
        /// <summary>
        /// 每回合收获
        /// </summary>
        /// <value></value>
        public BindableProperty<float> Harvesting { get; }

        // 隐藏属性
        public BindableProperty<float> EnemiesSpawnRate { get; }
        public BindableProperty<float> TreeSpawnRate { get; }
        public int CharacterId { get; set; }
        /// <summary>
        /// 升级点数，决定升级界面的显示与消失
        /// </summary>
        /// <value></value>
        public int UpgradePoint { get; set; }

        public BindableProperty<int> CurrWave { get; }
        public int DangerLevel { get; set; }
        public List<WeaponInfo> CurrWeapons { get; }
        public List<int> CurrItems { get; }
        public List<int> CurrLockIndices { get; }

        public void AddItem(int itemId);
        public bool HasItem(int itemId);
        public void AddWeapon(WeaponConfigItem weaponConfigItem, int rarity = 0);
        public bool CanBuyWeapon(string name, int rarity = 0);
        public void AddFloatValueByPropertyName(string name, float value);
        public BindableProperty<T> GetBindablePropertyByName<T>(string name);
        public bool IsPercentValue(string name);
    }

    public class PlayerSystem : AbstractSystem, IPlayerSystem
    {
        // 状态栏
        public BindableProperty<float> HP { get; } = new() { Value = 15 };
        public BindableProperty<float> Exp { get; } = new() { Value = 0 };
        public BindableProperty<float> CurrMaxExp { get; } = new() { Value = 20 };
        public BindableProperty<int> Level { get; } = new() { Value = 0 };
        public BindableProperty<float> Harvest { get; } = new() { Value = 30 };
        public BindableProperty<float> HarvestBag { get; } = new() { Value = 0 };

        // 属性栏
        public BindableProperty<float> MaxHp { get; } = new() { Value = 15 };
        public BindableProperty<float> HpRegeneration { get; } = new() { Value = 0 };
        public BindableProperty<float> LifeSteal { get; } = new() { Value = 0 };
        public BindableProperty<float> Damage { get; } = new() { Value = 0 };
        public BindableProperty<float> MeleeDamage { get; } = new() { Value = 0 };
        public BindableProperty<float> RangedDamage { get; } = new() { Value = 0 };
        public BindableProperty<float> ElementalDamage { get; } = new() { Value = 0 };
        public BindableProperty<float> AttackSpeed { get; } = new() { Value = 0 };
        public BindableProperty<float> CritChance { get; } = new() { Value = 0 };
        public BindableProperty<float> Engineering { get; } = new() { Value = 0 };
        public BindableProperty<float> Range { get; } = new() { Value = 0 };
        public BindableProperty<float> Armor { get; } = new() { Value = 0 };
        public BindableProperty<float> Dodge { get; } = new() { Value = 0 };
        public BindableProperty<float> Speed { get; } = new() { Value = 0 };
        public BindableProperty<float> Luck { get; } = new() { Value = 0 };
        public BindableProperty<float> Harvesting { get; } = new() { Value = 0 };

        // 隐藏属性
        public BindableProperty<float> EnemiesSpawnRate { get; } = new() { Value = 1 };
        public BindableProperty<float> TreeSpawnRate { get; } = new() { Value = 1 };
        public int CharacterId { get; set; }
        public int UpgradePoint { get; set; }

        private readonly List<WeaponInfo> mWeaponInfos = new();
        private readonly List<int> mItems = new();
        private readonly List<int> mLockIndices = new();
        public BindableProperty<int> CurrWave { get; set; } = new() { Value = 1 };
        public int DangerLevel { get; set; }
        public List<WeaponInfo> CurrWeapons => mWeaponInfos;
        public List<int> CurrItems => mItems;
        public List<int> CurrLockIndices => mLockIndices;

        private ItemConfigItem[] mItemConfigs;

        protected override void OnInit()
        {
            // 创建一个GO用于挂载TimeSystemUpdateBehaviour
            var updateBehaviourGO = new GameObject("GameManager");
            updateBehaviourGO.AddComponent<DontDestroyOnLoadScript>();
            mItemConfigs = this.GetModel<ItemConfigModel>().GetAllConfigItems();
        }

        /// <summary>
        /// 装备道具和角色
        /// 角色编号从152开始
        /// </summary>
        /// <param name="itemId">道具编号</param>
        public void AddItem(int itemId)
        {
            // 增加属性
            MaxHp.Value += mItemConfigs[itemId].MaxHp;
            HpRegeneration.Value += mItemConfigs[itemId].HpRegeneration;
            LifeSteal.Value += mItemConfigs[itemId].LifeSteal;
            Damage.Value += mItemConfigs[itemId].Damage;
            MeleeDamage.Value += mItemConfigs[itemId].MeleeDamage;
            RangedDamage.Value += mItemConfigs[itemId].RangedDamage;
            ElementalDamage.Value += mItemConfigs[itemId].ElementalDamage;
            AttackSpeed.Value += mItemConfigs[itemId].AttackSpeed;
            CritChance.Value += mItemConfigs[itemId].CritChance;
            Engineering.Value += mItemConfigs[itemId].Engineering;
            Range.Value += mItemConfigs[itemId].Range;
            Armor.Value += mItemConfigs[itemId].Armor;
            Dodge.Value += mItemConfigs[itemId].Dodge;
            Speed.Value += mItemConfigs[itemId].Speed;
            Luck.Value += mItemConfigs[itemId].Luck;
            Harvesting.Value += mItemConfigs[itemId].Harvesting;

            // 显示到道具栏
            mItems.Add(itemId);

            this.SendEvent<AddItemEvent>();
        }

        public void AddWeapon(WeaponConfigItem weaponConfigItem, int rarity = 0)
        {
            // TODO 武器升级
            WeaponInfo weaponInfo = new(weaponConfigItem, rarity);
            // 人物属性影响武器效果
            weaponInfo.CritChance += CritChance.Value;
            if (weaponInfo.CritChance > 1)
                weaponInfo.CritChance = 1;

            weaponInfo.Damage += weaponInfo.DamageModifier * MeleeDamage.Value;
            weaponInfo.Damage *= 1 + Damage.Value;

            weaponInfo.Range += Range.Value;
            weaponInfo.Lifesteal += LifeSteal.Value;

            // 武器上限
            CurrWeapons.Add(weaponInfo);
        }

        public bool CanBuyWeapon(string name, int rarity = 0)
        {
            if (CurrWeapons.Count < 6)
                return true;

            for (int i = 0; i < CurrWeapons.Count; i++)
            {
                if (CurrWeapons.Exists(x => x.Name == name))
                    return true;
            }

            return false;
        }

        public bool HasItem(int itemId)
        {
            return CurrItems.Contains(itemId);
        }

        public void AddFloatValueByPropertyName(string name, float value)
        {
            Type t = Type.GetType("BrotatoM.PlayerSystem");
            ((BindableProperty<float>)t.GetProperty(name).GetValue(this)).Value += value;
        }

        public BindableProperty<T> GetBindablePropertyByName<T>(string name)
        {
            Type t = Type.GetType("BrotatoM.PlayerSystem");
            return (BindableProperty<T>)t.GetProperty(name).GetValue(this);
        }

        public bool IsPercentValue(string name)
        {
            return Params.PERCENT_VALUES.Contains(name);
        }
    }
}
