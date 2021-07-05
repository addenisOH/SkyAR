using MoverioBasicFunctionUnityPlugin;
using System;
using System.IO;
using UnityEngine;

public class VideoRecordController : MonoBehaviour
{
    public bool isStartRecord;

    private CameraSceneDirector cameraSceneDirector;

    void Awake()
    {
        var cameraSceneDirectorObj = GameObject.Find("CameraSceneDirector");
        cameraSceneDirector = cameraSceneDirectorObj.GetComponent<CameraSceneDirector>();
    }

    public void OnClick()
    {
        if (!MoverioCamera.IsActive())
        {
            return;
        }

        if (isStartRecord)
        {
            cameraSceneDirector.StartRecord();

            if (!MoverioCamera.StartRecord(Path.Combine(GetDirectoryOutputVideo(), DateTime.Now.ToString("yyyyMMddhhmmss") + ".mp4")))
            {
                Debug.LogError("Start record is failed.");
                cameraSceneDirector.RecordStopped();
            }
        }
        else
        {
            cameraSceneDirector.StopRecord();

            MoverioCamera.StopRecord();
        }
    }

    public void OnRecordStarted()
    {
        cameraSceneDirector.RecordStarted();
    }

    public void OnRecordStopped()
    {
        cameraSceneDirector.RecordStopped();
    }

    private string GetDirectoryOutputVideo()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            using (var player = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                using (var context = player.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    using (var picturesDirFile = context.Call<AndroidJavaObject>("getExternalFilesDir", "Movies"))
                    {
                        return picturesDirFile.Call<string>("getAbsolutePath");
                    }
                }
            }
        }
        else
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}
