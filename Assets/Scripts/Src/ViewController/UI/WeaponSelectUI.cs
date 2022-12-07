using UnityEngine;
using UnityEngine.UIElements;
using QFramework;

namespace BrotatoM
{
    public class WeaponSelectUI : BrotatoGameController
    {
        private VisualElement mRootElement;
        private IPlayerSystem mPlayerSystem;
        private CharacterConfigModel mCharacterConfigModel;
        private WeaponConfigItem[] mWeaponConfigItems;

        private void Start()
        {
            mWeaponConfigItems = this.GetModel<WeaponConfigModel>().GetAllConfigItems();
            mCharacterConfigModel = this.GetModel<CharacterConfigModel>();
            mPlayerSystem = this.GetSystem<IPlayerSystem>();

            mRootElement = GetComponent<UIDocument>().rootVisualElement;

            //TODO 根据选中的人物显示武器
            GenerateWeaponIcons();

            this.RegisterEvent<NextPanelEvent>(e =>
            {
                ShowSelectedCharacter();
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void ShowSelectedCharacter()
        {
            // 显示已选人物
            Log.Debug("已选人物:" + mPlayerSystem.CharacterId);
            var charaItem = mCharacterConfigModel.GetConfigItemById(mPlayerSystem.CharacterId);
            mRootElement.Q("chara-icon").style.backgroundImage = new StyleBackground(Resources.Load<Sprite>(charaItem.Path));
            mRootElement.Q<Label>("chara-name").text = charaItem.Name;
            mRootElement.Q<Label>("chara-description").text = charaItem.Stats;
        }

        private void GenerateWeaponIcons()
        {
            // 武器选择
            var firstRow = mRootElement.Q("first-row");
            var secondRow = mRootElement.Q("second-row");
            var thirdRow = mRootElement.Q("third-row");
            firstRow.Clear();
            secondRow.Clear();
            thirdRow.Clear();

            InfoButton weaponBtn;
            var mCharacterWeaponItems = mWeaponConfigItems.GetRandomElements(33);
            for (int i = 0; i < mCharacterWeaponItems.Length; i++)
            {
                weaponBtn = new InfoButton(mCharacterWeaponItems[i].Path, i, OnClick, OnHover);
                weaponBtn.style.flexBasis = Length.Percent(9);
                if (i < 11)
                {
                    firstRow.Add(weaponBtn);
                }
                else if (i < 22)
                {
                    secondRow.Add(weaponBtn);
                }
                else
                {
                    thirdRow.Add(weaponBtn);
                }
            }
        }

        private void OnHover(int i)
        {
            // 显示选中武器的信息
            mRootElement.Q("weapon-icon").style.backgroundImage = new StyleBackground(Resources.Load<Sprite>(mWeaponConfigItems[i].Path));
            mRootElement.Q<Label>("weapon-name").text = mWeaponConfigItems[i].Name;
            // TODO 构建描述
            mRootElement.Q<Label>("weapon-description").text = mWeaponConfigItems[i].SpecialEffects;
        }

        private void OnClick(int i)
        {
            Log.Debug("选择了武器: " + mWeaponConfigItems[i].Name);
            mPlayerSystem.CurrWeapons.Clear();
            mPlayerSystem.AddWeapon(mWeaponConfigItems[i]);
            // 切换到难度选择面板
            this.SendCommand<NextPanelCommand>();
        }
    }
}
