using MoverioBasicFunctionUnityPlugin;
using UnityEngine;
using UnityEngine.UI;

public class HeadsetAttachedAndDetachedController : MonoBehaviour
{
    private Text headsetAttachedAndDetachedValue;

    void Start()
    {
        var textToDisplayObject = GameObject.Find("HeadsetAttachedAndDetachedValue");
        headsetAttachedAndDetachedValue = textToDisplayObject.GetComponent<Text>();
    }

    public void OnClick()
    {
        if (headsetAttachedAndDetachedValue == null)
        {
            return;
        }

        headsetAttachedAndDetachedValue.text = MoverioInfo.IsHeadsetAttached() ? "Attached" : "Detached";
    }
}
