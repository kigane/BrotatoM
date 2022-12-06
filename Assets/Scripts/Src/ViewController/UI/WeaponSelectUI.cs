using UnityEngine;
using UnityEngine.UIElements;
using QFramework;

namespace BrotatoM
{
    public class WeaponSelectUI : BrotatoGameController
    {
        private VisualElement mRootElement;
        private WeaponConfigItem[] mWeaponConfigItems;

        private void OnEnable()
        {
            mWeaponConfigItems = this.GetModel<WeaponConfigModel>().GetAllConfigItems();

            mRootElement = GetComponent<UIDocument>().rootVisualElement;
            if (mWeaponConfigItems.Length == 0)
                return;

            var firstRow = mRootElement.Q("first-row");
            var secondRow = mRootElement.Q("second-row");
            var thirdRow = mRootElement.Q("third-row");
            firstRow.Clear();
            secondRow.Clear();
            thirdRow.Clear();

            //TODO 根据选中的人物显示武器
            InfoButton WeaponBtn;
            for (int i = 0; i < mWeaponConfigItems.Length; i++)
            {
                WeaponBtn = new InfoButton(mWeaponConfigItems[i].Path, i, OnClick, OnHover);
                WeaponBtn.style.flexBasis = Length.Percent(9);
                if (i < 11)
                {
                    firstRow.Add(WeaponBtn);
                }
                else if (i < 22)
                {
                    secondRow.Add(WeaponBtn);
                }
                else
                {
                    thirdRow.Add(WeaponBtn);
                }
            }
        }

        private void OnHover(int i)
        {
            // 显示选中角色的信息
            mRootElement.Q("weapon-icon").style.backgroundImage = new StyleBackground(Resources.Load<Sprite>(mWeaponConfigItems[i].Path));
            mRootElement.Q<Label>("weapon-name").text = mWeaponConfigItems[i].Name;
            // TODO 构建描述
            mRootElement.Q<Label>("weapon-description").text = mWeaponConfigItems[i].SpecialEffects;
        }

        private void OnClick(int i)
        {
            //TODO 记录选中武器
            Log.Debug("选择了武器: " + mWeaponConfigItems[i].Name);
            // 切换到难度选择面板
            this.SendCommand<NextPanelCommand>();
        }
    }
}
