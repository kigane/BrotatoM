using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System;
using QFramework;

namespace BrotatoM
{
    public class GameStartUI : BrotatoGameController
    {
        private VisualElement mRootElement;
        private readonly Action[] mBtnActions = new Action[4];
        public UIDocument characterSelectUI;

        private void Start()
        {
            mBtnActions[0] = OnClickStartBtn;
            mBtnActions[1] = OnClickSettingsBtn;
            mBtnActions[2] = OnClickProgressBtn;
            mBtnActions[3] = OnClickQuitBtn;

            mRootElement = GetComponent<UIDocument>().rootVisualElement;
            var optionsElement = mRootElement.Q("options");
            mRootElement.Query<Button>().ForEach(btn =>
            {
                // :hover 的替代方案。 鼠标移动上去变成白底黑字，离开则恢复为黑底白字。
                btn.RegisterCallback<MouseOverEvent>((type) =>
                {
                    // Debug.Log(type); // type = MouseOverEvent 即注册的事件
                    Debug.Log(optionsElement.IndexOf(btn));
                    btn.style.backgroundColor = new Color(1f, 1f, 1f, 0.8f);
                    btn.style.color = new Color(0, 0, 0, 0.8f);
                });

                btn.RegisterCallback<MouseLeaveEvent>((type) =>
                {
                    btn.style.backgroundColor = new Color(0, 0, 0, 0.8f);
                    btn.style.color = new Color(1f, 1f, 1f, 0.8f);
                });

                btn.RegisterCallback<ClickEvent>((type) =>
                {
                    mBtnActions[optionsElement.IndexOf(btn)]();
                });
            });
        }

        private void OnClickStartBtn()
        {
            // characterSelectUI.SetActive(true);
            characterSelectUI.sortingOrder = 6;
            // SceneManager.LoadScene("MainScene");
        }

        private void OnClickSettingsBtn()
        {
            Debug.Log("show settings UI");
        }

        private void OnClickProgressBtn()
        {
            Debug.Log("show Progress UI");
        }

        private void OnClickQuitBtn()
        {
            Debug.Log("quit");
            // 退出游戏
#if UNITY_EDITOR // 编辑器环境
            UnityEditor.EditorApplication.isPlaying = false;
#else //打包出来的环境下
            Application.Quit();
#endif
        }
    }
}
