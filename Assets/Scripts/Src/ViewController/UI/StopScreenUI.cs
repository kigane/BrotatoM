using QFramework;
using UnityEngine;
using UnityEngine.UIElements;

namespace BrotatoM
{
    public class StopScreenUI : BaseUI
    {
        private IPlayerSystem mPlayerSystem;
        private VisualElement mAttrPanel;

        private void Awake()
        {
            mPlayerSystem = this.GetSystem<IPlayerSystem>();
        }

        protected override void OnUIEnable()
        {
            // 显示武器

            // 显示道具

            // 显示属性
            ShowPlayerProperties();

            // 注册按钮事件
            RegisterBtnHoverBehaviour();
        }

    }
}
