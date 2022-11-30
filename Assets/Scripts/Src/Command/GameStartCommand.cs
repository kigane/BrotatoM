using QFramework;

namespace BrotatoM
{
    public class GameStartCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.SendEvent<GameStartEvent>();
        }
    }
}