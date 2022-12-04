using QFramework;

namespace BrotatoM
{
    public class NextPanelCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.SendEvent<NextPanelEvent>();
        }
    }
}