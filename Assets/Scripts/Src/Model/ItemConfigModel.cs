using UnityEngine;

namespace BrotatoM
{
    public class ItemConfigItem : IConfigItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rarity { get; set; }
        public string Effects { get; set; }
        public int BasePrice { get; set; }
        public int Limit { get; set; }
        public string UnlockedBy { get; set; }
        public string Path { get; set; }
        public float MaxHp { get; set; }
        public float HpRegeneration { get; set; }
        public float LifeSteal { get; set; }
        public float Damage { get; set; }
        public float MeleeDamage { get; set; }
        public float RangedDamage { get; set; }
        public float ElementalDamage { get; set; }
        public float AttackSpeed { get; set; }
        public float CritChance { get; set; }
        public float Engineering { get; set; }
        public float Range { get; set; }
        public float Armor { get; set; }
        public float Dodge { get; set; }
        public float Speed { get; set; }
        public float Luck { get; set; }
        public float Harvesting { get; set; }
    }

    public class ItemConfigModel : BaseConfigModel<ItemConfigItem>
    {
        public ItemConfigModel(string path) : base(path)
        {
        }

        protected override void OnInit()
        {
            base.OnInit();
            VerifyLogs("Exoskeleton");
            VerifyLogs(mItems[Params.ITEM_AMOUNT].Name);
        }

        public ItemConfigItem[] GetAllBuyableItems()
        {
            ItemConfigItem[] buyableItems = new ItemConfigItem[Params.ITEM_AMOUNT];
            for (int i = 0; i < Params.ITEM_AMOUNT; i++)
            {
                buyableItems[i] = mItems[i];
            }
            return buyableItems;
        }

        private void VerifyLogs(string name)
        {
            string msg = "Items-" + name + ": (";
            ItemConfigItem item = mDict[name];
            msg += item.Id + ", ";
            msg += item.Name + ", ";
            msg += item.Rarity + ", ";
            msg += item.Effects + ", ";
            msg += GetMessage(item.MaxHp);
            msg += GetMessage(item.HpRegeneration);
            msg += GetMessage(item.LifeSteal);
            msg += GetMessage(item.Damage);
            msg += GetMessage(item.MeleeDamage);
            msg += GetMessage(item.RangedDamage);
            msg += GetMessage(item.ElementalDamage);
            msg += GetMessage(item.AttackSpeed);
            msg += GetMessage(item.CritChance);
            msg += GetMessage(item.Engineering);
            msg += GetMessage(item.Range);
            msg += GetMessage(item.Armor);
            msg += GetMessage(item.Dodge);
            msg += GetMessage(item.Speed);
            msg += GetMessage(item.Luck);
            msg += GetMessage(item.Harvesting);
            msg += item.Limit + ", ";
            msg += item.UnlockedBy + ", ";
            msg += item.Path + ")";
            Log.Debug(msg);
        }

        private string GetMessage(float a)
        {
            if (a == 0)
                return "";
            else
                return a.ToString() + " ";
        }
    }
}
