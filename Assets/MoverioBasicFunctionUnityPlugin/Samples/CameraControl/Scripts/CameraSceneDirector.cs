using UnityEngine;
using UnityEngine.UI;

public class CameraSceneDirector : MonoBehaviour
{
    private Button takePicture;
    private Button startRecord;
    private Button stopRecord;

    void Awake()
    {
        var takePictureObj = GameObject.Find("TakePicture");
        takePicture = takePictureObj.GetComponent<Button>();

        var startRecordObj = GameObject.Find("StartRecord");
        startRecord = startRecordObj.GetComponent<Button>();

        var stopRecordObj = GameObject.Find("StopRecord");
        stopRecord = stopRecordObj.GetComponent<Button>();

        SetNormalMode();
    }

    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    void OnDestroy()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }

    public void TakePicture()
    {
        SetWaitMode();
    }

    public void PictureCompleted()
    {
        SetNormalMode();
    }

    public void StartRecord()
    {
        SetWaitMode();
    }

    public void RecordStarted()
    {
        SetRecodingMode();
    }

    public void StopRecord()
    {
        SetWaitMode();
    }

    public void RecordStopped()
    {
        SetNormalMode();
    }

    private void SetNormalMode()
    {
        takePicture.interactable = true;
        startRecord.interactable = true;
        stopRecord.interactable = false;
    }

    private void SetWaitMode()
    {
        takePicture.interactable = false;
        startRecord.interactable = false;
        stopRecord.interactable = false;
    }

    private void SetRecodingMode()
    {
        takePicture.interactable = false;
        startRecord.interactable = false;
        stopRecord.interactable = true;
    }
}
