using UnityEngine;

namespace BrotatoM
{
    public class Weapon : MonoBehaviour
    {
        private Transform mEnemyTransform;
        private GameObject mBullet;
        public float mRange;
        private float mShouldRotate;
        private float mCooldown = 0.5f;
        private bool needCooldown = false;

        private void Start()
        {
            mBullet = transform.Find("Bullet").gameObject;
        }

        private void Update()
        {
            mCooldown -= Time.deltaTime;

            if (mCooldown <= 0)
            {
                needCooldown = false;
                mCooldown = 0.5f;
            }

            if (Input.GetKeyDown(KeyCode.Space) && !needCooldown)
            {
                Shoot();
                needCooldown = true;
            }

            RotateWeapon();
        }

        public void Shoot()
        {
            // 生成子弹
            var bullet = Instantiate(mBullet, mBullet.transform.position, mBullet.transform.rotation);
            // 原来Bullet是Gun的子对象，Instantiate出来的会在根节点，因此缩放需要调整。
            bullet.transform.localScale = mBullet.transform.lossyScale;
            bullet.SetActive(true);
            // 直接在这里设置子弹速度和方向
            var rig = bullet.GetComponent<Rigidbody2D>();
            rig.velocity = 6 * (transform.rotation * Vector3.right).normalized;
            Destroy(bullet, 3f);
        }

        /// <summary>
        /// 旋转武器，指向最近的敌人
        /// </summary>
        private void RotateWeapon()
        {
            // 计算朝向
            mEnemyTransform = FindNearestEnemyInRange(mRange);
            if (!mEnemyTransform)
            {
                // 回到原位
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                if (transform.localScale.y < 0) // 如果翻转了，翻转回来
                {
                    var localScale = transform.localScale;
                    localScale.y *= -1;
                    transform.localScale = localScale;
                }
                return;
            }

            var targetDir = mEnemyTransform.position - transform.position;
            float angle;
            if (targetDir.x > 0)
            {
                // Vector3.Angle的结果为(0~180)
                angle = Vector3.Angle(Vector3.right, targetDir) * Mathf.Sign(targetDir.y);
            }
            else
            {
                angle = Vector3.Angle(Vector3.left, targetDir);
                angle = (180 - angle) * Mathf.Sign(targetDir.y);
            }
            // Debug.Log(angle);
            // 旋转武器
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            // 翻转y轴
            mShouldRotate = (angle < 90 && angle > -90) ? -1 : 1;
            if (mShouldRotate * transform.lossyScale.y > 0) // 在左侧且未翻转
            {
                var localScale = transform.localScale;
                localScale.y *= -1;
                transform.localScale = localScale;
            }
        }

        /// <summary>
        /// 找到离武器最近的敌人
        /// 所有敌人都挂在Enemy对象下
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        private Transform FindNearestEnemyInRange(float range)
        {
            var enemyTransform = GameObject.FindWithTag("Enemy").transform;
            int nearestIndex = -1;
            float minDistance = float.MaxValue;
            float distance;

            if (enemyTransform.childCount == 0) // 没有敌人
                return null;

            for (int i = 0; i < enemyTransform.childCount; i++)
            {
                distance = Vector3.Distance(transform.position, enemyTransform.GetChild(i).position);
                if (distance < range && distance < minDistance)
                { // 在预警范围内找最近
                    minDistance = distance;
                    nearestIndex = i;
                }
            }

            if (nearestIndex < 0) // 范围内没有目标
                return null;

            return enemyTransform.GetChild(nearestIndex);
        }
    }
}
