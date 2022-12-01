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
            var playerModel = this.GetModel<IPlayerModel>();
            playerModel.HP.Value -= mDamage;
        }
    }
}