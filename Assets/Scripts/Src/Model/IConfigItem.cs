using QFramework;

namespace BrotatoM
{
    /// <summary>
    /// 让ReadJSONToDictionary泛型方法可以使用类型的Name属性而添加
    /// </summary>
    public interface IConfigItem
    {
        string Name { get; }
    }
}
