using QFramework;
using UnityEngine;

namespace BrotatoM
{
    public class StatConfigItem : IConfigItem
    {
        public string Name { get; set; }
        public string Effects { get; set; }
        public string Limit { get; set; }
        public string NegativeStatEffect { get; set; }
        public string Path { get; set; }
        public string PathBig { get; set; }
    }

    public class StatConfigModel : BaseConfigModel<StatConfigItem>
    {
        public StatConfigModel(string path) : base(path)
        {
        }

        protected override void OnInit()
        {
            base.OnInit();
            // VerifyLogs("MaxHp");
        }

        private void VerifyLogs(string name)
        {
            string msg = "Stats-" + name + ": (";
            StatConfigItem item = mDict[name];
            msg += item.Name + ", ";
            msg += item.Limit + ", ";
            msg += item.Effects + ", ";
            msg += item.NegativeStatEffect + ", ";
            msg += item.Path + ", ";
            msg += item.PathBig + ")";
            Log.Debug(msg);
        }
    }
}
