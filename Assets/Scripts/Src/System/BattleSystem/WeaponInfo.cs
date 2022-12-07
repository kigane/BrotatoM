namespace BrotatoM
{
    public class WeaponInfo
    {
        public string Name;
        public string[] Class;
        public string Path;
        public float Damage;
        public float DamageModifier;
        public float AttackSpeed;
        public float Range;
        public float Knockback;
        public float Lifesteal;
        public float CritChance;
        public float CritMultiplicator;
        public string SpecialEffects;

        public WeaponInfo() { }

        public WeaponInfo(WeaponConfigItem weaponConfigItem, int rarity)
        {
            Name = weaponConfigItem.Name;
            Class = weaponConfigItem.Class;
            Path = weaponConfigItem.Path;
            Damage = weaponConfigItem.Damage[rarity];
            DamageModifier = weaponConfigItem.DamageModifier[rarity];
            AttackSpeed = weaponConfigItem.AttackSpeed[rarity];
            Range = weaponConfigItem.Range[rarity];
            Knockback = weaponConfigItem.Knockback[rarity];
            Lifesteal = weaponConfigItem.Lifesteal[rarity];
            CritChance = weaponConfigItem.CritChance[rarity];
            CritMultiplicator = weaponConfigItem.CritMultiplicator[rarity];
            SpecialEffects = weaponConfigItem.SpecialEffects;
        }
    }
}
