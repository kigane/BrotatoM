using UnityEngine;
using UnityEngine.UIElements;
// using DG.Tweening;

namespace BrotatoM
{
    public class MainScreenUI : MonoBehaviour
    {
        private VisualElement mRootElement;
        private VisualElement mPromptElement;
        private VisualElement mBodyContainer;
        private VisualElement mHealthBar;

        private void Start()
        {
            mRootElement = GetComponent<UIDocument>().rootVisualElement;

            mPromptElement = mRootElement.Q<Label>("prompt");
            mPromptElement.style.display = DisplayStyle.None;

            mBodyContainer = mRootElement.Q("body-container");
            mBodyContainer.style.display = DisplayStyle.None;

            mHealthBar = mRootElement.Q("health-bar");

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
