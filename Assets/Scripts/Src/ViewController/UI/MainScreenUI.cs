using UnityEngine;
using UnityEngine.UIElements;
using QFramework;
using System.Collections;
using System;
using UnityEngine.InputSystem;
// using DG.Tweening;

namespace BrotatoM
{
    public class MainScreenUI : BrotatoGameController
    {
        private VisualElement mRootElement;
        private VisualElement mBodyContainer;
        private VisualElement mLevelUpContainer;
        private VisualElement mAttrPanel;
        private Label mPromptLabel;
        private Label mHarvestLabel;
        private Label mHarvestBagLabel;
        private Label mTimeLabel;
        private IPlayerModel mPlayerModel;
        private PlayerControl mPlayerControl;
        private ITimeSystem mTimeSystem;
        private GameState mGameState = GameState.PLAY;
        private AttrInfo[] mNeedShowProperties;

        private void Awake()
        {
            mPlayerControl = new PlayerControl();
            mTimeSystem = this.GetSystem<ITimeSystem>();
        }

        private void Start()
        {
            #region UI处理
            // 取得UI元素的引用
            mPlayerModel = this.GetModel<IPlayerModel>();
            mRootElement = GetComponent<UIDocument>().rootVisualElement;

            mPromptLabel = mRootElement.Q<Label>("prompt");
            mPromptLabel.style.display = DisplayStyle.None;

            mBodyContainer = mRootElement.Q("body-container");
            mBodyContainer.style.display = DisplayStyle.None;

            // 收获
            mHarvestLabel = mRootElement.Q<Label>("stuff-amount");
            mHarvestLabel.text = mPlayerModel.Harvest.Value.ToString();
            // 收获袋
            mHarvestBagLabel = mRootElement.Q<Label>("stock-amount");
            mHarvestBagLabel.text = mPlayerModel.HarvestBag.Value.ToString();

            // 倒计时
            mTimeLabel = mRootElement.Q<Label>("time");
            mTimeLabel.text = "5";

            // 升级界面
            mLevelUpContainer = mRootElement.Q("levelup");
            mLevelUpContainer.Clear();

            // 属性栏
            mAttrPanel = mRootElement.Q("attrs");
            mAttrPanel.Clear();

            // 生成属性行
            mNeedShowProperties = this.SendQuery(new NeedShowPropertiesQuery());
            AttrRow attrRow;
            for (int i = 0; i < mNeedShowProperties.Length; i++)
            {
                attrRow = new AttrRow(mNeedShowProperties[i]);
                mAttrPanel.Add(attrRow);
            }

            // UI Toolkit在第一帧还没有计算出各个元素的width, height，值都为NaN
            // 需要等待一帧后才能获取到实际值
            StartCoroutine(MainScreenUIInitialization());
            #endregion

            // 倒计时
            mTimeSystem.AddCountDownTask(5);

            #region 注册值变更事件
            mPlayerModel.HP.Register(value =>
            {
                UpdateBar("health-bar", value / mPlayerModel.MaxHp.Value);
            });

            mPlayerModel.Exp.Register(value =>
            {
                UpdateBar("exp-bar", value / mPlayerModel.CurrMaxExp.Value);
            });

            mPlayerModel.Harvest.Register(value =>
            {
                mRootElement.Q<Label>("stuff-amount").text = value.ToString();
            });
            #endregion

            #region 注册事件处理函数
            // 升级
            this.RegisterEvent<UpgradeEvent>(e =>
            {
                Log.Info("升级!");
                // icon为UI Builder中的模板元素
                var icon = new Icon("ArtAssets/Characters/60px-Mutant");
                icon.style.flexBasis = Length.Percent(25);
                mLevelUpContainer.Add(icon);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            this.RegisterEvent<CountDownIntervalEvent>(e =>
            {
                mTimeLabel.text = e.Seconds.ToString();
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            this.RegisterEvent<CountDownOverEvent>(e =>
            {
                mPromptLabel.text = "升级!";
                mPromptLabel.style.display = DisplayStyle.Flex;
                this.SendCommand<WaveOverCommand>();
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
            #endregion
        }

        private void OnEnable()
        {
            mPlayerControl.Enable();
            mPlayerControl.Player.Return.performed += OnReturn;
        }

        private void OnReturn(InputAction.CallbackContext obj)
        {
            if (mGameState == GameState.PLAY)
            {
                // 显示暂停界面
                Log.Info("游戏暂停");
                mBodyContainer.style.display = DisplayStyle.Flex;
                mTimeSystem.Stop();
                mGameState = GameState.STOPPED;
            }
            else if (mGameState == GameState.STOPPED)
            {
                Log.Info("游戏继续");
                mBodyContainer.style.display = DisplayStyle.None;
                mTimeSystem.Resume();
                mGameState = GameState.PLAY;
            }
        }

        private IEnumerator MainScreenUIInitialization()
        {
            yield return null; // 等待一帧
            UpdateBar("health-bar", mPlayerModel.HP.Value / mPlayerModel.MaxHp.Value);
            UpdateBar("exp-bar", mPlayerModel.Exp.Value / mPlayerModel.CurrMaxExp.Value);
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
    }
}
