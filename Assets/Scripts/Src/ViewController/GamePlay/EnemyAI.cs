using UnityEngine;

namespace BrotatoM
{
    public class EnemyAI : MonoBehaviour
    {
        public float moveSpeed;
        private Transform mPlayerTransform;

        private void Start()
        {
            mPlayerTransform = GameObject.FindWithTag("Player").transform;
        }

        private void Update()
        {
            MoveTo(transform, mPlayerTransform, moveSpeed);
        }

        private void MoveTo(Transform source, Transform target, float moveSpeed)
        {
            var moveDir = (target.position - source.position).normalized;
            ChangeDirection(moveDir);
            var currPos = transform.position;
            currPos.x += moveDir.x * Time.deltaTime * moveSpeed;
            currPos.y += moveDir.y * Time.deltaTime * moveSpeed;
            transform.position = currPos;
        }

        private void ChangeDirection(Vector3 moveDir)
        {
            if (moveDir.x * transform.localScale.x < 0)
            {
                var localScale = transform.localScale;
                localScale.x *= -1;
                transform.localScale = localScale;
            }
        }
    }
}
