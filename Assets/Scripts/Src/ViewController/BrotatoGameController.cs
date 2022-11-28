using UnityEngine;
using QFramework;

namespace BrotatoM
{
    public abstract class BrotatoGameController : MonoBehaviour, IController
    {
        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return BrotatoGame.Interface;
        }
    }
}