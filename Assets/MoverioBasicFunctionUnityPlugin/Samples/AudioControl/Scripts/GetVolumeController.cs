using MoverioBasicFunctionUnityPlugin;
using UnityEngine;
using UnityEngine.UI;

public class GetVolumeController : MonoBehaviour
{
    private Text volumeValue;

    void Start()
    {
        var textToDisplayObject = GameObject.Find("VolumeValue");
        volumeValue = textToDisplayObject.GetComponent<Text>();
    }

    public void OnClick()
    {
        if (volumeValue == null)
        {
            return;
        }

        if (!MoverioAudio.IsActive())
        {
            return;
        }

        volumeValue.text = MoverioAudio.GetVolume().ToString();
    }
}
