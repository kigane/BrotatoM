using UnityEngine;
using QFramework;

namespace BrotatoM
{
    public class Harvesting : BrotatoGameController
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                // 收获和经验+1或+2
                this.SendCommand(new AddHarvestingCommand());

                // 销毁
                Destroy(gameObject);
            }
        }
    }
}
