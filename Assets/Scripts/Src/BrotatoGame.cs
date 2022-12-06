using QFramework;

namespace BrotatoM
{
    public class BrotatoGame : Architecture<BrotatoGame>
    {
        protected override void Init()
        {
            // 系统层
            RegisterSystem<ITimeSystem>(new TimeSystem());
            RegisterSystem<IPlayerSystem>(new PlayerSystem());

            // 模型层
            RegisterModel(new WeaponConfigModel("Configs/ProcessedWeapons"));
            RegisterModel(new EnemyConfigModel("Configs/ProcessedEnemies"));
            RegisterModel(new ItemConfigModel("Configs/ProcessedItems"));
            RegisterModel(new StatConfigModel("Configs/ProcessedStats"));
            RegisterModel(new CharacterConfigModel("Configs/ProcessedCharacters"));
            RegisterModel(new DangerConfigModel("Configs/ProcessedDangers"));
            RegisterModel(new UpgradeConfigModel("Configs/ProcessedUpgrades"));

            // 工具层
            RegisterUtility<IJsonSerializer>(new NewtonsoftJsonSerializer());
        }
    }
}
