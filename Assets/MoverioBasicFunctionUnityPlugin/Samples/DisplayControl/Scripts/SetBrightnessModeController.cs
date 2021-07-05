using MoverioBasicFunctionUnityPlugin;
using MoverioBasicFunctionUnityPlugin.Type;
using UnityEngine;

public class SetBrightnessModeController : MonoBehaviour
{
    public bool isBrightnessModeManual;

    public void OnClick()
    {
        if (!MoverioDisplay.IsActive())
        {
            return;
        }

        if (isBrightnessModeManual)
        {
            if (!MoverioDisplay.SetBrightnessMode(BrightnessMode.BRIGHTNESS_MODE_MANUAL))
            {
                Debug.LogError("Setting the brightness mode is failed.");
            }
        }
        else
        {
            if (!MoverioDisplay.SetBrightnessMode(BrightnessMode.BRIGHTNESS_MODE_AUTO))
            {
                Debug.LogError("Setting the brightness mode is failed.");
            }
        }
    }
}
