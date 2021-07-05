using MoverioBasicFunctionUnityPlugin;
using MoverioBasicFunctionUnityPlugin.Type;
using UnityEngine;

public class CameraPreviewController : MonoBehaviour
{
    private void OnEnable()
    {
        if (!MoverioCamera.IsActive())
        {
            return;
        }

        var property = MoverioCamera.GetProperty();
        if (property == null)
        {
            return;
        }

        var rectTransform = gameObject.GetComponent<RectTransform>();
        var size = rectTransform.sizeDelta;

        switch (property.CaptureSize)
        {
            case CaptureSize.CAPTURE_SIZE_640x480:
            case CaptureSize.CAPTURE_SIZE_2592x1944:
                size.y = 1080;
                break;
            case CaptureSize.CAPTURE_SIZE_1280x720:
            case CaptureSize.CAPTURE_SIZE_1920x1080:
                size.y = 810;
                break;
        }

        rectTransform.sizeDelta = size;
    }
}
