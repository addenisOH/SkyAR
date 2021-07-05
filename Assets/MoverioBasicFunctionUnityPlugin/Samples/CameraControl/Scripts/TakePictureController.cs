using MoverioBasicFunctionUnityPlugin;
using System;
using System.IO;
using UnityEngine;

public class TakePictureController : MonoBehaviour
{
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

        cameraSceneDirector.TakePicture();

        if (!MoverioCamera.TakePicture(Path.Combine(GetDirectoryOutputPicture(), DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpg")))
        {
            Debug.LogError("Take picture is failed.");
            cameraSceneDirector.PictureCompleted();
        }
    }

    public void OnPictureCompleted()
    {
        cameraSceneDirector.PictureCompleted();
    }

    private string GetDirectoryOutputPicture()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            using (var player = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                using (var context = player.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    using (var picturesDirFile = context.Call<AndroidJavaObject>("getExternalFilesDir", "Pictures"))
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
