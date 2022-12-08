using QFramework;

namespace BrotatoM
{
    public class WaveOverCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.SendEvent<WaveOverEvent>();
        }
    }
}