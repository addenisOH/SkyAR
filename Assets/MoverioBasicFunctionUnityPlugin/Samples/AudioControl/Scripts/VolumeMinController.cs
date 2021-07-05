using MoverioBasicFunctionUnityPlugin;
using UnityEngine;
using UnityEngine.UI;

public class VolumeMinController : MonoBehaviour
{
    private Text volumeMinValue;

    void Start()
    {
        var textToDisplayObject = GameObject.Find("VolumeMinValue");
        volumeMinValue = textToDisplayObject.GetComponent<Text>();
    }

    public void OnClick()
    {
        if (volumeMinValue == null)
        {
            return;
        }

        if (!MoverioAudio.IsActive())
        {
            return;
        }

        volumeMinValue.text = MoverioAudio.GetVolumeMin().ToString();
    }
}
