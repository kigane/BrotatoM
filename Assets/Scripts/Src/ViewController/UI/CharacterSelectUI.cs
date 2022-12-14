using UnityEngine;
using UnityEngine.UIElements;
using QFramework;

namespace BrotatoM
{
    public class CharacterSelectUI : BrotatoGameController
    {
        private VisualElement mRootElement;
        private CharacterConfigItem[] mCharacterConfigItems;
        private IPlayerSystem mPlayerSystem;

        // 当开始游戏时，CharacterSelectUI GO被SetActivate(true)时，被调用
        private void OnEnable()
        {
            mPlayerSystem = this.GetSystem<IPlayerSystem>();
            mCharacterConfigItems = this.GetModel<CharacterConfigModel>().GetAllConfigItems();

            mRootElement = GetComponent<UIDocument>().rootVisualElement;
            GenerateCharacterIcons();
        }

        private void GenerateCharacterIcons()
        {
            var firstRow = mRootElement.Q("first-row");
            var secondRow = mRootElement.Q("second-row");
            var thirdRow = mRootElement.Q("third-row");
            firstRow.Clear();
            secondRow.Clear();
            thirdRow.Clear();

            InfoButton characterBtn;
            for (int i = 0; i < mCharacterConfigItems.Length; i++)
            {
                // new出来的是UI Builder中的模板元素
                characterBtn = new InfoButton(mCharacterConfigItems[i].Path, i, OnClick, OnHover);
                characterBtn.style.flexBasis = Length.Percent(9);
                if (i < 11)
                {
                    firstRow.Add(characterBtn);
                }
                else if (i < 22)
                {
                    secondRow.Add(characterBtn);
                }
                else
                {
                    thirdRow.Add(characterBtn);
                }
            }
        }

        private void OnHover(int i)
        {
            // 显示选中角色的信息
            mRootElement.Q("icon").style.backgroundImage = new StyleBackground(Resources.Load<Sprite>(mCharacterConfigItems[i].Path));
            mRootElement.Q<Label>("name").text = mCharacterConfigItems[i].Name;
            mRootElement.Q<Label>("description").text = mCharacterConfigItems[i].Stats;
        }

        private void OnClick(int i)
        {
            Log.Debug("选择了角色: " + mCharacterConfigItems[i].Name);
            mPlayerSystem.CurrItems.Clear();
            mPlayerSystem.AddItem(Params.ITEM_AMOUNT + i);
            mPlayerSystem.CharacterId = i;
            // 切换到武器面板
            this.SendCommand<NextPanelCommand>();
        }
    }
}
