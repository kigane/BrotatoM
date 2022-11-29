using UnityEngine;

namespace BrotatoM
{
    public class Camera : MonoBehaviour
    {
        private void LateUpdate()
        {
            var playerGO = GameObject.FindWithTag("Player");
            if (!playerGO)
                return;

            Transform playerTransform = playerGO.transform;

            var camPos = playerTransform.position;
            camPos.x = Mathf.Clamp(camPos.x, -6, 6);
            camPos.y = Mathf.Clamp(camPos.y, -4, 4);
            camPos.z = transform.position.z;

            transform.position = camPos;
        }
    }
}
