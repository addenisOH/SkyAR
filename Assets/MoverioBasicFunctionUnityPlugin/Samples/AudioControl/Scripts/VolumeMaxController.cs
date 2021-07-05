using MoverioBasicFunctionUnityPlugin;
using UnityEngine;
using UnityEngine.UI;

public class VolumeMaxController : MonoBehaviour
{
    private Text volumeMaxValue;

    void Start()
    {
        var textToDisplayObject = GameObject.Find("VolumeMaxValue");
        volumeMaxValue = textToDisplayObject.GetComponent<Text>();
    }

    public void OnClick()
    {
        if (volumeMaxValue == null)
        {
            return;
        }

        if (!MoverioAudio.IsActive())
        {
            return;
        }

        volumeMaxValue.text = MoverioAudio.GetVolumeMax().ToString();
    }
}
