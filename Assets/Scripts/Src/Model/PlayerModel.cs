using QFramework;

namespace BrotatoM
{
    public interface IPlayerModel : IModel
    {
        // 状态栏
        public BindableProperty<int> HP { get; }
        public BindableProperty<int> Exp { get; }

        public BindableProperty<int> CurrMaxExp { get; }
        public BindableProperty<int> Level { get; }
        public BindableProperty<int> Harvest { get; }
        public BindableProperty<int> HarvestBag { get; }
        // 属性栏
        public BindableProperty<int> MaxHP { get; }
        public BindableProperty<int> HPRegeneration { get; }
        public BindableProperty<int> LifeSteal { get; }
        public BindableProperty<int> Damage { get; }
        public BindableProperty<int> MeleeDamage { get; }
        public BindableProperty<int> RangedDamage { get; }
        public BindableProperty<int> ElementalDamage { get; }
        public BindableProperty<int> AttackSpeed { get; }
        public BindableProperty<int> CritChance { get; }
        public BindableProperty<int> EngineeringStat { get; }
        public BindableProperty<int> RangeStat { get; }
        public BindableProperty<int> Armor { get; }
        public BindableProperty<int> DodgeStat { get; }
        public BindableProperty<int> SpeedStat { get; }
        public BindableProperty<int> Luck { get; }
        public BindableProperty<int> Harvesting { get; }
        // 隐藏属性
        public BindableProperty<int> EnemiesSpawnRate { get; }
        public BindableProperty<int> TreeSpawnRate { get; }
        public BindableProperty<int> CharacterId { get; }
    }

    public class PlayerModel : AbstractModel, IPlayerModel
    {
        // 状态栏
        public BindableProperty<int> HP { get; } = new() { Value = 15 };
        public BindableProperty<int> Exp { get; } = new() { Value = 0 };
        public BindableProperty<int> CurrMaxExp { get; } = new() { Value = 20 };
        public BindableProperty<int> Level { get; } = new() { Value = 0 };
        public BindableProperty<int> Harvest { get; } = new() { Value = 30 };
        public BindableProperty<int> HarvestBag { get; } = new() { Value = 0 };
        // 属性栏
        public BindableProperty<int> MaxHP { get; } = new() { Value = 15 };
        public BindableProperty<int> HPRegeneration { get; } = new() { Value = 0 };
        public BindableProperty<int> LifeSteal { get; } = new() { Value = 0 };
        public BindableProperty<int> Damage { get; } = new() { Value = 0 };
        public BindableProperty<int> MeleeDamage { get; } = new() { Value = 0 };
        public BindableProperty<int> RangedDamage { get; } = new() { Value = 0 };
        public BindableProperty<int> ElementalDamage { get; } = new() { Value = 0 };
        public BindableProperty<int> AttackSpeed { get; } = new() { Value = 0 };
        public BindableProperty<int> CritChance { get; } = new() { Value = 0 };
        public BindableProperty<int> EngineeringStat { get; } = new() { Value = 0 };
        public BindableProperty<int> RangeStat { get; } = new() { Value = 0 };
        public BindableProperty<int> Armor { get; } = new() { Value = 0 };
        public BindableProperty<int> DodgeStat { get; } = new() { Value = 0 };
        public BindableProperty<int> SpeedStat { get; } = new() { Value = 0 };
        public BindableProperty<int> Luck { get; } = new() { Value = 0 };
        public BindableProperty<int> Harvesting { get; } = new() { Value = 0 };
        // 隐藏属性
        public BindableProperty<int> EnemiesSpawnRate { get; } = new() { Value = 1 };
        public BindableProperty<int> TreeSpawnRate { get; } = new() { Value = 1 };
        public BindableProperty<int> CharacterId { get; } = new() { Value = 0 };

        protected override void OnInit()
        {

        }
    }
}
