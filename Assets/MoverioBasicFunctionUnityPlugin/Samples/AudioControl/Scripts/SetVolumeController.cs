using MoverioBasicFunctionUnityPlugin;
using UnityEngine;
using UnityEngine.UI;

public class SetVolumeController : MonoBehaviour
{
    private Dropdown volumeValue;

    void Start()
    {
        var valueDropdownObject = GameObject.Find("SetVolumeValue");
        volumeValue = valueDropdownObject.GetComponent<Dropdown>();
    }

    public void OnClick()
    {
        if (volumeValue == null)
        {
            return;
        }

        if (!MoverioAudio.IsActive())
        {
            return;
        }

        var valueText = volumeValue.options[volumeValue.value].text;
        if (!int.TryParse(valueText, out int value))
        {
            return;
        }

        // For BT-35E, setting a number between 16 and 33 will return false.
        // If you are using BT-30C and imit mode is on, setting a number between 21 and 33 will return false.
        if (!MoverioAudio.SetVolume(value))
        {
            Debug.LogError("Setting the volume is failed.");
        }
    }
}
