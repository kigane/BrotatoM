using QFramework;
using UnityEngine;

namespace BrotatoM
{
    public class HurtPlayerCommand : AbstractCommand
    {
        private readonly float mDamage;

        public HurtPlayerCommand(float damage)
        {
            mDamage = damage;
        }

        protected override void OnExecute()
        {
            var playerSystem = this.GetSystem<IPlayerSystem>();
            playerSystem.HP.Value -= mDamage;
        }
    }
}