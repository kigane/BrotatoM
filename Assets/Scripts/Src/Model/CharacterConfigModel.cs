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
        private CharacterConfigItem[] mItems;
        public CharacterConfigModel(string path) : base(path)
        {

        }

        protected override void OnInit()
        {
            base.OnInit();
            mItems = mDict.Values.ToArray();
            // VerifyLogs("Chunky");
        }

        public CharacterConfigItem[] GetAllCharacterConfig()
        {
            return mItems;
        }

        public CharacterConfigItem GetCharacterConfigItemById(int i)
        {
            if (i < 0 || i >= mItems.Length)
                throw new System.IndexOutOfRangeException("Character Id is out of range!");

            return mItems[i];
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
            Debug.Log(msg);
        }
    }
}
