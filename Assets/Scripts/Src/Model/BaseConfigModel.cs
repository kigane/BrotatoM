using QFramework;
using System.Collections.Generic;
using UnityEngine;

namespace BrotatoM
{
    public class BaseConfigModel<T> : AbstractModel, IModel where T : IConfigItem
    {
        protected string mConfigPath;
        protected Dictionary<string, T> mDict = new();

        public BaseConfigModel(string path)
        {
            mConfigPath = path;
        }

        public T GetConfigItemByName(string name)
        {
            return mDict[name];
        }

        protected override void OnInit()
        {
            this.GetUtility<IJsonSerializer>().ReadJsonToDictionary(mConfigPath, mDict);
        }

        protected string ArrLogMsg<T1>(T1[] arr)
        {
            string msg = "[";
            for (int i = 0; i < arr.Length; i++)
            {
                if (i == arr.Length - 1)
                    msg += arr[i] + "]";
                else
                    msg += arr[i] + ", ";
            }
            return msg;
        }
    }
}
