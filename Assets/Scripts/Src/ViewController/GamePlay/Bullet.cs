using UnityEngine;

namespace BrotatoM
{
    public class Bullet : MonoBehaviour
    {
        public float speed;
        public Vector2 direction = new(1f, 0f);
        private Rigidbody2D mRig;

        private void Awake()
        {
            mRig = GetComponent<Rigidbody2D>();
            Destroy(gameObject, 3f);
        }

        private void Start()
        {
            speed = 5;
            var mPlayerTransform = GameObject.FindWithTag("Player").transform;
            var mWeaponTransform = mPlayerTransform.Find("Weapon");
            // 四元数和向量的乘积表示旋转
            mRig.velocity = speed * (mWeaponTransform.rotation * Vector3.right).normalized;
        }
    }
}
