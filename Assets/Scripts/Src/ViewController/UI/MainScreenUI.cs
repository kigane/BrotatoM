﻿using UnityEngine;
using UnityEngine.UIElements;
using QFramework;
using System.Collections;
using UnityEngine.InputSystem;

namespace BrotatoM
{
    public class MainScreenUI : BaseUI
    {
        public UIDocument stopScreenUI;
        public UIDocument shopScreenUI;
        private VisualElement mBodyContainer;
        private VisualElement mLevelUpContainer;
        private VisualElement mUpgradeContainer;
        private VisualElement mAttrPanel;
        private Label mPromptLabel;
        private Label mHarvestLabel;
        private Label mHarvestBagLabel;
        private Label mTimeLabel;
        private IPlayerSystem mPlayerSystem;
        private PlayerControl mPlayerControl;
        private ITimeSystem mTimeSystem;
        private AttrInfo[] mNeedShowProperties;

        private void Awake()
        {
            mPlayerControl = new PlayerControl();
            mTimeSystem = this.GetSystem<ITimeSystem>();
            mPlayerSystem = this.GetSystem<IPlayerSystem>();
        }

        protected override void OnUIEnable()
        {
            mPlayerControl.Enable();
            mPlayerControl.Player.Return.performed += OnReturn;

            #region UI处理
            // 取得UI元素的引用
            mPromptLabel = mRootElement.Q<Label>("prompt");
            Hide(mPromptLabel);

            mBodyContainer = mRootElement.Q("body-container");
            Hide(mBodyContainer);

            // 收获
            mHarvestLabel = mRootElement.Q<Label>("stuff-amount");
            mHarvestLabel.text = mPlayerSystem.Harvest.Value.ToString();
            // 收获袋
            mHarvestBagLabel = mRootElement.Q<Label>("stock-amount");
            mHarvestBagLabel.text = mPlayerSystem.HarvestBag.Value.ToString();

            // 倒计时
            mTimeLabel = mRootElement.Q<Label>("time");
            mTimeLabel.text = Params.WaveLastSeconds[mPlayerSystem.CurrWave.Value].ToString();
            mTimeSystem.AddCountDownTask(Params.WaveLastSeconds[mPlayerSystem.CurrWave.Value]);
            GameManager.Instance.GenerateEnemies(5);

            // 升级图标
            mLevelUpContainer = mRootElement.Q("levelup");
            mLevelUpContainer.Clear();

            // 升级界面
            ShowRandomUpgradeItems();

            // 属性栏
            mAttrPanel = mRootElement.Q("attrs");
            mAttrPanel.Clear();

            mNeedShowProperties = this.SendQuery(new NeedShowPropertiesQuery());
            AttrRow attrRow;
            for (int i = 0; i < mNeedShowProperties.Length; i++)
            {
                attrRow = new AttrRow(mNeedShowProperties[i]);
                attrRow.style.flexBasis = Length.Percent(6.5f);
                mAttrPanel.Add(attrRow);
            }

            RegisterBtnHoverBehaviour();

            mRootElement.Q<Button>("refresh-btn").RegisterCallback<ClickEvent>((type) =>
            {
                ShowRandomUpgradeItems();
            });

            // UI Toolkit在第一帧还没有计算出各个元素的width, height，值都为NaN
            // 需要等待一帧后才能获取到实际值
            StartCoroutine(UIBarsInitialization());
            #endregion

            #region 注册值变更事件
            mPlayerSystem.HP.Register(value =>
            {
                UpdateBar("health-bar", value / mPlayerSystem.MaxHp.Value);
            });

            mPlayerSystem.Exp.Register(value =>
            {
                UpdateBar("exp-bar", value / mPlayerSystem.CurrMaxExp.Value);
            });

            mPlayerSystem.Harvest.Register(value =>
            {
                mRootElement.Q<Label>("stuff-amount").text = value.ToString();
            });

            mPlayerSystem.CurrWave.Register(value =>
            {
                mRootElement.Q<Label>("wave").text = $"第{value}波";
            });
            #endregion

            #region 注册事件处理函数
            // 升级
            this.RegisterEvent<UpgradeEvent>(e =>
            {
                Log.Info("升级!");
                // icon为UI Builder中的模板元素
                var icon = new Icon(Params.UpgradeIconPath);
                icon.style.flexBasis = Length.Percent(25);
                mLevelUpContainer.Add(icon);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // 倒计时读秒
            this.RegisterEvent<CountDownIntervalEvent>(e =>
            {
                mTimeLabel.text = e.Seconds.ToString();
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // 倒计时结束
            this.RegisterEvent<CountDownOverEvent>(e =>
            {
                // 增加收获
                mPlayerSystem.Harvest.Value += mPlayerSystem.Harvesting.Value;
                //TODO 收集可收集物
                mPromptLabel.text = "通过!";
                Show(mPromptLabel);

                this.GetSystem<ITimeSystem>().AddDelayTask(1f, () =>
                {
                    if (mPlayerSystem.UpgradePoint > 0)
                    {// 升级
                        mPromptLabel.text = "升级!";
                        // 显示升级界面(根据UpgradePoint)
                        Show(mBodyContainer);
                    }//TODO 开箱子，获得道具
                    else
                    {// 直接进商店
                        shopScreenUI.gameObject.SetActive(true);
                    }
                });
                // TODO 在延迟的一秒内,让敌人消失，并收集可收集物
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // 刷新升级属性
            this.RegisterEvent<RefreshUpgradeItemsEvent>(e =>
            {
                ShowRandomUpgradeItems();
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // 进入商店界面
            this.RegisterEvent<WaveOverEvent>(e =>
            {
                shopScreenUI.gameObject.SetActive(true);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // 出发按钮
            this.RegisterEvent<NextWaveEvent>(e =>
            {
                shopScreenUI.gameObject.SetActive(false);
                Hide(mBodyContainer);
                Hide(mPromptLabel);
                // 开始新一轮倒计时
                mTimeSystem.AddCountDownTask(Params.WaveLastSeconds[mPlayerSystem.CurrWave.Value]);
                mTimeLabel.text = Params.WaveLastSeconds[mPlayerSystem.CurrWave.Value].ToString();
                // 升级图标清空
                mLevelUpContainer.Clear();
                //TODO 箱子图标清空
                //TODO 每隔几秒生成一波敌人
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            #endregion
        }

        private void OnDisable()
        {
            mPlayerControl.Player.Return.performed -= OnReturn;
        }

        private void OnReturn(InputAction.CallbackContext obj)
        {
            if (GameManager.Instance.State == GameState.PLAY)
            {
                // 显示暂停界面
                Log.Info("游戏暂停", 16);
                stopScreenUI.gameObject.SetActive(true);
                mTimeSystem.Stop();
                GameManager.Instance.State = GameState.STOPPED;
                Time.timeScale = 0;
            }
            else if (GameManager.Instance.State == GameState.STOPPED)
            {
                Log.Info("游戏继续", 16);
                stopScreenUI.gameObject.SetActive(false);
                mTimeSystem.Resume();
                GameManager.Instance.State = GameState.PLAY;
                Time.timeScale = 1;
            }
        }

        private void ShowRandomUpgradeItems()
        {
            mUpgradeContainer = mRootElement.Q("attrs-select-container");
            mUpgradeContainer.Clear();
            mUpgradeContainer.style.flexDirection = FlexDirection.Row;

            var upgradeItems = this.GetModel<UpgradeConfigModel>().GetAllConfigItems().GetRandomElements(4);
            UpgradeContainer upgradeContainer;
            for (int i = 0; i < 4; i++)
            {
                // 会因为闭包引发问题，注册的所有函数的i都是4
                // upgradeContainer = new UpgradeContainer(upgradeItems[i], () =>
                // {
                //     Log.Debug("Upgrade: " + i);
                //     var item = upgradeItems[i];
                //     Log.Debug($"选择了 {item.Name}: +{item.Value}{item.Ability}");
                //     // 加能力
                //     mPlayerSystem.AddFloatValueByPropertyName(item.Ability, item.Value);
                //     mPlayerSystem.UpgradePoint--;
                //     if (mPlayerSystem.UpgradePoint > 0)
                //     {
                //         GetRandomUpgradeItems();
                //     }
                // });
                upgradeContainer = new UpgradeContainer(upgradeItems[i]);
                upgradeContainer.style.flexBasis = Length.Percent(25);
                upgradeContainer.style.marginLeft = Length.Percent(0.5f);
                mUpgradeContainer.Add(upgradeContainer);
            }
        }

        private IEnumerator UIBarsInitialization()
        {
            yield return null; // 等待一帧
            UpdateBar("health-bar", mPlayerSystem.HP.Value / mPlayerSystem.MaxHp.Value);
            UpdateBar("exp-bar", mPlayerSystem.Exp.Value / mPlayerSystem.CurrMaxExp.Value);
        }

        /// <summary>
        /// 更新进度条
        /// </summary>
        /// <param name="barName">进度条ID</param>
        /// <param name="progress">进度</param>
        private void UpdateBar(string barName, float progress)
        {
            var bar = mRootElement.Q(barName);
            float barLength = bar.parent.worldBound.width - 10;
            bar.style.width = progress * barLength;
        }

        private void Show(VisualElement el)
        {
            el.style.display = DisplayStyle.Flex;
        }

        private void Hide(VisualElement el)
        {
            el.style.display = DisplayStyle.None;
        }
    }
}
