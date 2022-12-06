using UnityEngine;
using UnityEngine.UIElements;
using QFramework;
using System.Collections;
using UnityEngine.InputSystem;

namespace BrotatoM
{
    public class MainScreenUI : BrotatoGameController
    {
        public UIDocument stopScreenUI;
        private VisualElement mRootElement;
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
            mPlayerSystem = this.GetSystem<IPlayerSystem>();
            mRootElement = GetComponent<UIDocument>().rootVisualElement;

            mPromptLabel = mRootElement.Q<Label>("prompt");
            mPromptLabel.style.display = DisplayStyle.None;

            mBodyContainer = mRootElement.Q("body-container");
            mBodyContainer.style.display = DisplayStyle.None;

            // 收获
            mHarvestLabel = mRootElement.Q<Label>("stuff-amount");
            mHarvestLabel.text = mPlayerSystem.Harvest.Value.ToString();
            // 收获袋
            mHarvestBagLabel = mRootElement.Q<Label>("stock-amount");
            mHarvestBagLabel.text = mPlayerSystem.HarvestBag.Value.ToString();

            // 倒计时
            mTimeLabel = mRootElement.Q<Label>("time");
            mTimeLabel.text = "5";

            // 升级界面
            mLevelUpContainer = mRootElement.Q("levelup");
            mLevelUpContainer.Clear();

            mUpgradeContainer = mRootElement.Q("attrs-select-container");
            mUpgradeContainer.Clear();
            mUpgradeContainer.style.flexDirection = FlexDirection.Row;

            var upgradeItems = this.GetModel<UpgradeConfigModel>().GetAllConfigItems().GetRandomElements(4);
            UpgradeContainer upgradeContainer;
            for (int i = 0; i < 4; i++)
            {
                upgradeContainer = new UpgradeContainer(upgradeItems[i]);
                upgradeContainer.style.flexBasis = Length.Percent(25);
                upgradeContainer.style.marginLeft = Length.Percent(0.5f);
                mUpgradeContainer.Add(upgradeContainer);
            }

            // 属性栏
            mAttrPanel = mRootElement.Q("attrs");
            mAttrPanel.Clear();

            // 生成属性行
            mNeedShowProperties = this.SendQuery(new NeedShowPropertiesQuery());
            AttrRow attrRow;
            for (int i = 0; i < mNeedShowProperties.Length; i++)
            {
                attrRow = new AttrRow(mNeedShowProperties[i]);
                attrRow.style.flexBasis = Length.Percent(6.5f);
                mAttrPanel.Add(attrRow);
            }

            mRootElement.Query<Button>().ForEach(btn =>
            {
                var rawBackgroundColor = btn.style.backgroundColor;
                var rawColor = btn.style.color;
                // :hover 的替代方案。 鼠标移动上去变成白底黑字，离开则恢复为黑底白字。
                btn.RegisterCallback<MouseOverEvent>((type) =>
                {
                    btn.style.backgroundColor = new Color(1f, 1f, 1f, 0.8f);
                    btn.style.color = new Color(0, 0, 0, 0.8f);
                });

                btn.RegisterCallback<MouseLeaveEvent>((type) =>
                {
                    btn.style.backgroundColor = rawBackgroundColor;
                    btn.style.color = rawColor;
                });
            });

            // UI Toolkit在第一帧还没有计算出各个元素的width, height，值都为NaN
            // 需要等待一帧后才能获取到实际值
            StartCoroutine(MainScreenUIInitialization());
            #endregion

            // 倒计时
            mTimeSystem.AddCountDownTask(5);

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

            // 倒计时
            this.RegisterEvent<CountDownIntervalEvent>(e =>
            {
                mTimeLabel.text = e.Seconds.ToString();
            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // 一波结束
            this.RegisterEvent<CountDownOverEvent>(e =>
            {
                mPromptLabel.text = "通过!";
                mPromptLabel.style.display = DisplayStyle.Flex;
                this.GetSystem<ITimeSystem>().AddDelayTask(1f, () =>
                {
                    mPromptLabel.text = "升级!";
                    this.SendCommand<WaveOverCommand>();
                });
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
                Log.Info("游戏暂停", 16);
                stopScreenUI.gameObject.SetActive(true);
                // mBodyContainer.style.display = DisplayStyle.Flex;
                mTimeSystem.Stop();
                mGameState = GameState.STOPPED;
                Time.timeScale = 0;
            }
            else if (mGameState == GameState.STOPPED)
            {
                Log.Info("游戏继续", 16);
                stopScreenUI.gameObject.SetActive(false);
                // mBodyContainer.style.display = DisplayStyle.None;
                mTimeSystem.Resume();
                mGameState = GameState.PLAY;
                Time.timeScale = 1;
            }
        }

        private IEnumerator MainScreenUIInitialization()
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
    }
}
