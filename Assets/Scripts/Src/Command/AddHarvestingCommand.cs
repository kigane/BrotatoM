using QFramework;

namespace BrotatoM
{
    public class AddHarvestingCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            var playerSystem = this.GetSystem<IPlayerSystem>();
            playerSystem.Harvest.Value++;
            playerSystem.Exp.Value += 15;

            // 升级
            if (playerSystem.Exp.Value >= playerSystem.CurrMaxExp.Value)
            {
                var temp = playerSystem.CurrMaxExp.Value;
                playerSystem.CurrMaxExp.Value *= 1.2f;
                playerSystem.Exp.Value -= temp;
                playerSystem.UpgradePoint++;
                this.SendEvent<UpgradeEvent>();
            }
        }
    }
}