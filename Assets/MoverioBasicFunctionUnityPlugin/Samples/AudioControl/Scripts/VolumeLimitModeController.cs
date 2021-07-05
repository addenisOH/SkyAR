using MoverioBasicFunctionUnityPlugin;
using MoverioBasicFunctionUnityPlugin.Type;
using UnityEngine;

public class VolumeLimitModeController : MonoBehaviour
{
    public bool isVolumeLimitMode;

    public void OnClick()
    {
        if (!MoverioAudio.IsActive())
        {
            return;
        }

        if (isVolumeLimitMode)
        {
            if (!MoverioAudio.SetVolumeLimitMode(VolumeLimitMode.VOLUME_LIMIT_MODE_ON))
            {
                Debug.LogError("Setting the volume limit mode is failed.");
            }
        }
        else
        {
            if (!MoverioAudio.SetVolumeLimitMode(VolumeLimitMode.VOLUME_LIMIT_MODE_OFF))
            {
                Debug.LogError("Setting the volume limit mode is failed.");
            }
        }
    }
}
