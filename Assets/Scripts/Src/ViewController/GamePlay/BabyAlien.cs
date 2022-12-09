using UnityEngine;
using QFramework;

namespace BrotatoM
{
    public class BabyAlien : Enemy
    {
        private Animator mAnimator;

        private void Awake()
        {
            mAnimator = GetComponent<Animator>();
        }

        protected override void OnStart()
        {
            this.RegisterEvent<CountDownOverEvent>(e =>
            {
                mAnimator.SetBool("IsCountDownOver", true);
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
    }
}
