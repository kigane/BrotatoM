using System;
using QFramework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace BrotatoM
{
    public class StopScreenUI : BaseUI
    {
        private IPlayerSystem mPlayerSystem;
        private ITimeSystem mTimeSystem;
        private GameManagerSystem mGMSystem;
        private ItemConfigModel mItemConfigModel;
        private VisualElement mAttrPanel;

        private void Awake()
        {
            mPlayerSystem = this.GetSystem<IPlayerSystem>();
            mTimeSystem = this.GetSystem<ITimeSystem>();
            mGMSystem = this.GetSystem<GameManagerSystem>();
            mItemConfigModel = this.GetModel<ItemConfigModel>();
        }

        protected override void OnUIEnable()
        {
            // 显示武器
            var weapons = mPlayerSystem.CurrWeapons;
            Log.Debug("武器数量" + weapons.Count);
            InfoButton weaponBtn;
            for (int i = 0; i < weapons.Count; i++)
            {
                Log.Debug(weapons[i].Name);
                // new出来的是UI Builder中的模板元素
                weaponBtn = new InfoButton(weapons[i]);
                weaponBtn.style.flexBasis = Length.Percent(25);
                weaponBtn.style.marginRight = 5;
                if (i < 3)
                {
                    mRootElement.Q("weapon-first-row").Add(weaponBtn);
                }
                else
                {
                    mRootElement.Q("weapon-second-row").Add(weaponBtn);
                }
            }

            // 显示道具
            var items = mPlayerSystem.CurrItems;
            InfoButton itemBtn;
            for (int i = 0; i < items.Count; i++)
            {
                // new出来的是UI Builder中的模板元素
                itemBtn = new InfoButton(mItemConfigModel.GetConfigItemById(items[i]));
                itemBtn.style.flexBasis = Length.Percent(25);
                itemBtn.style.marginRight = 5;
                if (i < 3)
                {
                    mRootElement.Q("item-first-row").Add(itemBtn);
                }
                else
                {
                    mRootElement.Q("item-second-row").Add(itemBtn);
                }
            }

            // 显示属性
            ShowPlayerProperties();

            // 注册按钮事件
            RegisterBtnHoverBehaviour();
            mRootElement.Q<Button>("continue-btn").clickable.clicked += OnContinue;
            mRootElement.Q<Button>("restart-btn").clickable.clicked += OnRestart;
            mRootElement.Q<Button>("settings-btn").clickable.clicked += OnSettings;
            mRootElement.Q<Button>("return-btn").clickable.clicked += OnReturn;
        }

        private void OnContinue()
        {
            Log.Info("游戏继续", 16);
            transform.gameObject.SetActive(false);
            mTimeSystem.Resume();
            mGMSystem.State = GameState.PLAY;
            Time.timeScale = 1;
        }

        private void OnRestart()
        {
            Log.Info("游戏重新开始", 16);
            mGMSystem.State = GameState.PLAY;
        }

        private void OnSettings()
        {
            Log.Info("显示设置界面", 16);
        }

        private void OnReturn()
        {
            Log.Info("返回主界面", 16);
            SceneManager.LoadScene("GameStartScene");
            mTimeSystem.ClearAllTasks();
            mTimeSystem.Resume();
            mGMSystem.State = GameState.SET_UP;
            Time.timeScale = 1;
            mPlayerSystem.ResetPlayerStat();
            mPlayerSystem.UpgradePoint = 0;
            mPlayerSystem.HarvestBag.Value = 0;
        }
    }
}
