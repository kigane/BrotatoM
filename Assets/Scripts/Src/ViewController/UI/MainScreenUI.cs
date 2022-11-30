using UnityEngine;
using UnityEngine.UIElements;
using QFramework;
using System.Collections;
// using DG.Tweening;

namespace BrotatoM
{
    public class MainScreenUI : BrotatoGameController
    {
        private VisualElement mRootElement;
        private VisualElement mPromptElement;
        private VisualElement mBodyContainer;
        private VisualElement mHealthBar;
        private VisualElement mExpBar;
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

            mHarvestLabel = mRootElement.Q<Label>("stuff-amount");
            mHarvestLabel.text = mPlayerModel.Harvest.Value.ToString();
            mHarvestBagLabel = mRootElement.Q<Label>("stock-amount");
            mHarvestBagLabel.text = mPlayerModel.HarvestBag.Value.ToString();

            mTimeLabel = mRootElement.Q<Label>("time");
            StartCoroutine(CountDown(20));

            // UI Toolkit在第一帧还没有计算出各个元素的width,height，值都为NaN
            // 需要等待一帧后才能获取到实际值
            StartCoroutine(MainScreenUIInitialization());

            // Invoke(nameof(AnimateLoadingBar), 1f);
        }

        private void AnimateLoadingBar()
        {
            //Grab the final width of the progress bar based on the parent and
            //remove 25px to account for margins
            // float endWidth = mHealthBar.parent.worldBound.width - 10;
            // DOTween.To(() => 0, x => mHealthBar.style.width = x, endWidth, 5f).SetEase(Ease.Linear);
        }

        private IEnumerator MainScreenUIInitialization()
        {
            yield return null; // 等待一帧
            mHealthBar = mRootElement.Q("health-bar");
            float healthBarLength = mHealthBar.parent.worldBound.width - 10;
            mHealthBar.style.width = (float)mPlayerModel.HP.Value / (float)mPlayerModel.MaxHP.Value * healthBarLength;

            mExpBar = mRootElement.Q("exp-bar");
            float expBarLength = mExpBar.parent.resolvedStyle.width - 10;
            mExpBar.style.width = (float)mPlayerModel.Exp.Value / (float)mPlayerModel.CurrMaxExp.Value * expBarLength;
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
        }
    }
}
