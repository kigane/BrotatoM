using UnityEngine;
using QFramework;

namespace BrotatoM
{
    public class Enemy : BrotatoGameController
    {
        public float moveSpeed;
        protected Transform mPlayerTransform;
        protected float mEnemyHP = 5;
        protected float mDamage = 1;

        protected void Start()
        {
            mPlayerTransform = GameObject.FindWithTag("Player").transform;
        }

        protected void Update()
        {
            MoveTo(transform, mPlayerTransform, moveSpeed);
        }

        protected void OnTriggerEnter2D(Collider2D other)
        {
            // 被攻击
            if (other.CompareTag("Projectile"))
            {
                Destroy(other.gameObject);

                // 受伤
                mEnemyHP -= 4;

                if (mEnemyHP <= 0)
                { // 死亡
                    Destroy(gameObject);
                    //TODO 掉落数量
                    // 掉落位置随机
                    var dropPos = transform.position;
                    dropPos.x += Random.Range(-0.5f, 0.5f);
                    dropPos.y += Random.Range(-0.5f, 0.5f);
                    GameObject mHarvestGO = Resources.Load<GameObject>("Prefabs/Harvesting");
                    Instantiate(mHarvestGO, dropPos, mHarvestGO.transform.rotation);
                }
                else
                { //TODO 击退

                }
            }

            // 攻击玩家
            if (other.CompareTag("Player"))
            {
                this.SendCommand(new HurtPlayerCommand(mDamage));
            }
        }

        /// <summary>
        /// source向target以moveSpeed移动。
        /// </summary>
        /// <param name="source">要移动的物体</param>
        /// <param name="target">目标物体</param>
        /// <param name="moveSpeed">速度</param>
        protected void MoveTo(Transform source, Transform target, float moveSpeed)
        {
            var moveDir = (target.position - source.position).normalized;
            ChangeDirection(moveDir);
            var currPos = transform.position;
            currPos.x += moveDir.x * Time.deltaTime * moveSpeed;
            currPos.y += moveDir.y * Time.deltaTime * moveSpeed;
            transform.position = currPos;
        }

        /// <summary>
        /// 左右移动时翻转图片
        /// </summary>
        /// <param name="moveDir">移动方向</param>
        protected void ChangeDirection(Vector3 moveDir)
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
