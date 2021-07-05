using UnityEngine;

public class ShowPropertyController : MonoBehaviour
{
    public GameObject propertyPanel;
    public GameObject cameraPreview;

    public void OnClick()
    {
        propertyPanel.SetActive(true);
        cameraPreview.SetActive(false);
    }
}
