using UnityEngine;
using UnityEngine.UIElements;

namespace BrotatoM
{
    public class MainScreenUI : MonoBehaviour
    {
        private VisualElement mRootElement;
        private VisualElement mPromptElement;
        private VisualElement mBodyContainer;

        private void Start()
        {
            mRootElement = GetComponent<UIDocument>().rootVisualElement;

            mPromptElement = mRootElement.Q<Label>("prompt");
            mPromptElement.style.display = DisplayStyle.None;

            mBodyContainer = mRootElement.Q("body-container");
            mBodyContainer.style.display = DisplayStyle.None;
        }
    }
}
