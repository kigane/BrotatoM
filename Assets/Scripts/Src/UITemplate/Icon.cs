using UnityEngine;
using UnityEngine.UIElements;

namespace BrotatoM
{
    public class Icon : VisualElement
    {
        private readonly TemplateContainer mTemplateContainer;

        // 让自定义控件可以显示到UI Builder中
        public new class UxmlFactory : UxmlFactory<Icon> { }

        // 提供无参构造函数
        public Icon()
        {
            // 模板元素内的根容器。
            mTemplateContainer = Resources.Load<VisualTreeAsset>("UI/Template/icon").Instantiate();
            mTemplateContainer.style.flexGrow = 1.0f;

            hierarchy.Add(mTemplateContainer);
        }

        public Icon(string path) : this()
        {
            var icon = mTemplateContainer.Q("icon");
            icon.style.backgroundImage = new StyleBackground(Resources.Load<Sprite>(path));
        }
    }
}
