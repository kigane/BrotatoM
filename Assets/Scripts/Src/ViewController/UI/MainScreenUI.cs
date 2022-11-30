using UnityEngine;
using UnityEngine.UIElements;
using QFramework;
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
        private IPlayerModel mPlayerModel;

        private void Start()
        {
            mPlayerModel = this.GetModel<IPlayerModel>();
            mRootElement = GetComponent<UIDocument>().rootVisualElement;

            mPromptElement = mRootElement.Q<Label>("prompt");
            mPromptElement.style.display = DisplayStyle.None;

            mBodyContainer = mRootElement.Q("body-container");
            mBodyContainer.style.display = DisplayStyle.None;

            mHealthBar = mRootElement.Q("health-bar");
            float healthBarLength = mHealthBar.parent.worldBound.width - 10;
            mHealthBar.style.width = mPlayerModel.HP.Value / mPlayerModel.MaxHP.Value * healthBarLength;

            mExpBar = mRootElement.Q("exp-bar");
            float expBarLength = mExpBar.parent.worldBound.width - 10;
            mExpBar.style.width = mPlayerModel.Exp.Value / mPlayerModel.CurrMaxExp.Value * expBarLength;
            Debug.Log(mPlayerModel.Exp.Value);
            Debug.Log(mPlayerModel.CurrMaxExp.Value);
            Debug.Log(mPlayerModel.Exp.Value / mPlayerModel.CurrMaxExp.Value);
            Debug.Log(expBarLength);
            Debug.Log(mPlayerModel.Exp.Value / mPlayerModel.CurrMaxExp.Value * expBarLength);

            // Invoke(nameof(AnimateLoadingBar), 1f);
        }

        private void AnimateLoadingBar()
        {
            //Grab the final width of the progress bar based on the parent and
            //remove 25px to account for margins
            // float endWidth = mHealthBar.parent.worldBound.width - 10;
            // DOTween.To(() => 0, x => mHealthBar.style.width = x, endWidth, 5f).SetEase(Ease.Linear);
        }
    }
}
