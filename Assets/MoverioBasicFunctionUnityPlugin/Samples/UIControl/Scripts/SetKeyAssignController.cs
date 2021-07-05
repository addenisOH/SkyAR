using MoverioBasicFunctionUnityPlugin;
using MoverioBasicFunctionUnityPlugin.Type;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SetKeyAssignController : MonoBehaviour
{
    public void OnClick()
    {
        var keycodeStr = GetKeycodeText();

        if (!(Enum.TryParse(keycodeStr, out KeycodeValue code) && Enum.IsDefined(typeof(KeycodeValue), code)))
        {
            Debug.LogError("Setting the key assign is failed.");
            return;
        }

        var physicalKeyStr = GetPhysicalKeyText();
        if (!(Enum.TryParse(physicalKeyStr, out PhysicalKey key) && Enum.IsDefined(typeof(PhysicalKey), key)))
        {
            Debug.LogError("Setting the key assign is failed.");
            return;
        }

        if (!MoverioUI.SetKeyAssign(key, code))
        {
            Debug.LogError("Setting the key assign is failed.");
        }
    }

    private string GetKeycodeText()
    {
        var valueDropdownObject = GameObject.Find("SetKeycodeValue");
        var keycodeValue = valueDropdownObject.GetComponent<Dropdown>();
        return keycodeValue.options[keycodeValue.value].text;
    }

    private string GetPhysicalKeyText()
    {
        var valueDropdownObject = GameObject.Find("KeyAssignPhysicalKeyValue");
        var physicalKeyValue = valueDropdownObject.GetComponent<Dropdown>();
        return physicalKeyValue.options[physicalKeyValue.value].text;
    }
}
