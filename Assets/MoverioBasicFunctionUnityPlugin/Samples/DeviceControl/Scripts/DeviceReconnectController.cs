using MoverioBasicFunctionUnityPlugin;
using UnityEngine;

public class DeviceReconnectController : MonoBehaviour
{
    public void OnClick()
    {
        MoverioInfo.Reconnect();
    }
}
