using System;
using UnityEngine;
using UnityEngine.InputSystem;
using QFramework;

namespace BrotatoM
{
    public class Player : BrotatoGameController
    {
        public float moveSpeed;
        private PlayerControl mPlayerControl;
        private SpriteRenderer mSpriteRenderer;
        private bool mFlipped;
        private IPlayerSystem mPlayerSystem;
        public Vector3[] weaponLocations;

        private void Awake()
        {
            mPlayerSystem = this.GetSystem<IPlayerSystem>();
            mPlayerControl = new PlayerControl();

            // 人物贴图
            mSpriteRenderer = GetComponent<SpriteRenderer>();
            mSpriteRenderer.sprite = Resources.Load<Sprite>(this.SendQuery(new CharacterSpritePathQuery(mPlayerSystem.CharacterId)));

            // 生成初始武器
            GenerateWeapons();

            //TODO 监听武器变更事件
        }

        private void Update()
        {
            // 移动逻辑
            var moveSignal = mPlayerControl.Player.Move.ReadValue<Vector2>();
            var currPos = transform.position;
            currPos.x += moveSignal.x * Time.deltaTime * moveSpeed;
            currPos.x = Mathf.Clamp(currPos.x, -14, 14);
            currPos.y += moveSignal.y * Time.deltaTime * moveSpeed;
            currPos.y = Mathf.Clamp(currPos.y, -8, 8);
            ChangeDirection(moveSignal);
            transform.position = currPos;
        }

        private void OnEnable()
        {
            mPlayerControl.Enable();
        }

        private void OnDisable()
        {
            mPlayerControl.Disable();
        }

        private void GenerateWeapons()
        {
            GameObject weaponPrefab = Resources.Load<GameObject>("Prefabs/Weapon");
            for (int i = 0; i < mPlayerSystem.CurrWeapons.Count; i++)
            {
                WeaponInfo weaponInfo = mPlayerSystem.CurrWeapons[i];
                var weapon = Instantiate(weaponPrefab, transform.position, Quaternion.identity, transform);
                weapon.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                weapon.transform.localPosition = weaponLocations[i];
                weapon.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(weaponInfo.Path);
                Log.Debug($"{weaponInfo.Name}: {weaponInfo.Range} Range");
                weapon.GetComponent<Weapon>().range = weaponInfo.Range;
            }
        }

        private void ChangeDirection(Vector2 moveDir)
        {
            if (moveDir.x < 0)
            {
                mSpriteRenderer.flipX = true;
                mFlipped = true;
            }
            else if (moveDir.x > 0)
            {
                if (mFlipped)
                {
                    mSpriteRenderer.flipX = false;
                    mFlipped = false;
                }
            }
        }
    }
}
