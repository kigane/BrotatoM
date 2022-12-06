using UnityEngine;

namespace BrotatoM
{
    public class ItemConfigItem : IConfigItem
    {
        public string Name { get; set; }
        public int Rarity { get; set; }
        public string Effects { get; set; }
        public int BasePrice { get; set; }
        public int Limit { get; set; }
        public string UnlockedBy { get; set; }
        public string Path { get; set; }
    }

    public class ItemConfigModel : BaseConfigModel<ItemConfigItem>
    {
        public ItemConfigModel(string path) : base(path)
        {
        }

        protected override void OnInit()
        {
            base.OnInit();
            // VerifyLogs("Exoskeleton");
            // VerifyLogs("Wheat");
        }

        private void VerifyLogs(string name)
        {
            string msg = "Items-" + name + ": (";
            ItemConfigItem item = mDict[name];
            msg += item.Name + ", ";
            msg += item.Rarity + ", ";
            msg += item.Effects + ", ";
            msg += item.Limit + ", ";
            msg += item.UnlockedBy + ", ";
            msg += item.Path + ")";
            Log.Debug(msg);
        }
    }
}
