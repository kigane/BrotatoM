using UnityEngine;
using QFramework;

namespace BrotatoM
{
    public class Harvesting : Collectable
    {
        protected override void OnStart()
        {
            this.RegisterEvent<CountDownOverEvent>(e =>
            {
                mCanCollect = true;
            });
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                // 收获和经验+1或+2
                this.SendCommand(new AddHarvestingCommand(1));

                // 销毁
                Destroy(gameObject);
            }
        }
    }
}
