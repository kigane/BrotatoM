using QFramework;

namespace BrotatoM
{
    public class AddHarvestingCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            var playerModel = this.GetModel<IPlayerModel>();
            playerModel.Harvest.Value++;
            playerModel.Exp.Value += 10;

            // 升级
            if (playerModel.Exp.Value >= playerModel.CurrMaxExp.Value)
            {
                var temp = playerModel.CurrMaxExp.Value;
                playerModel.CurrMaxExp.Value *= 1.2f;
                playerModel.Exp.Value -= temp;
                playerModel.UpgradePoint.Value++;
                this.SendEvent<UpgradeEvent>();
            }
        }
    }
}