using QFramework;

namespace BrotatoM
{
    public interface IPlayerModel : IModel
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
        public BindableProperty<float> MaxHP { get; }
        public BindableProperty<float> HPRegeneration { get; }
        public BindableProperty<float> LifeSteal { get; }
        public BindableProperty<float> Damage { get; }
        public BindableProperty<float> MeleeDamage { get; }
        public BindableProperty<float> RangedDamage { get; }
        public BindableProperty<float> ElementalDamage { get; }
        public BindableProperty<float> AttackSpeed { get; }
        public BindableProperty<float> CritChance { get; }
        public BindableProperty<float> EngineeringStat { get; }
        public BindableProperty<float> RangeStat { get; }
        public BindableProperty<float> Armor { get; }
        public BindableProperty<float> DodgeStat { get; }
        public BindableProperty<float> SpeedStat { get; }
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
    }

    public class PlayerModel : AbstractModel, IPlayerModel
    {
        // 状态栏
        public BindableProperty<float> HP { get; } = new() { Value = 15 };
        public BindableProperty<float> Exp { get; } = new() { Value = 0 };
        public BindableProperty<float> CurrMaxExp { get; } = new() { Value = 20 };
        public BindableProperty<int> Level { get; } = new() { Value = 0 };
        public BindableProperty<float> Harvest { get; } = new() { Value = 30 };
        public BindableProperty<float> HarvestBag { get; } = new() { Value = 0 };
        // 属性栏
        public BindableProperty<float> MaxHP { get; } = new() { Value = 15 };
        public BindableProperty<float> HPRegeneration { get; } = new() { Value = 0 };
        public BindableProperty<float> LifeSteal { get; } = new() { Value = 0 };
        public BindableProperty<float> Damage { get; } = new() { Value = 0 };
        public BindableProperty<float> MeleeDamage { get; } = new() { Value = 0 };
        public BindableProperty<float> RangedDamage { get; } = new() { Value = 0 };
        public BindableProperty<float> ElementalDamage { get; } = new() { Value = 0 };
        public BindableProperty<float> AttackSpeed { get; } = new() { Value = 0 };
        public BindableProperty<float> CritChance { get; } = new() { Value = 0 };
        public BindableProperty<float> EngineeringStat { get; } = new() { Value = 0 };
        public BindableProperty<float> RangeStat { get; } = new() { Value = 0 };
        public BindableProperty<float> Armor { get; } = new() { Value = 0 };
        public BindableProperty<float> DodgeStat { get; } = new() { Value = 0 };
        public BindableProperty<float> SpeedStat { get; } = new() { Value = 0 };
        public BindableProperty<float> Luck { get; } = new() { Value = 0 };
        public BindableProperty<float> Harvesting { get; } = new() { Value = 0 };
        // 隐藏属性
        public BindableProperty<float> EnemiesSpawnRate { get; } = new() { Value = 1 };
        public BindableProperty<float> TreeSpawnRate { get; } = new() { Value = 1 };
        public BindableProperty<int> CharacterId { get; } = new() { Value = 0 };

        protected override void OnInit()
        {

        }
    }
}
