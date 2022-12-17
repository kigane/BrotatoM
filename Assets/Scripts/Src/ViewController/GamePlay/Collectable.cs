using UnityEngine;
using QFramework;

namespace BrotatoM
{
    public abstract class Collectable : BrotatoGameController
    {
        private Transform mPlayerTransform;
        private readonly float mSmoothTime = 0.3f;
        private Vector3 velocity = Vector3.zero;
        protected bool mCanCollect = false;

        private void Start()
        {
            mPlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            OnStart();
        }

        protected abstract void OnStart();

        private void Update()
        {
            if (mCanCollect)
                transform.position = Vector3.SmoothDamp(transform.position, mPlayerTransform.position, ref velocity, mSmoothTime);
        }
    }
}
