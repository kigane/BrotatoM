using UnityEngine;

namespace BrotatoM
{
    public class EnemyConfigItem : IConfigItem
    {
        public string Name { get; set; }
        public string Behavior { get; set; }
        public int Health { get; set; }
        public float HpIncreasePerWave { get; set; }
        public int Speed { get; set; }
        public int Damage { get; set; }
        public float DamageIncreasePerWave { get; set; }
        public int CoinsDropped { get; set; }
        public float FoodDropRate { get; set; }
        public float ContainerDropRate { get; set; }
        public int FirstWave { get; set; }
        public int FirstWaveD1 { get; set; }
        public int Id { get; set; }
        public string Path { get; set; }
    }

    public class EnemyConfigModel : BaseConfigModel<EnemyConfigItem>
    {
        public EnemyConfigModel(string path) : base(path)
        {
        }

        protected override void OnInit()
        {
            base.OnInit();
            // VerifyLogs("BabyAlien");
        }

        private void VerifyLogs(string name)
        {
            string msg = "Enemy-" + name + ": (";
            EnemyConfigItem item = mDict[name];
            msg += item.Name + ", ";
            msg += item.Behavior + ", ";
            msg += item.Health + ", ";
            msg += item.HpIncreasePerWave + ", ";
            msg += item.Speed + ", ";
            msg += item.Damage + ", ";
            msg += item.DamageIncreasePerWave + ", ";
            msg += item.CoinsDropped + ", ";
            msg += item.FoodDropRate + ", ";
            msg += item.ContainerDropRate + ", ";
            msg += item.FirstWave + ", ";
            msg += item.FirstWaveD1 + ", ";
            msg += item.Path + ")";
            Log.Debug(msg);
        }
    }
}
