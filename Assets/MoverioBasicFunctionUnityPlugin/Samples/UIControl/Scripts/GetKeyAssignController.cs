using MoverioBasicFunctionUnityPlugin;
using MoverioBasicFunctionUnityPlugin.Type;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GetKeyAssignController : MonoBehaviour
{
    private Text keycodeValue;

    void Start()
    {
        var textToDisplayObject = GameObject.Find("GetKeycodeValue");
        keycodeValue = textToDisplayObject.GetComponent<Text>();
    }

    public void OnClick()
    {

        if (keycodeValue == null)
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
            Debug.LogError("Getting the key assign is failed.");
            keycodeValue.text = "-";
            return;
        }

        try
        {
            var getValue = MoverioUI.GetKeyAssign(key);
            keycodeValue.text = getValue.ToString();
        }
        catch (IOException e)
        {
            keycodeValue.text = "-";
            Debug.LogError("Getting the key assign is failed. Message = " + e.Message);
        }
    }

    private string GetPhysicalKeyText()
    {
        var valueDropdownObject = GameObject.Find("KeyAssignPhysicalKeyValue");
        var physicalKeyValue = valueDropdownObject.GetComponent<Dropdown>();
        return physicalKeyValue.options[physicalKeyValue.value].text;
    }
}
