using QFramework;

namespace BrotatoM
{
    public class WaveOverCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            Log.Debug("On WaveOverCommand");
            var playerSystem = this.GetSystem<IPlayerSystem>();
            playerSystem.CurrWave.Value++;
            this.SendEvent<WaveOverEvent>();
        }
    }
}