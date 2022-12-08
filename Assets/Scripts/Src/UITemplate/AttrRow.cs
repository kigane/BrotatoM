using UnityEngine;
using UnityEngine.UIElements;
using QFramework;
using System;

namespace BrotatoM
{
    public class AttrRow : VisualElement, IController
    {
        // 模板外套的根元素
        private readonly TemplateContainer mTemplateContainer;

        // 让自定义控件可以显示到UI Builder中
        public new class UxmlFactory : UxmlFactory<AttrRow> { }

        // 提供无参构造函数
        public AttrRow()
        {
            mTemplateContainer = Resources.Load<VisualTreeAsset>("UI/Template/attr-container").Instantiate();

            hierarchy.Add(mTemplateContainer);
        }

        public AttrRow(AttrInfo attrInfo) : this()
        {
            var playerSystem = this.GetSystem<IPlayerSystem>();
            mTemplateContainer.Q("attr-icon").style.backgroundImage = new StyleBackground(Resources.Load<Sprite>(attrInfo.path));
            mTemplateContainer.Q<Label>("attr-name-text").text = attrInfo.name;
            mTemplateContainer.Q<Label>("attr-value-text").text = attrInfo.value.ToString();

            var property = playerSystem.GetBindablePropertyByName<float>(attrInfo.name);
            property.Register(value =>
            {
                // Log.Debug($"{attrInfo.name} {value}");
                if (playerSystem.IsPercentValue(attrInfo.name))
                {
                    mTemplateContainer.Q<Label>("attr-value-text").text = ((int)(value * 100)).ToString();
                }
                else
                {
                    mTemplateContainer.Q<Label>("attr-value-text").text = ((int)value).ToString();
                }
            });
        }

        public IArchitecture GetArchitecture()
        {
            return BrotatoGame.Interface;
        }
    }
}
