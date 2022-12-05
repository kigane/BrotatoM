using QFramework;

namespace BrotatoM
{
    public class WaveOverCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            Log.Debug("On WaveOverCommand");
            var playerModel = this.GetModel<IPlayerModel>();
            //TODO 波数+1
            //TODO 收集场上的可收集物

            // 增加收获
            playerModel.Harvest.Value += playerModel.Harvesting.Value;

            // 显示升级界面(根据UpgradePoint)
            if (playerModel.UpgradePoint.Value > 0)
            {
                Log.Debug("显示升级界面", 16);
            }

            // 显示商店界面
            Log.Debug("显示商店界面", 16);
        }
    }
}