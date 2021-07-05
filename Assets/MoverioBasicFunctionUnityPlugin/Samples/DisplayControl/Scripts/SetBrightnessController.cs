using MoverioBasicFunctionUnityPlugin;
using UnityEngine;
using UnityEngine.UI;

public class SetBrightnessController : MonoBehaviour
{
    private Dropdown brightnessValue;

    void Start()
    {
        var valueDropdownObject = GameObject.Find("SetBrightnessValue");
        brightnessValue = valueDropdownObject.GetComponent<Dropdown>();
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

        var valueText = brightnessValue.options[brightnessValue.value].text;
        if (!int.TryParse(valueText, out int value))
        {
            return;
        }

        if (!MoverioDisplay.SetBrightness(value))
        {
            Debug.LogError("Setting the brightness is failed.");
        }
    }
}
