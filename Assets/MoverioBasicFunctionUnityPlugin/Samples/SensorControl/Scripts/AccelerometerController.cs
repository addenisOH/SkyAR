using MoverioBasicFunctionUnityPlugin;
using MoverioBasicFunctionUnityPlugin.Type;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class AccelerometerController : MonoBehaviour
{
    private GameObject sensorValueLayout;
    private GameObject sensorHeaderLayout;
    private Text xValue;
    private Text yValue;
    private Text zValue;
    private Text accuracyValue;

    void Start()
    {
        sensorValueLayout = transform.Find("SensorValueLayout").gameObject;
        sensorHeaderLayout = transform.Find("SensorHeaderLayout").gameObject;

        xValue = sensorValueLayout.transform.Find("SensorXValue").gameObject.GetComponent<Text>();
        yValue = sensorValueLayout.transform.Find("SensorYValue").gameObject.GetComponent<Text>();
        zValue = sensorValueLayout.transform.Find("SensorZValue").gameObject.GetComponent<Text>();
        accuracyValue = sensorHeaderLayout.transform.Find("SensorAccuracy").gameObject.GetComponent<Text>();
    }

    void Update()
    {
        if (!MoverioInput.IsActive())
        {
            return;
        }

        var value = MoverioInput.GetAcc();
        UpdateSensorValue(value);

        try
        {
            var accuracy = MoverioInput.GetAccAccuracy();
            UpdateSensorAccuracy(accuracy);
        }
        catch (IOException e)
        {
            Debug.LogError("Getting the accuracy of Accelerometer sensor is failed. Message = " + e.Message);
        }
    }

    private void UpdateSensorValue(Vector3 value)
    {
        xValue.text = value.x.ToString("F4");
        yValue.text = value.y.ToString("F4");
        zValue.text = value.z.ToString("F4");
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
