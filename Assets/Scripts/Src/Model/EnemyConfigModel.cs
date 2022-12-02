using System.Collections.Generic;
using QFramework;

namespace BrotatoM
{
    public interface IEnemyConfigModel : IModel
    {
        EnemyConfigItem GetEnemyConfigItemByName(string name);
    }

    public class EnemyConfigItem : IConfigItem
    {
        public string Name { get; set; }
        public string Behavior { get; set; }
        public int Health { get; set; }
        public int HpIncreasePerWave { get; set; }
        public int Speed { get; set; }
        public int Damage { get; set; }
        public int DamageIncreasePerWave { get; set; }
        public int CoinsDropped { get; set; }
        public float FoodDropRate { get; set; }
        public float ContainerDropRate { get; set; }
        public int FirstWave { get; set; }
        public int FirstWaveD1 { get; set; }
        public int Id { get; set; }
        public string Path { get; set; }
    }

    public class EnemyConfigModel : AbstractModel, IEnemyConfigModel
    {
        private readonly Dictionary<string, EnemyConfigItem> mEnemies = new();

        protected override void OnInit()
        {
            this.GetUtility<IJsonSerializer>().ReadJsonToDictionary("Configs/ProcessedEnemies", mEnemies);
        }

        public EnemyConfigItem GetEnemyConfigItemByName(string name)
        {
            return mEnemies[name];
        }
    }
}
