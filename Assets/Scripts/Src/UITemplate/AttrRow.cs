﻿using UnityEngine;
using UnityEngine.UIElements;

namespace BrotatoM
{
    public class AttrRow : VisualElement
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
            mTemplateContainer.Q("attr-icon").style.backgroundImage = new StyleBackground(Resources.Load<Sprite>(attrInfo.path));
            mTemplateContainer.Q<Label>("attr-name-text").text = attrInfo.name;
            mTemplateContainer.Q<Label>("attr-value-text").text = attrInfo.value.ToString();
        }
    }
}