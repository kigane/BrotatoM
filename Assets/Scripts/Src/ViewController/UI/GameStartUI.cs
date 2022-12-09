using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System;
using QFramework;
using UnityEngine.InputSystem;

namespace BrotatoM
{
    public class GameStartUI : BrotatoGameController
    {
        public UIDocument[] UIPanels;
        private VisualElement mRootElement;
        private readonly Action[] mBtnActions = new Action[4];
        private PlayerControl mPlayerControl;
        private int mCurrPanelIndex = 0;

        private void Awake()
        {
            mPlayerControl = new PlayerControl();
        }

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
                    // Log.Debug(type); // type = MouseOverEvent 即注册的事件
                    Log.Debug(optionsElement.IndexOf(btn));
                    btn.style.backgroundColor = UIColor.WHITE;
                    btn.style.color = UIColor.BLACK;
                });

                btn.RegisterCallback<MouseLeaveEvent>((type) =>
                {
                    btn.style.backgroundColor = UIColor.BLACK;
                    btn.style.color = UIColor.WHITE;
                });

                btn.RegisterCallback<ClickEvent>((type) =>
                {
                    mBtnActions[optionsElement.IndexOf(btn)]();
                });
            });

            this.RegisterEvent<NextPanelEvent>(e =>
            {
                // 0.开始界面 1.选人界面 2.选武器界面 3.难度界面
                if (mCurrPanelIndex + 1 == 4)
                {
                    // this.GetSystem<GameManagerSystem>().State = GameState.PLAY;
                    GameManager.Instance.State = GameState.PLAY;
                    SceneManager.LoadScene("MainScene");
                }
                else
                {
                    ShowUIPanel(mCurrPanelIndex + 1);
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void OnEnable()
        {
            mPlayerControl.Enable();
            mPlayerControl.Player.Return.performed += OnReturn;
        }

        private void OnDisable()
        {
            mPlayerControl.Player.Return.performed -= OnReturn;
        }

        private void OnReturn(InputAction.CallbackContext obj)
        {
            if (mCurrPanelIndex == 0) // 在开始界面按esc
                return;
            else if (mCurrPanelIndex < 0) // 设置界面，和成就界面index分别设为-1, -2
            {
                ShowUIPanel(0);
            }
            else // 1.选人界面 2.选武器界面 3.难度界面
            {
                ShowUIPanel(mCurrPanelIndex - 1);
            }
        }

        private void OnClickStartBtn()
        {
            ShowUIPanel(1);
        }

        private void OnClickSettingsBtn()
        {
            Log.Debug("show settings UI");
        }

        private void OnClickProgressBtn()
        {
            Log.Debug("show Progress UI");
        }

        private void OnClickQuitBtn()
        {
            Log.Debug("quit");
            // 退出游戏
#if UNITY_EDITOR // 编辑器环境
            UnityEditor.EditorApplication.isPlaying = false;
#else //打包出来的环境下
            Application.Quit();
#endif
        }

        private void ShowUIPanel(int index)
        {
            for (int i = 0; i < UIPanels.Length; i++)
            {
                if (index == i)
                {
                    UIPanels[i].sortingOrder = 9;
                }
                else
                {
                    UIPanels[i].sortingOrder = 0;
                }
            }

            mCurrPanelIndex = index;
        }
    }
}
