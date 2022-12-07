using QFramework;

namespace BrotatoM
{
    public class RefreshUpgradeItemsCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.SendEvent<RefreshUpgradeItemsEvent>();
        }
    }
}