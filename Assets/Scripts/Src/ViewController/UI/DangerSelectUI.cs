using UnityEngine;
using UnityEngine.UIElements;
using QFramework;

namespace BrotatoM
{
    public class DangerSelectUI : BrotatoGameController
    {
        private VisualElement mRootElement;
        private DangerConfigItem[] mDangerConfigItems;
        private IPlayerSystem mPlayerSystem;
        private CharacterConfigModel mCharacterConfigModel;

        private void OnEnable()
        {
            mPlayerSystem = this.GetSystem<IPlayerSystem>();
            mCharacterConfigModel = this.GetModel<CharacterConfigModel>();
            mDangerConfigItems = this.GetModel<DangerConfigModel>().GetAllConfigItems();

            mRootElement = GetComponent<UIDocument>().rootVisualElement;
            if (mDangerConfigItems.Length == 0)
                return;

            ShowDangerLevels();

            this.RegisterEvent<NextPanelEvent>(e =>
            {
                ShowSelectedCharacter();
                if (mPlayerSystem.CurrWeapons.Count != 0)
                    ShowSelectedWeapon();
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void ShowSelectedCharacter()
        {
            // 显示已选人物
            // Log.Debug("已选人物:" + mPlayerSystem.CharacterId);
            var charaItem = mCharacterConfigModel.GetConfigItemById(mPlayerSystem.CharacterId);
            mRootElement.Q("chara-icon").style.backgroundImage = new StyleBackground(Resources.Load<Sprite>(charaItem.Path));
            mRootElement.Q<Label>("chara-name").text = charaItem.Name;
            mRootElement.Q<Label>("chara-description").text = charaItem.Stats;
        }

        private void ShowSelectedWeapon()
        {
            // 显示已选人物
            WeaponInfo weaponInfo = mPlayerSystem.CurrWeapons[0];
            // Log.Debug("已选武器:" + weaponInfo.Name);
            mRootElement.Q("weapon-icon").style.backgroundImage = new StyleBackground(Resources.Load<Sprite>(weaponInfo.Path));
            mRootElement.Q<Label>("weapon-name").text = weaponInfo.Name;
            mRootElement.Q<Label>("weapon-tag").text = weaponInfo.Class.ToString();
            mRootElement.Q<Label>("weapon-description").text = weaponInfo.SpecialEffects;
        }

        private void ShowDangerLevels()
        {
            var firstRow = mRootElement.Q("first-row");
            firstRow.Clear();
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
            Log.Debug("选择了难度: " + mDangerConfigItems[i].Name);
            this.GetSystem<IPlayerSystem>().DangerLevel = i;
            // 切换到难度选择面板
            this.SendCommand<NextPanelCommand>();
        }
    }
}
