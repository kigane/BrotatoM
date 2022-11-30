using UnityEngine;

namespace BrotatoM
{
    public class WeaponRotate : MonoBehaviour
    {
        private Transform mPlayerTransform;
        private GameObject mBullet;
        private float mShouldRotate;
        private float mIsPlayerRight;

        private void Start()
        {
            mPlayerTransform = transform.parent;
            mBullet = transform.Find("Bullet").gameObject;
            // 设置武器方向
            transform.RotateAround(mPlayerTransform.position, Vector3.forward, 123f);
            Shoot();
        }

        private void Update()
        {
            // 主角朝向
            mIsPlayerRight = Mathf.Sign(mPlayerTransform.localScale.x);
            // 武器绕主角旋转
            // transform.RotateAround(mPlayerTransform.position, Vector3.forward, 0.1f);
            // 计算旋转角
            float rotation = Vector3.Angle(Vector3.right, transform.position - mPlayerTransform.position);

            // 旋转到另一边时，在y轴翻转武器的图像。
            if (rotation < 90 && rotation > -90)
            {
                mShouldRotate = 1;
            }
            else
            {
                mShouldRotate = -1;
            }

            if (mShouldRotate * transform.localScale.y < 0)
            {
                var localScale = transform.localScale;
                localScale.y *= -1;
                transform.localScale = localScale;
            }

            // 主角反向时，武器的翻转逻辑也要翻转
            var lScale = transform.localScale;
            lScale.y *= mIsPlayerRight;
            transform.localScale = lScale;
        }

        public void Shoot()
        {
            var bullet = Instantiate(mBullet, mBullet.transform.position, mBullet.transform.rotation);
            // 原来Bullet是Gun的子对象，Instantiate出来的会在根节点，因此缩放需要调整。
            bullet.transform.localScale = mBullet.transform.lossyScale;
            bullet.SetActive(true);
        }
    }
}
