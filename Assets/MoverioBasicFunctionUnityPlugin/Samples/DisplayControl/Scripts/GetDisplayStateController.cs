using MoverioBasicFunctionUnityPlugin;
using MoverioBasicFunctionUnityPlugin.Type;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GetDisplayStateController : MonoBehaviour
{
    private Text displayStateValue;

    void Start()
    {
        var textToDisplayStateObject = GameObject.Find("DisplayStateValue");
        displayStateValue = textToDisplayStateObject.GetComponent<Text>();
    }

    public void OnClick()
    {
        if (displayStateValue == null)
        {
            return;
        }

        if (!MoverioDisplay.IsActive())
        {
            return;
        }

        try
        {
            if (MoverioDisplay.GetDisplayState() == DisplayState.DISPLAY_STATE_OFF)
            {
                displayStateValue.text = "Off";
            }
            else
            {
                displayStateValue.text = "On";
            }
        }
        catch (IOException e)
        {
            Debug.LogError("Getting the display state is failed. Message = " + e.Message);
        }
    }
}
