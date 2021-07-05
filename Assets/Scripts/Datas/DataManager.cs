using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [Header("Data source")]
    public DataSourceTypes source;
    [Header("Data source - File")]
    public string pathToFile;
    [Header("Data source - Bluetooth")]
    public string deviceName = "ESP32";
    public string deviceAddress = "A4:CF:12:9A:C8:5A";
    public string serviceUID = "6E400001-B5A3-F393-E0A9-E50E24DCCA9E";
    public string characteristicUID = "6E400003-B5A3-F393-E0A9-E50E24DCCA9E";

    [Header("Gyro")]
    public bool useGyro = true;

    [Header("UI objects")]
    public SkyArUiBase roll;
    public SkyArUiBase pitch;
    public SkyArUiBase yawCompass;
    public SkyArUiBase airSpeed;
    public SkyArUiBase altitude;
    public SkyArUiBase verticalSpeed;
    public SkyArUiBase flightSymmetry;
    public SkyArUiBase turnRate;

    private IDataSource dataSource;
    private float lastYaw = 0f;
    private const int BUFFERSIZE = 30;
    private RingBuffer<float> angleDiffBuffer = new RingBuffer<float>(BUFFERSIZE);

    private void Awake()
    {
        switch (source)
        {
            case DataSourceTypes.Debug:
                dataSource = DataSourceFactory.GetDataFromDebug();
                break;
            case DataSourceTypes.File:
                dataSource = DataSourceFactory.GetDataFromFile(pathToFile);
                break;
            case DataSourceTypes.Bluetooth:
                dataSource = gameObject.AddComponent<DataSourceBluetooth>();
                break;
            default:
                dataSource = DataSourceFactory.GetDataFromDebug();
                break;
        }
    }

    private void Start()
    {
        if (dataSource != null)
        {
            if (dataSource.IsInitialize())
            {
                dataSource.NewDataReceived += DataSource_NewDataReceived;
                dataSource.Exec();
            }
        }
    }

    private void Update()
    {
        ProcessGyro();
    }

    private void ProcessGyro()
    {
        if (!useGyro)
            return;

        float newYaw = lastYaw;
        float newComp = MoverioMagneticField.GetHeading();

        var quadYaw = GetAngleQuadrant(lastYaw);
        var quadComp = GetAngleQuadrant(newComp);



        if (quadComp == CompassQuadrant.I && quadYaw == CompassQuadrant.IV)
        {
            newYaw = lastYaw - 360f;
        }
        else if (quadComp == CompassQuadrant.IV && quadYaw == CompassQuadrant.I)
        {
            newComp -= 360f;
        }

        float angleDiff = Mathf.Abs(newYaw - newComp);

        angleDiffBuffer.Add(angleDiff);

        if (angleDiffBuffer.Size() >= BUFFERSIZE)
        {
            if (angleDiffBuffer.FloatMean() > 45f)
            {
                ToggleSomeUI(false);
            }
            else
            {
                ToggleSomeUI(true);
            }
            angleDiffBuffer.Clear();
        }
    }
    private void ToggleSomeUI(bool status)
    {
        pitch.gameObject.SetActive(status);
        roll.gameObject.SetActive(status);
        yawCompass.gameObject.SetActive(status);
        turnRate.gameObject.SetActive(status);
        flightSymmetry.gameObject.SetActive(status);
    }

    private CompassQuadrant GetAngleQuadrant(float angle)
    {
        if (angle > 270 && angle <= 360)
            return CompassQuadrant.IV;
        else if (angle <= 270 && angle > 180)
            return CompassQuadrant.III;
        else if (angle <= 180 && angle > 90)
            return CompassQuadrant.II;
        else if (angle <= 90 && angle > 0)
            return CompassQuadrant.I;
        else
            return CompassQuadrant.I;
    }

    private void DataSource_NewDataReceived(SkyArData data)
    {
        MainThreadDispatcher.RunOnMainThread(() => SendDataToUI(data));
    }

    private void SendDataToUI(SkyArData data)
    {
        pitch?.SetNewData(data);
        yawCompass?.SetNewData(data);
        airSpeed?.SetNewData(data);
        altitude?.SetNewData(data);
        verticalSpeed?.SetNewData(data);
        flightSymmetry?.SetNewData(data);
        turnRate?.SetNewData(data);
        roll?.SetNewData(data);

        lastYaw = data.Yaw;
    }

    private void OnApplicationQuit()
    {
        if (dataSource != null)
        {
            dataSource.NewDataReceived -= DataSource_NewDataReceived;
            dataSource.Stop();
        }
    }

    public void SetUseGyro(bool status)
    {
        useGyro = status;
        if (!status)
            ToggleSomeUI(true);
    }
}

public enum DataSourceTypes
{
    Debug,
    File,
    Bluetooth
}

public enum CompassQuadrant
{
    I,
    II,
    III,
    IV
}
