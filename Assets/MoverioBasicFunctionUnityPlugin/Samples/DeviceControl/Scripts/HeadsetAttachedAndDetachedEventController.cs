using UnityEngine;
using UnityEngine.UI;

public class HeadsetAttachedAndDetachedEventController : MonoBehaviour
{
    private Text headsetAttachedAndDetachedEventValue;

    void Start()
    {
        var textToDisplayObject = GameObject.Find("HeadsetAttachedAndDetachedEventValue");
        headsetAttachedAndDetachedEventValue = textToDisplayObject.GetComponent<Text>();
    }

    public void OnHeadsetAttached()
    {
        headsetAttachedAndDetachedEventValue.text = "Attached";
    }

    public void OnHeadsetDetached()
    {
        headsetAttachedAndDetachedEventValue.text = "Detached";
    }
}
