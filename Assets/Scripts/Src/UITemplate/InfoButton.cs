using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace BrotatoM
{
    public class InfoButton : VisualElement
    {
        private readonly TemplateContainer mTemplateContainer;

        // 让自定义控件可以显示到UI Builder中
        public new class UxmlFactory : UxmlFactory<InfoButton> { }

        // 提供无参构造函数
        public InfoButton()
        {
            // 模板元素内的根容器。
            mTemplateContainer = Resources.Load<VisualTreeAsset>("UI/Template/character-btn").Instantiate();
            mTemplateContainer.style.flexGrow = 1.0f;

            hierarchy.Add(mTemplateContainer);
        }

        // 接受数据并展示
        public InfoButton(string path, int i, Action<int> onClick, Action<int> onHover) : this()
        {
            // userData = path; // VisualElement内置object类型数据保存变量
            var btn = mTemplateContainer.Q("character-btn");
            btn.style.backgroundImage = new StyleBackground(Resources.Load<Sprite>(path));

            // 鼠标移动上去背景色变白。
            btn.RegisterCallback<MouseOverEvent>((type) =>
            {
                onHover(i);
                btn.style.backgroundColor = UIColor.WHITE;
            });

            btn.RegisterCallback<MouseLeaveEvent>((type) =>
            {
                btn.style.backgroundColor = UIColor.DARKER_GRAY;
            });

            btn.RegisterCallback<ClickEvent>((type) =>
            {
                onClick(i);
            });
        }
    }
}
