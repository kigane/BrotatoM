using System;
using System.Reflection;
using QFramework;

namespace BrotatoM
{
    public struct AttrInfo
    {
        public string path;
        public string name;
        public float value;
    }

    public class NeedShowPropertiesQuery : AbstractQuery<AttrInfo[]>
    {
        private AttrInfo[] properties = new AttrInfo[16];
        private string[] attrs = new string[]{
            "MaxHp",
            "HpRegeneration",
            "LifeSteal",
            "Damage",
            "MeleeDamage",
            "RangedDamage",
            "ElementalDamage",
            "AttackSpeed",
            "CritChance",
            "Engineering",
            "Range",
            "Armor",
            "Dodge",
            "Speed",
            "Luck",
            "Harvesting",
        };

        protected override AttrInfo[] OnDo()
        {
            var playerModel = this.GetModel<IPlayerModel>();
            var statsModel = this.GetModel<StatConfigModel>();
            Type t = Type.GetType("BrotatoM.PlayerModel", true);
            for (int i = 0; i < attrs.Length; i++)
            {
                properties[i].name = attrs[i];
                properties[i].value = ((BindableProperty<float>)t.GetProperty(attrs[i]).GetValue(playerModel)).Value;
                properties[i].path = statsModel.GetConfigItemByName(attrs[i]).Path;
            }
            return properties;
        }
    }
}
