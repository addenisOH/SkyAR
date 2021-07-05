using MoverioBasicFunctionUnityPlugin;
using UnityEngine;
using UnityEngine.UI;

public class HeadsetSerialNumberController : MonoBehaviour
{
    private Text headsetSerialNumberValue;

    void Start()
    {
        var textToDisplayObject = GameObject.Find("HeadsetSerialNumberValue");
        headsetSerialNumberValue = textToDisplayObject.GetComponent<Text>();
    }

    public void OnClick()
    {
        if (headsetSerialNumberValue == null)
        {
            return;
        }

        if (!MoverioInfo.IsActive())
        {
            return;
        }

        headsetSerialNumberValue.text = MoverioInfo.GetHeadsetSerialNumber();
    }
}
