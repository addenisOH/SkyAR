using MoverioBasicFunctionUnityPlugin;
using UnityEngine;

public class InputReconnectController : MonoBehaviour
{
    public void OnClick()
    {
        MoverioInput.Reconnect();
    }
}
