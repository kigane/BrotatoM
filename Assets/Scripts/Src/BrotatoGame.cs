using QFramework;

namespace BrotatoM
{
    public class BrotatoGame : Architecture<BrotatoGame>
    {
        protected override void Init()
        {
            // 系统层
            RegisterSystem<ITimeSystem>(new TimeSystem());

            // 模型层
            RegisterModel<IPlayerModel>(new PlayerModel());
            RegisterModel(new WeaponConfigModel("Configs/ProcessedWeapons"));

            // 工具层
            RegisterUtility<IJsonSerializer>(new NewtonsoftJsonSerializer());
        }
    }
}
