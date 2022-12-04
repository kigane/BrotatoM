using QFramework;

namespace BrotatoM
{
    public class AddHarvestingCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            var playerModel = this.GetModel<IPlayerModel>();
            playerModel.Harvest.Value++;
            playerModel.Exp.Value++;

            // 升级
            if (playerModel.Exp.Value >= playerModel.CurrMaxExp.Value)
            {
                playerModel.Exp.Value -= playerModel.CurrMaxExp.Value;
                playerModel.CurrMaxExp.Value *= 1.2f;
                playerModel.UpgradePoint.Value++;
                this.SendEvent<UpgradeEvent>();
            }
        }
    }
}