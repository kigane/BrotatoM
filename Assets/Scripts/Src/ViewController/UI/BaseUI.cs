using QFramework;
using UnityEngine;
using UnityEngine.UIElements;
using System;

namespace BrotatoM
{
    public abstract class BaseUI : BrotatoGameController
    {
        protected VisualElement mRootElement;

        /// <summary>
        /// 在OnEnable()方法内，获取UIDocument的根元素后执行。
        /// </summary>
        protected abstract void OnUIEnable();

        protected void OnEnable()
        {
            Log.Debug(this + " OnEnable");
            mRootElement = GetComponent<UIDocument>().rootVisualElement;
            OnUIEnable();
        }

        protected void ShowPlayerProperties()
        {
            var mAttrPanel = mRootElement.Q("attrs");
            mAttrPanel.Clear();
            var mNeedShowProperties = this.SendQuery(new NeedShowPropertiesQuery());
            AttrRow attrRow;
            for (int i = 0; i < mNeedShowProperties.Length; i++)
            {
                attrRow = new AttrRow(mNeedShowProperties[i]);
                attrRow.style.flexBasis = Length.Percent(6.5f);
                mAttrPanel.Add(attrRow);
            }
        }

        protected void RegisterBtnHoverBehaviour()
        {
            // 按钮hover
            mRootElement.Query<Button>().ForEach(btn =>
            {
                var rawBackgroundColor = btn.style.backgroundColor;
                var rawColor = btn.style.color;
                // :hover 的替代方案。 鼠标移动上去变成白底黑字，离开则恢复为黑底白字。
                btn.RegisterCallback<MouseOverEvent>((type) =>
                {
                    btn.style.backgroundColor = UIColor.WHITE;
                    btn.style.color = UIColor.BLACK;
                });

                btn.RegisterCallback<MouseLeaveEvent>((type) =>
                {
                    btn.style.backgroundColor = rawBackgroundColor;
                    btn.style.color = rawColor;
                });
            });
        }
    }
}
