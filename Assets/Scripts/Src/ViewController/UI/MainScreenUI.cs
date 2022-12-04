using UnityEngine;
using UnityEngine.UIElements;
using QFramework;
using System.Collections;
using System;
// using DG.Tweening;

namespace BrotatoM
{
    public class MainScreenUI : BrotatoGameController
    {
        private VisualElement mRootElement;
        private VisualElement mPromptElement;
        private VisualElement mBodyContainer;
        private Label mHarvestLabel;
        private Label mHarvestBagLabel;
        private Label mTimeLabel;
        private IPlayerModel mPlayerModel;

        private void Start()
        {
            mPlayerModel = this.GetModel<IPlayerModel>();
            mRootElement = GetComponent<UIDocument>().rootVisualElement;

            mPromptElement = mRootElement.Q<Label>("prompt");
            mPromptElement.style.display = DisplayStyle.None;

            mBodyContainer = mRootElement.Q("body-container");
            mBodyContainer.style.display = DisplayStyle.None;

            // 收获
            mHarvestLabel = mRootElement.Q<Label>("stuff-amount");
            mHarvestLabel.text = mPlayerModel.Harvest.Value.ToString();
            // 收获袋
            mHarvestBagLabel = mRootElement.Q<Label>("stock-amount");
            mHarvestBagLabel.text = mPlayerModel.HarvestBag.Value.ToString();

            mTimeLabel = mRootElement.Q<Label>("time");

            // 倒计时
            StartCoroutine(CountDown(20));

            // 设置属性图标
            var mAttrIcon = mRootElement.Query("attr-icon").First();
            mAttrIcon.style.backgroundImage = new StyleBackground(Resources.Load<Sprite>("ArtAssets/Stats/20px-Ranged_Damage"));

            // UI Toolkit在第一帧还没有计算出各个元素的width,height，值都为NaN
            // 需要等待一帧后才能获取到实际值
            StartCoroutine(MainScreenUIInitialization());

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
            this.RegisterEvent<UpgradeEvent>(OnUpgrade);
            #endregion
        }

        private void OnDestroy()
        {
            // 注销事件处理函数
            this.UnRegisterEvent<UpgradeEvent>(OnUpgrade);
        }

        private void OnUpgrade(UpgradeEvent obj)
        {
            throw new NotImplementedException();
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

        private IEnumerator CountDown(int seconds)
        {
            mTimeLabel.text = seconds.ToString();
            while (seconds > 0)
            {
                yield return new WaitForSeconds(1);
                seconds--;
                mTimeLabel.text = seconds.ToString();
            }

            // 一波结束命令
        }
    }
}
