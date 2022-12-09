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
        private readonly IPlayerSystem mPlayerSystem;

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
            mPlayerSystem = this.GetSystem<IPlayerSystem>();
            mTemplateContainer.Q("attr-icon").style.backgroundImage = new StyleBackground(Resources.Load<Sprite>(attrInfo.path));
            mTemplateContainer.Q<Label>("attr-name-text").text = attrInfo.name;
            mTemplateContainer.Q<Label>("attr-value-text").text = GetAttrValue(attrInfo.name, attrInfo.value);

            var property = mPlayerSystem.GetBindablePropertyByName<float>(attrInfo.name);
            property.Register(value =>
            {
                // Log.Debug($"{attrInfo.name} {value}");
                mTemplateContainer.Q<Label>("attr-value-text").text = GetAttrValue(attrInfo.name, value);
            });
        }

        private string GetAttrValue(string name, float value)
        {
            if (mPlayerSystem.IsPercentValue(name))
            {
                return Mathf.RoundToInt(value * 100).ToString();
            }
            else
            {
                return Mathf.RoundToInt(value).ToString();
            }
        }

        public IArchitecture GetArchitecture()
        {
            return BrotatoGame.Interface;
        }
    }
}
