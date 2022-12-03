using UnityEngine;
using UnityEngine.UIElements;

namespace BrotatoM
{
    public class InfoPanel : VisualElement
    {
        // 让自定义控件可以显示到UI Builder中
        public new class UxmlFactory : UxmlFactory<InfoPanel> { }

        // 提供无参构造函数
        public InfoPanel() { }


    }
}
