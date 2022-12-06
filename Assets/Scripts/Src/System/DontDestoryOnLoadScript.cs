using UnityEngine;

namespace BrotatoM
{
    public class DontDestroyOnLoadScript : MonoBehaviour
    {
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
