using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

namespace BrotatoM
{
    public class UIDocumentControl : MonoBehaviour
    {
        private VisualElement mRootElement;
        private void Start()
        {
            mRootElement = GetComponent<UIDocument>().rootVisualElement;
            mRootElement.Query<Label>("NameLabel").ForEach(el =>
            {
                el.text = "波奇酱";
            });
            mRootElement.Query<Label>("NameLabel").AtIndex(1).text = "小孤独";
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // mRootElement.style.display = mRootElement.style.display == DisplayStyle.Flex ? DisplayStyle.None : DisplayStyle.Flex;
                mRootElement.style.backgroundColor = new Color(0, 0, 0);
            }
        }
    }
}
