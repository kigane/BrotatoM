using QFramework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace BrotatoM
{
    public class GameOverUI : BaseUI
    {
        private ITimeSystem mTimeSystem;
        private IPlayerSystem mPlayerSystem;

        private void Awake()
        {
            mTimeSystem = this.GetSystem<ITimeSystem>();
            mPlayerSystem = this.GetSystem<IPlayerSystem>();
        }

        protected override void OnUIEnable()
        {
            mRootElement.Q<Button>("return-btn").clickable.clicked += OnReturn;
        }

        private void OnReturn()
        {
            Log.Info("返回主界面", 16);
            SceneManager.LoadScene("GameStartScene");
            mTimeSystem.ClearAllTasks();
            mTimeSystem.Resume();
            GameManagerSystem.Instance.State = GameState.PLAY;
            Time.timeScale = 1;
            mPlayerSystem.ResetPlayerStat();
            mPlayerSystem.UpgradePoint = 0;
            mPlayerSystem.HarvestBag.Value = 0;
        }
    }
}
