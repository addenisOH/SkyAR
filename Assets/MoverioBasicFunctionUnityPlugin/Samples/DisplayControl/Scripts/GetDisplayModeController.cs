using MoverioBasicFunctionUnityPlugin;
using MoverioBasicFunctionUnityPlugin.Type;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GetDisplayModeController : MonoBehaviour
{
    private Text displayModeValue;

    void Start()
    {
        var textToDisplayObject = GameObject.Find("DisplayModeValue");
        displayModeValue = textToDisplayObject.GetComponent<Text>();
    }

    public void OnClick()
    {
        if (displayModeValue == null)
        {
            return;
        }

        if (!MoverioDisplay.IsActive())
        {
            return;
        }

        try
        {
            if (MoverioDisplay.GetDisplayMode() == DisplayMode.DISPLAY_MODE_2D)
            {
                displayModeValue.text = "2D";
            }
            else
            {
                displayModeValue.text = "3D";
            }
        }
        catch (IOException e)
        {
            Debug.LogError("Getting the display mode is failed. Message = " + e.Message);
        }
    }
}
