using QFramework;

namespace BrotatoM
{
    public class NextWaveCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.GetSystem<IPlayerSystem>().CurrWave.Value++;
            this.SendEvent<NextWaveEvent>();
        }
    }
}