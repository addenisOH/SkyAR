using MoverioBasicFunctionUnityPlugin.Type;
using UnityEngine;
using UnityEngine.UI;

public class HeadsetMotionDetectEventController : MonoBehaviour
{
    private GameObject sensorValueLayout;
    private GameObject sensorHeaderLayout;
    private Text sensorValue;
    private Text accuracyValue;

    void Start()
    {
        sensorValueLayout = transform.Find("SensorValueLayout").gameObject;
        sensorHeaderLayout = transform.Find("SensorHeaderLayout").gameObject;

        sensorValue = sensorValueLayout.transform.Find("SensorValue").gameObject.GetComponent<Text>();
        accuracyValue = sensorHeaderLayout.transform.Find("SensorAccuracy").gameObject.GetComponent<Text>();
    }

    public void OnHeadsetMotionDetect(SensorDataAccuracy accuracy)
    {
        CancelInvoke("ResetSensorText");
        sensorValue.text = "Headset Motion Detect";
        UpdateSensorAccuracy(accuracy);
        Invoke("ResetSensorText", 2f);
    }

    private void ResetSensorText()
    {
        sensorValue.text = "-";
        accuracyValue.text = "-";
    }

    private void UpdateSensorAccuracy(SensorDataAccuracy accuracy)
    {
        var inputText = "-";
        switch (accuracy)
        {
            case SensorDataAccuracy.SENSOR_DATA_ACCURACY_HIGH:
                {
                    inputText = "High";
                    break;
                }

            case SensorDataAccuracy.SENSOR_DATA_ACCURACY_MEDIUM:
                {
                    inputText = "Medium";
                    break;
                }
            case SensorDataAccuracy.SENSOR_DATA_ACCURACY_LOW:
                {
                    inputText = "Low";
                    break;
                }
            case SensorDataAccuracy.SENSOR_DATA_UNRELIABLE:
                {
                    inputText = "Unreliable";
                    break;
                }
        }
        accuracyValue.text = inputText;
    }
}
