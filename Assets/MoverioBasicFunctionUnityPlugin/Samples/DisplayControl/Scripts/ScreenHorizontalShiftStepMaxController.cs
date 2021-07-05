using MoverioBasicFunctionUnityPlugin;
using UnityEngine;
using UnityEngine.UI;

public class ScreenHorizontalShiftStepMaxController : MonoBehaviour
{
    private Text screenHorizontalShiftStepMaxValue;

    void Start()
    {
        var textToDisplayObject = GameObject.Find("ScreenHorizontalShiftStepMaxValue");
        screenHorizontalShiftStepMaxValue = textToDisplayObject.GetComponent<Text>();
    }

    public void OnClick()
    {
        if (screenHorizontalShiftStepMaxValue == null)
        {
            return;
        }

        if (!MoverioDisplay.IsActive())
        {
            return;
        }

        screenHorizontalShiftStepMaxValue.text = MoverioDisplay.GetScreenHorizontalShiftStepMax().ToString();
    }
}
