using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

namespace BrotatoM
{
    public class UIDocumentControl1 : MonoBehaviour
    {
        private VisualElement mRootElement;
        private float mIntensity;
        private void Start()
        {
            mRootElement = GetComponent<UIDocument>().rootVisualElement.Q("Root");
            mIntensity = 0f;
            mRootElement.style.backgroundColor = new Color(1, 1, 1, mIntensity);
            StartCoroutine(Fade());
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // mRootElement.style.display = mRootElement.style.display == DisplayStyle.Flex ? DisplayStyle.None : DisplayStyle.Flex;
                mRootElement.style.backgroundColor = new Color(0, 0, 0);
            }
        }

        private IEnumerator Fade()
        {
            while (mIntensity < 1.0f)
            {
                // 前几帧比较费时，第三帧花了0.28s。
                mIntensity = Mathf.Lerp(mIntensity, 1.0f, Time.deltaTime * 2f);
                mRootElement.style.backgroundColor = new Color(1, 1, 1, mIntensity);
                yield return null;
            }
        }
    }
}
