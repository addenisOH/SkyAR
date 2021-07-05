using MoverioBasicFunctionUnityPlugin;
using UnityEngine;

public class CameraReconnectController : MonoBehaviour
{
    public void OnClick()
    {
        MoverioCamera.Reconnect();
    }
}
