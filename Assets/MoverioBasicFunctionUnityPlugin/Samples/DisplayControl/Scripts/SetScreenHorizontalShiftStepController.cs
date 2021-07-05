using MoverioBasicFunctionUnityPlugin;
using UnityEngine;
using UnityEngine.UI;

public class SetScreenHorizontalShiftStepController : MonoBehaviour
{
    private Dropdown screenHorizontalShiftStepValue;

    void Start()
    {
        var valueDropdownObject = GameObject.Find("SetScreenHorizontalShiftStepValue");
        screenHorizontalShiftStepValue = valueDropdownObject.GetComponent<Dropdown>();
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

        var valueText = screenHorizontalShiftStepValue.options[screenHorizontalShiftStepValue.value].text;
        if (!int.TryParse(valueText, out int value))
        {
            return;
        }

        if (!MoverioDisplay.SetScreenHorizontalShiftStep(value))
        {
            Debug.LogError("Setting the screen horizontal shift step is failed.");
        }
    }
}
