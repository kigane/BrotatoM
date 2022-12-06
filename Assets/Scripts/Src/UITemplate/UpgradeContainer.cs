using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace BrotatoM
{
    public class UpgradeContainer : VisualElement
    {
        // 模板外套的根元素
        private readonly TemplateContainer mTemplateContainer;

        // 让自定义控件可以显示到UI Builder中
        public new class UxmlFactory : UxmlFactory<UpgradeContainer> { }

        // 提供无参构造函数
        public UpgradeContainer()
        {
            mTemplateContainer = Resources.Load<VisualTreeAsset>("UI/Template/attr-select-container").Instantiate();
            mTemplateContainer.style.flexGrow = 1;

            hierarchy.Add(mTemplateContainer);
        }

        // 有参构造函数
        public UpgradeContainer(UpgradeConfigItem item) : this()
        {
            mTemplateContainer.Q("attr-pic").style.backgroundImage = new StyleBackground(Resources.Load<Sprite>(item.Path));
            mTemplateContainer.Q<Label>("attr-name").text = item.Name;
            mTemplateContainer.Q<Label>("attr-description").text = item.Value + item.Ability;
            mTemplateContainer.Q<Button>("select-btn").clickable.clicked += () =>
            {
                Log.Debug($"选择了 {item.Name}: +{item.Value}{item.Ability}");
            };
        }
    }
}
