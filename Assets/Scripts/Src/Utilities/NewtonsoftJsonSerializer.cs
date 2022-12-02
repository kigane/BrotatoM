using System.Collections.Generic;
using Newtonsoft.Json;
using QFramework;
using UnityEngine;

namespace BrotatoM
{
    public interface IJsonSerializer : IUtility
    {
        void ReadJsonToDictionary<T>(string path, Dictionary<string, T> dict) where T : IConfigItem;
    }

    public class NewtonsoftJsonSerializer : IJsonSerializer
    {
        /// <summary>
        /// 读取配置文件到字典，以配置类的Name项为Key，配置类为Value
        /// </summary>
        /// <param name="path">配置文件路径，不需要后缀</param>
        /// <param name="dict">目标字典</param>
        /// <typeparam name="T">配置类</typeparam>
        public void ReadJsonToDictionary<T>(string path, Dictionary<string, T> dict) where T : IConfigItem
        {
            string jsonText = Resources.Load<TextAsset>(path).text;
            Debug.Log("WeaponConfigModel OnInit");
            T[] items = JsonConvert.DeserializeObject<T[]>(jsonText);
            for (int i = 0; i < items.Length; i++)
            {
                dict.Add(items[i].Name, items[i]);
            }
        }
    }
}
