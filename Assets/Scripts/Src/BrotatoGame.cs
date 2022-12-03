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
            RegisterModel(new EnemyConfigModel("Configs/ProcessedEnemies"));
            RegisterModel(new ItemConfigModel("Configs/ProcessedItems"));
            RegisterModel(new StatConfigModel("Configs/ProcessedStats"));
            RegisterModel(new CharacterConfigModel("Configs/ProcessedCharacters"));

            // 工具层
            RegisterUtility<IJsonSerializer>(new NewtonsoftJsonSerializer());
        }
    }
}
