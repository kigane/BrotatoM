using QFramework;
using UnityEngine;
using System.Linq;

namespace BrotatoM
{
    public class CharacterConfigItem : IConfigItem
    {
        public string Name { get; set; }
        public string Stats { get; set; }
        public string UnlockedBy { get; set; }
        public string Unlocks { get; set; }
        public string Path { get; set; }
    }

    public class CharacterConfigModel : BaseConfigModel<CharacterConfigItem>
    {
        public CharacterConfigModel(string path) : base(path)
        {

        }

        protected override void OnInit()
        {
            base.OnInit();
            // VerifyLogs("Chunky");
        }

        private void VerifyLogs(string name)
        {
            string msg = "Character-" + name + ": (";
            CharacterConfigItem item = mDict[name];
            msg += item.Name + ", ";
            msg += item.Stats + ", ";
            msg += item.UnlockedBy + ", ";
            msg += item.Unlocks + ", ";
            msg += item.Path + ")";
            Log.Debug(msg);
        }
    }
}
