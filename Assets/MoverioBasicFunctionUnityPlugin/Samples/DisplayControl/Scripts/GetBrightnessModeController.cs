using MoverioBasicFunctionUnityPlugin;
using MoverioBasicFunctionUnityPlugin.Type;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GetBrightnessModeController : MonoBehaviour
{
    private Text brightnessModeValue;

    void Start()
    {
        var textToDisplayObject = GameObject.Find("BrightnessModeValue");
        brightnessModeValue = textToDisplayObject.GetComponent<Text>();
    }

    public void OnClick()
    {
        if (brightnessModeValue == null)
        {
            return;
        }

        if (!MoverioDisplay.IsActive())
        {
            return;
        }

        try
        {
            if (MoverioDisplay.GetBrightnessMode() == BrightnessMode.BRIGHTNESS_MODE_MANUAL)
            {
                brightnessModeValue.text = "Manual";
            }
            else
            {
                brightnessModeValue.text = "Auto";
            }
        }
        catch (IOException e)
        {
            Debug.LogError("Getting the brightness mode is failed. Message = " + e.Message);
        }
    }
}
