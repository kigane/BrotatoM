using QFramework;

namespace BrotatoM
{
    public class BrotatoGame : Architecture<BrotatoGame>
    {
        protected override void Init()
        {
            RegisterModel<IPlayerModel>(new PlayerModel());
            RegisterSystem<ITimeSystem>(new TimeSystem());
        }
    }
}
