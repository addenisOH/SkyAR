﻿using MoverioBasicFunctionUnityPlugin;
using MoverioBasicFunctionUnityPlugin.Type;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LightController : MonoBehaviour
{
    private GameObject sensorValueLayout;
    private GameObject sensorHeaderLayout;
    private Text lxValue;
    private Text accuracyValue;

    void Start()
    {
        sensorValueLayout = transform.Find("SensorValueLayout").gameObject;
        sensorHeaderLayout = transform.Find("SensorHeaderLayout").gameObject;

        lxValue = sensorValueLayout.transform.Find("SensorlxValue").gameObject.GetComponent<Text>();
        accuracyValue = sensorHeaderLayout.transform.Find("SensorAccuracy").gameObject.GetComponent<Text>();
    }

    void Update()
    {
        if (!MoverioInput.IsActive())
        {
            return;
        }

        var value = MoverioInput.GetLight();
        UpdateSensorValue(value);

        try
        {
            var accuracy = MoverioInput.GetLightAccuracy();
            UpdateSensorAccuracy(accuracy);
        }
        catch (IOException e)
        {
            Debug.LogError("Getting the accuracy of Ambient light sensor is failed. Message = " + e.Message);
        }
    }

    private void UpdateSensorValue(float value)
    {
        lxValue.text = value.ToString();
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
