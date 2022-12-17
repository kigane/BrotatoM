using QFramework;

namespace BrotatoM
{
    public class AddHarvestingCommand : AbstractCommand
    {
        public int addAmount;

        public AddHarvestingCommand(int amount)
        {
            addAmount = amount;
        }

        protected override void OnExecute()
        {
            var playerSystem = this.GetSystem<IPlayerSystem>();
            if (GameManagerSystem.Instance.State == GameState.COLLECTING)
            {
                playerSystem.HarvestBag.Value++;
            }
            else
            {
                if (playerSystem.HarvestBag.Value > 0)
                {
                    playerSystem.Harvest.Value += addAmount + 1;
                    playerSystem.HarvestBag.Value--;
                }
                else
                    playerSystem.Harvest.Value += addAmount;
            }
            playerSystem.Exp.Value += 3;

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