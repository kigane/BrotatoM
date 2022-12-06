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
        public BindableProperty<int> CharacterId { get; }
        /// <summary>
        /// 升级点数，决定升级界面的显示与消失
        /// </summary>
        /// <value></value>
        public BindableProperty<int> UpgradePoint { get; }

        public BindableProperty<int> CurrWave { get; }
        public int DangerLevel { get; set; }
        public WeaponInfo[] CurrWeapons { get; }
        public List<int> CurrItems { get; }

        public void AddItem(int itemId);
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
        public BindableProperty<int> CharacterId { get; } = new() { Value = 0 };
        public BindableProperty<int> UpgradePoint { get; } = new() { Value = 0 };

        private readonly WeaponInfo[] mWeaponInfos = new WeaponInfo[6];
        private readonly List<int> mItems = new();
        public BindableProperty<int> CurrWave { get; } = new BindableProperty<int>()
        {
            Value = 1
        };
        public int DangerLevel { get; set; }
        public WeaponInfo[] CurrWeapons => mWeaponInfos;
        public List<int> CurrItems => mItems;

        protected override void OnInit()
        {
            // 创建一个GO用于挂载TimeSystemUpdateBehaviour
            var updateBehaviourGO = new GameObject("GameManager");
            updateBehaviourGO.AddComponent<DontDestroyOnLoadScript>();
        }

        // 装备道具
        public void AddItem(int itemId)
        {
            // 增加属性

            // 显示到道具栏
            mItems.Add(itemId);
        }

        public bool HasItem(int itemId)
        {
            return mItems.Contains(itemId);
        }
    }
}
