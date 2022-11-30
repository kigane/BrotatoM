using UnityEngine;

namespace BrotatoM
{
    public class Bullet : MonoBehaviour
    {
        public float speed;
        public Vector2 direction = new(1f, 0f);
        private Rigidbody2D mRig;
        private float isRight;

        private void Awake()
        {
            mRig = GetComponent<Rigidbody2D>();
            Destroy(gameObject, 3f);
        }

        private void Start()
        {
            speed = 5;
            var mPlayerTransform = GameObject.FindWithTag("Player").transform;
            var direction = transform.position - mPlayerTransform.position;
            mRig.velocity = speed * direction.normalized;
        }
    }
}
