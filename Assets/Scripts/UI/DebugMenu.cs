using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MoverioBasicFunctionUnityPlugin;

public class DebugMenu : MonoBehaviour
{
    public Canvas uiCanvas;
    public TMP_Text distanceText;
    public TMP_Text compassText;
    public Toggle compassToggle;
    public TMP_Text debugText;

    private const int DISTANCE_DEFAULT = 36;
    private int currentDistance;
    private int minDistance;
    private int maxDistance;
    private void Start()
    {
        if (MoverioDisplay.IsActive())
        {
            MoverioDisplay.SetScreenHorizontalShiftStep(DISTANCE_DEFAULT);
            currentDistance = MoverioDisplay.GetScreenHorizontalShiftStep();
            minDistance = MoverioDisplay.GetScreenHorizontalShiftStepMin();
            maxDistance = MoverioDisplay.GetScreenHorizontalShiftStepMax();
            distanceText.text = currentDistance.ToString();

            //compassText.text = GyroManager.Instance.GetCompassRotation().ToString() + "°";
            compassToggle.isOn = GameObject.Find("DATAMANGER").GetComponent<DataManager>().useGyro;
        }
        else
        {
            Debug.LogError("MoverioDisplay not active");
            debugText.text = "MoverioDisplay not active";
        }

        UpdateCompass();
    }

    private void UpdateCompass()
    {
        if (MoverioInput.IsActive())
        {
            compassText.text = MoverioMagneticField.GetHeading().ToString();
        }
        else
        {
            Debug.LogError("MoverioInput not active");
            debugText.text = "MoverioInput not active";
        }
    }
    public void DistanceUp()
    {
        int newDistance = Mathf.Clamp(currentDistance + 1, minDistance, maxDistance);
        updateDistance(newDistance);
    }
    public void DistanceDown()
    {
        int newDistance = Mathf.Clamp(currentDistance - 1, minDistance, maxDistance);
        updateDistance(newDistance);
    }
    private void updateDistance(int newDistance)
    {
        if (newDistance != currentDistance)
        {
            currentDistance = newDistance;
            MoverioDisplay.SetScreenHorizontalShiftStep(currentDistance);
            distanceText.text = currentDistance.ToString();
        }
    }
    private void Update()
    {
        UpdateCompass();
        
    }
    public void Reconnect()
    {
        MoverioDisplay.Reconnect();
    }
}
