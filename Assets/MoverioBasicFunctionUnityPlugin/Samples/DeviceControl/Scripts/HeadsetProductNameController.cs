using MoverioBasicFunctionUnityPlugin;
using UnityEngine;
using UnityEngine.UI;

public class HeadsetProductNameController : MonoBehaviour
{
    private Text headsetProductNameValue;

    void Start()
    {
        var textToDisplayObject = GameObject.Find("HeadsetProductNameValue");
        headsetProductNameValue = textToDisplayObject.GetComponent<Text>();
    }

    public void OnClick()
    {
        if (headsetProductNameValue == null)
        {
            return;
        }

        if (!MoverioInfo.IsActive())
        {
            return;
        }

        headsetProductNameValue.text = MoverioInfo.GetHeadsetProductName();
    }
}
