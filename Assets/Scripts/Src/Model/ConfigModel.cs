using QFramework;
using System.Collections.Generic;
using UnityEngine;

namespace BrotatoM
{
    public class ConfigModel<T> : AbstractModel, IModel where T : IConfigItem
    {
        protected string mConfigPath;
        protected Dictionary<string, T> mDict = new();

        public ConfigModel(string path) { }

        public T1 GetConfigItemByName<T1>(string name) where T1 : T
        {
            return (T1)mDict[name];
        }

        protected override void OnInit()
        {
            this.GetUtility<IJsonSerializer>().ReadJsonToDictionary(mConfigPath, mDict);
        }
    }
}
