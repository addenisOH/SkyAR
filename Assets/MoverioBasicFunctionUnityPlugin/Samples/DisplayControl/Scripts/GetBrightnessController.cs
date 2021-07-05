using MoverioBasicFunctionUnityPlugin;
using UnityEngine;
using UnityEngine.UI;

public class GetBrightnessController : MonoBehaviour
{
    private Text brightnessValue;

    void Start()
    {
        var textToDisplayObject = GameObject.Find("BrightnessValue");
        brightnessValue = textToDisplayObject.GetComponent<Text>();
    }

    public void OnClick()
    {
        if (brightnessValue == null)
        {
            return;
        }

        if (!MoverioDisplay.IsActive())
        {
            return;
        }

        brightnessValue.text = MoverioDisplay.GetBrightness().ToString();
    }
}
