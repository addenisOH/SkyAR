using MoverioBasicFunctionUnityPlugin;
using MoverioBasicFunctionUnityPlugin.Type;
using UnityEngine;

public class SetDisplayStateController : MonoBehaviour
{
    public bool isDisplayStateOff;

    public void OnClick()
    {
        if (!MoverioDisplay.IsActive())
        {
            return;
        }

        if (isDisplayStateOff)
        {
            if (!MoverioDisplay.SetDisplayState(DisplayState.DISPLAY_STATE_OFF))
            {
                Debug.LogError("Setting the display state is failed.");
            }
        }
        else
        {
            if (!MoverioDisplay.SetDisplayState(DisplayState.DISPLAY_STATE_ON))
            {
                Debug.LogError("Setting the display state is failed.");
            }
        }
    }
}
