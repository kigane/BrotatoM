using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BrotatoM
{
    public class Player : MonoBehaviour
    {
        public float moveSpeed;
        private PlayerControl mPlayerControl;

        private void Awake()
        {
            mPlayerControl = new PlayerControl();
        }

        private void Update()
        {
            var moveSignal = mPlayerControl.Player.Move.ReadValue<Vector2>();
            var currPos = transform.position;
            currPos.x += moveSignal.x * Time.deltaTime * moveSpeed;
            currPos.x = Mathf.Clamp(currPos.x, -14, 14);
            currPos.y += moveSignal.y * Time.deltaTime * moveSpeed;
            currPos.y = Mathf.Clamp(currPos.y, -8, 8);
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
    }
}
