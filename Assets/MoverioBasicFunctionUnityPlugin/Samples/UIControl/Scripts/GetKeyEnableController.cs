using MoverioBasicFunctionUnityPlugin;
using MoverioBasicFunctionUnityPlugin.Type;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GetKeyEnableController : MonoBehaviour
{
    private Text keyEnableValue;

    void Start()
    {
        var textToDisplayObject = GameObject.Find("GetKeyEnableValue");
        keyEnableValue = textToDisplayObject.GetComponent<Text>();
    }

    public void OnClick()
    {
        if (keyEnableValue == null)
        {
            return;
        }

        if (!MoverioUI.IsActive())
        {
            return;
        }

        var physicalKeyStr = GetPhysicalKeyText();
        if (!(Enum.TryParse(physicalKeyStr, out PhysicalKey key) && Enum.IsDefined(typeof(PhysicalKey), key)))
        {
            Debug.LogError("Getting the key enabling is failed.");
            keyEnableValue.text = "-";
            return;
        }

        try
        {
            if (MoverioUI.GetKeyEnable(key) == KeyEnablingState.KEY_STATE_ENABLE)
            {
                keyEnableValue.text = "Enable";
            }
            else
            {
                keyEnableValue.text = "Disable";
            }
        }
        catch (IOException e)
        {
            Debug.LogError("Getting the display mode is failed. Message = " + e.Message);
            keyEnableValue.text = "-";
        }
    }

    private string GetPhysicalKeyText()
    {
        var valueDropdownObject = GameObject.Find("KeyEnablePhysicalKeyValue");
        var physicalKeyValue = valueDropdownObject.GetComponent<Dropdown>();
        return physicalKeyValue.options[physicalKeyValue.value].text;
    }
}
