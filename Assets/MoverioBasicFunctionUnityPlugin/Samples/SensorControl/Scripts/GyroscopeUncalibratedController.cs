using MoverioBasicFunctionUnityPlugin;
using MoverioBasicFunctionUnityPlugin.Type;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GyroscopeUncalibratedController : MonoBehaviour
{
    private GameObject sensorValueLayout;
    private GameObject sensorBiasValueLayout;
    private GameObject sensorHeaderLayout;
    private Text xValue;
    private Text yValue;
    private Text zValue;
    private Text xBiasValue;
    private Text yBiasValue;
    private Text zBiasValue;
    private Text accuracyValue;

    void Start()
    {
        sensorValueLayout = transform.Find("SensorValueLayout").gameObject;
        sensorBiasValueLayout = transform.Find("SensorBiasValueLayout").gameObject;
        sensorHeaderLayout = transform.Find("SensorHeaderLayout").gameObject;

        xValue = sensorValueLayout.transform.Find("SensorXValue").gameObject.GetComponent<Text>();
        yValue = sensorValueLayout.transform.Find("SensorYValue").gameObject.GetComponent<Text>();
        zValue = sensorValueLayout.transform.Find("SensorZValue").gameObject.GetComponent<Text>();
        xBiasValue = sensorBiasValueLayout.transform.Find("SensorBiasXValue").gameObject.GetComponent<Text>();
        yBiasValue = sensorBiasValueLayout.transform.Find("SensorBiasYValue").gameObject.GetComponent<Text>();
        zBiasValue = sensorBiasValueLayout.transform.Find("SensorBiasZValue").gameObject.GetComponent<Text>();
        accuracyValue = sensorHeaderLayout.transform.Find("SensorAccuracy").gameObject.GetComponent<Text>();
    }

    void Update()
    {
        if (!MoverioInput.IsActive())
        {
            return;
        }

        var value = MoverioInput.GetGyroUncalibrated();
        UpdateSensorValue(value);

        try
        {
            var accuracy = MoverioInput.GetGyroUncalibratedAccuracy();
            UpdateSensorAccuracy(accuracy);
        }
        catch (IOException e)
        {
            Debug.LogError("Getting the accuracy of Uncalibrated Gyroscope sensor is failed. Message = " + e.Message);
        }
    }

    private void UpdateSensorValue(float[] value)
    {
        if (value.Length != 6)
        {
            return;
        }

        xValue.text = value[0].ToString("F4");
        yValue.text = value[1].ToString("F4");
        zValue.text = value[2].ToString("F4");
        xBiasValue.text = value[3].ToString("F4");
        yBiasValue.text = value[4].ToString("F4");
        zBiasValue.text = value[5].ToString("F4");
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
