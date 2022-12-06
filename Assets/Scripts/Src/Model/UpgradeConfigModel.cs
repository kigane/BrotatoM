using UnityEngine;

namespace BrotatoM
{
    public class UpgradeConfigItem : IConfigItem
    {
        public string Name { get; set; }
        public int Rarity { get; set; }
        public string Ability { get; set; }
        public float Value { get; set; }
        public string Path { get; set; }
    }

    public class UpgradeConfigModel : BaseConfigModel<UpgradeConfigItem>
    {
        public UpgradeConfigModel(string path) : base(path)
        {
            mConfigPath = path;
        }

        protected override void OnInit()
        {
            base.OnInit();
            // VerifyLogs("Back_I");
        }

        private void VerifyLogs(string name)
        {
            string msg = "Upgrades-" + name + ": (";
            UpgradeConfigItem item = mDict[name];
            msg += item.Name + ", ";
            msg += item.Rarity + ", ";
            msg += item.Ability + ", ";
            msg += item.Value + ", ";
            msg += item.Path + ")";
            Log.Debug(msg);
        }
    }
}
