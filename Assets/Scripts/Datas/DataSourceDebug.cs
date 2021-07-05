using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DataSourceDebug : IDataSource
{
    public event NewDataHandler NewDataReceived;
    public event NewDebugHandler NewDebugReceived;

    private bool threadRunning;

    public void Exec()
    {
        NewDebugReceived?.Invoke("Start");
        threadRunning = true;
        Thread thread = new Thread(ThreadLoop);
        thread.Start();
    }

    public void Stop()
    {
        threadRunning = false;
    }

    private void ThreadLoop()
    {
        var data = new SkyArData()
        { Pitch = -900, Yaw = 0, AirSpeed = 0f, Altitude = 0f, VerticalSpeed = 0f, Roll = -650f, LateralG = 0f, VerticalG = 0f, TurnRate = -30f };

        while (threadRunning)
        {
            data.Pitch += 10f;
            data.Yaw = (data.Yaw > 360) ? 0 : data.Yaw + 1;
            data.AirSpeed += 1/0.36f;
            data.Altitude += 10/3.28084f;
            data.VerticalSpeed += (0.17f * 10f);
            data.Roll += 10f;
            data.LateralG += 100f;
            data.VerticalG += 10f;
            data.TurnRate++;

            NewDataReceived?.Invoke(data);

            Thread.Sleep(100);
        }
    }

    public bool IsInitialize()
    {
        return true;
    }
}
