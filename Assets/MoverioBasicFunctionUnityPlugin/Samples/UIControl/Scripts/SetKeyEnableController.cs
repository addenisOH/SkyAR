using MoverioBasicFunctionUnityPlugin;
using MoverioBasicFunctionUnityPlugin.Type;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SetKeyEnableController : MonoBehaviour
{
    public bool isKeyEnableModeEnable;

    public void OnClick()
    {
        if (!MoverioUI.IsActive())
        {
            return;
        }

        var physicalKeyStr = GetPhysicalKeyText();
        if (!(Enum.TryParse(physicalKeyStr, out PhysicalKey key) && Enum.IsDefined(typeof(PhysicalKey), key)))
        {
            Debug.LogError("Setting the key enabling is failed.");
            return;
        }

        if (!MoverioUI.SetKeyEnable(key, isKeyEnableModeEnable))
        {
            Debug.LogError("Setting the key enabling is failed.");
        }
    }

    private string GetPhysicalKeyText()
    {
        var valueDropdownObject = GameObject.Find("KeyEnablePhysicalKeyValue");
        var physicalKeyValue = valueDropdownObject.GetComponent<Dropdown>();
        return physicalKeyValue.options[physicalKeyValue.value].text;
    }
}
