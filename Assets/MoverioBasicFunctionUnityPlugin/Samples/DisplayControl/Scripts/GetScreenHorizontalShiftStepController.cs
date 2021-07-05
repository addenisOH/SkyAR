using MoverioBasicFunctionUnityPlugin;
using UnityEngine;
using UnityEngine.UI;

public class GetScreenHorizontalShiftStepController : MonoBehaviour
{
    private Text screenHorizontalShiftStepValue;

    void Start()
    {
        var textToDisplayObject = GameObject.Find("ScreenHorizontalShiftStepValue");
        screenHorizontalShiftStepValue = textToDisplayObject.GetComponent<Text>();
    }

    public void OnClick()
    {
        if (screenHorizontalShiftStepValue == null)
        {
            return;
        }

        if (!MoverioDisplay.IsActive())
        {
            return;
        }

        screenHorizontalShiftStepValue.text = MoverioDisplay.GetScreenHorizontalShiftStep().ToString();
    }
}
