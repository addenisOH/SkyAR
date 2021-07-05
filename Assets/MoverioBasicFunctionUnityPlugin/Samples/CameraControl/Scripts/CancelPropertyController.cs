using UnityEngine;

public class CancelPropertyController : MonoBehaviour
{
    public GameObject propertyPanel;
    public GameObject cameraPreview;

    public void OnClick()
    {
        cameraPreview.SetActive(true);
        propertyPanel.SetActive(false);
    }
}
