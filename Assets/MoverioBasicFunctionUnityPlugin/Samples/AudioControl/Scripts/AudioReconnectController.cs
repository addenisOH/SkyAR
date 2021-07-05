using MoverioBasicFunctionUnityPlugin;
using UnityEngine;

public class AudioReconnectController : MonoBehaviour
{
    public void OnClick()
    {
        MoverioAudio.Reconnect();
    }
}
