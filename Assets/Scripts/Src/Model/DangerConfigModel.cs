using QFramework;
using UnityEngine;

namespace BrotatoM
{
    public class DangerConfigItem : IConfigItem
    {
        public string Name { get; set; }
        public string Modifiers { get; set; }
        public string Unlocks { get; set; }
        public string Path { get; set; }
    }

    public class DangerConfigModel : BaseConfigModel<DangerConfigItem>
    {
        public DangerConfigModel(string path) : base(path)
        {
        }

        protected override void OnInit()
        {
            base.OnInit();
            // VerifyLogs("0");
        }

        private void VerifyLogs(string name)
        {
            string msg = "Dangers-" + name + ": (";
            DangerConfigItem item = mDict[name];
            msg += item.Name + ", ";
            msg += item.Modifiers + ", ";
            msg += item.Unlocks + ", ";
            msg += item.Path + ")";
            Log.Debug(msg);
        }
    }
}
