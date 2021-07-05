using MoverioBasicFunctionUnityPlugin;
using UnityEngine;
using UnityEngine.UI;

public class ScreenHorizontalShiftStepMinController : MonoBehaviour
{
    private Text screenHorizontalShiftStepMinValue;

    void Start()
    {
        var textToDisplayObject = GameObject.Find("ScreenHorizontalShiftStepMinValue");
        screenHorizontalShiftStepMinValue = textToDisplayObject.GetComponent<Text>();
    }

    public void OnClick()
    {
        if (screenHorizontalShiftStepMinValue == null)
        {
            return;
        }

        if (!MoverioDisplay.IsActive())
        {
            return;
        }

        screenHorizontalShiftStepMinValue.text = MoverioDisplay.GetScreenHorizontalShiftStepMin().ToString();
    }
}
