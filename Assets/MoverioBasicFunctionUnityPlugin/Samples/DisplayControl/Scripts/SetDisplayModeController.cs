using MoverioBasicFunctionUnityPlugin;
using MoverioBasicFunctionUnityPlugin.Type;
using UnityEngine;

public class SetDisplayModeController : MonoBehaviour
{
    public bool isDisplayMode2d;

    public void OnClick()
    {
        if (!MoverioDisplay.IsActive())
        {
            return;
        }

        if (isDisplayMode2d)
        {
            if (!MoverioDisplay.SetDisplayMode(DisplayMode.DISPLAY_MODE_2D))
            {
                Debug.LogError("Setting the display mode is failed.");
            }
        }
        else
        {
            if (!MoverioDisplay.SetDisplayMode(DisplayMode.DISPLAY_MODE_3D))
            {
                Debug.LogError("Setting the display mode is failed.");
            }
        }
    }
}
