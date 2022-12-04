using QFramework;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace BrotatoM
{
    public class BaseConfigModel<T> : AbstractModel, IModel where T : IConfigItem
    {
        protected string mConfigPath;
        protected Dictionary<string, T> mDict = new();
        protected T[] mItems;

        public BaseConfigModel(string path)
        {
            mConfigPath = path;
        }

        protected override void OnInit()
        {
            this.GetUtility<IJsonSerializer>().ReadJsonToDictionary(mConfigPath, mDict);
            mItems = mDict.Values.ToArray();
        }

        public T GetConfigItemByName(string name)
        {
            return mDict[name];
        }

        public T[] GetAllConfigItems()
        {
            return mItems;
        }

        public T GetConfigItemById(int i)
        {
            if (i < 0 || i >= mItems.Length)
                throw new System.IndexOutOfRangeException("Character Id is out of range!");

            return mItems[i];
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
