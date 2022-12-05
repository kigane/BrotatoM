using System;
using System.Reflection;
using QFramework;

namespace BrotatoM
{
    public struct AttrInfo
    {
        public string path;
        public string name;
        public string value;
    }

    public class NeedShowPropertiesQuery : AbstractQuery<AttrInfo[]>
    {
        private AttrInfo[] properties = new AttrInfo[16];
        protected override AttrInfo[] OnDo()
        {
            //TODO
            return properties;
        }
    }
}
