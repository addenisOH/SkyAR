using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading;
using System.ComponentModel;
using System;

public class DataSourceFile : IDataSource
{
    public event NewDataHandler NewDataReceived;
    public event NewDebugHandler NewDebugReceived;

    private string path;
    private bool readingFile = false;
    private bool isInitialize = false;
    public DataSourceFile(string pathToFile)
    {
        path = pathToFile;

        if (File.Exists(path))
        {
            isInitialize = true;
        }
    }

    public void Exec()
    {
        if (!File.Exists(path))
        {
            Debug.LogError("File doesn't exists !");
            return;
        }
        readingFile = true;
        Thread thread = new Thread(ThreadLoop);
        thread.Start();
    }

    private void ThreadLoop()
    {
        using (StreamReader file = new StreamReader(path))
        {
            TimeSpan lineTimeSpan;
            TimeSpan nextTimeSpan;
            TimeSpan sleepTimeSpan;

            //lire le premier timestamp
            lineTimeSpan = ReadTimeSpan(file.ReadLine());

            while (readingFile || file.EndOfStream)
            {
                //lire les datas
                SkyArData data = new SkyArData();
                for (int i = 0; i < 11; i++)
                {
                    int value = GetValueFromDataLine(file.ReadLine());
                    switch (i)
                    {
                        case 0: data.Pitch = value;
                            break;
                        case 1: data.Roll = value;
                            break;
                        case 2: data.Yaw = value;
                            break;
                        case 3: data.AirSpeed = value;
                            break;
                        case 4: data.Altitude = value;
                            break;
                        case 6: data.TurnRate = value;
                            break;
                        case 7:data.VerticalSpeed = value;
                            break;
                        case 8: data.LateralG = value;
                            break;
                        case 9: data.VerticalG = value;
                            break;
                        default:
                            break;
                    }
                }
                //appliquer les datas
                NewDataReceived?.Invoke(data);
                //lire ligne blanche
                file.ReadLine();

                //lire le timestamp suivant et sleep pendant la diff. Si negatif attendre 20ms
                nextTimeSpan = ReadTimeSpan(file.ReadLine());

                sleepTimeSpan = nextTimeSpan.Subtract(lineTimeSpan);
                lineTimeSpan = nextTimeSpan;
                int timeToSleep = (sleepTimeSpan.Milliseconds > 0) ? sleepTimeSpan.Milliseconds : 20;
                Thread.Sleep(timeToSleep);
            }
        }
    }

    private TimeSpan ReadTimeSpan(string line)
    {
        string[] lineTick = line.Split(new char[] { ':' });
        //convert 1/64sec to millisec
        float millisec = Int32.Parse(lineTick[3]) * 15.625f;
        return new TimeSpan(0, Int32.Parse(lineTick[0]), Int32.Parse(lineTick[1]), Int32.Parse(lineTick[2]), Mathf.RoundToInt(millisec));
    }

    private int GetValueFromDataLine(string line)
    {
        string[] splittedLine = line.Split(new char[] { ':' });
        return Int32.Parse(splittedLine[1]);
    }

    public void Stop()
    {
        readingFile = false;
    }

    public bool IsInitialize()
    {
        return isInitialize;
    }
}
