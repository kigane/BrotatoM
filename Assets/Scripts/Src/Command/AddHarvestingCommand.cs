using QFramework;

namespace BrotatoM
{
    public class AddHarvestingCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.GetModel<IPlayerModel>().Harvest.Value++;
            this.GetModel<IPlayerModel>().Exp.Value++;
        }
    }
}