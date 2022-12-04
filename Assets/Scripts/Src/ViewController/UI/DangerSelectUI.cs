using UnityEngine;
using UnityEngine.UIElements;
using QFramework;

namespace BrotatoM
{
    public class DangerSelectUI : BrotatoGameController
    {
        private VisualElement mRootElement;
        private DangerConfigItem[] mDangerConfigItems;

        private void OnEnable()
        {
            mDangerConfigItems = this.GetModel<DangerConfigModel>().GetAllConfigItems();

            mRootElement = GetComponent<UIDocument>().rootVisualElement;
            if (mDangerConfigItems.Length == 0)
                return;

            var firstRow = mRootElement.Q("first-row");
            firstRow.Clear();

            //TODO 显示选中的人物,武器
            InfoButton dangerBtn;
            for (int i = 0; i < mDangerConfigItems.Length; i++)
            {
                dangerBtn = new InfoButton(mDangerConfigItems[i].Path, i, OnClick, OnHover);
                dangerBtn.style.flexBasis = Length.Percent(20);
                firstRow.Add(dangerBtn);
            }
        }

        private void OnHover(int i)
        {
            // 显示选中难度的信息
            mRootElement.Q("danger-icon").style.backgroundImage = new StyleBackground(Resources.Load<Sprite>(mDangerConfigItems[i].Path));
            mRootElement.Q<Label>("danger-name").text = mDangerConfigItems[i].Name;
            mRootElement.Q<Label>("danger-description").text = mDangerConfigItems[i].Modifiers;
        }

        private void OnClick(int i)
        {
            //TODO 记录选中难度
            Debug.Log("选择了难度: " + mDangerConfigItems[i].Name);
            // 切换到难度选择面板
            this.SendCommand<NextPanelCommand>();
        }
    }
}
