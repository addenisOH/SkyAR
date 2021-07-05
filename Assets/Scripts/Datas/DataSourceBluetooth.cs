using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using UnityEngine;

public class DataSourceBluetooth : MonoBehaviour, IDataSource
{
    private bool isInitialize = true;

    public event NewDataHandler NewDataReceived;
    public event NewDebugHandler NewDebugReceived;

    private const string TRAME_DELIMITER = ">>";
    private string currentTrame = string.Empty;
    private string finalTrame = string.Empty;

    public bool IsInitialize()
    {
        return isInitialize;
    }

    public void Exec()
    {
        
    }

    public void DataSubscribe(string address, string charactUID, byte[] data)
    {
        string values = Encoding.UTF8.GetString(data);
        string[] trames;

        currentTrame += values;
        if (currentTrame.Contains(TRAME_DELIMITER))
        {
            trames = currentTrame.Split(new string[] { TRAME_DELIMITER }, System.StringSplitOptions.None);
            finalTrame = trames[0];
            currentTrame = trames[1];
            ConstructData(finalTrame);
            finalTrame = string.Empty;
        }
    }

    private void ConstructData(string message)
    {
        string trame = message.Replace("<", "");
        string[] values = trame.Split('>');
        SkyArData data = new SkyArData();

        foreach (string item in values)
        {
            string[] map = item.Split('|');
            int key;
            if (Int32.TryParse(map[0], out key))
            {
                switch ((EnumDatas)key)
                {
                    case EnumDatas.Pitch:
                        float pitch;
                        data.Pitch = float.TryParse(map[1], out pitch) ? pitch : 0f;
                        break;
                    case EnumDatas.Roll:
                        float roll;
                        data.Roll = float.TryParse(map[1], out roll) ? roll : 0f;
                        break;
                    case EnumDatas.Yaw:
                        int yaw;
                        data.Yaw = Int32.TryParse(map[1], out yaw) ? yaw : 0;
                        break;
                    case EnumDatas.Airspeed:
                        float airspeed;
                        data.AirSpeed = float.TryParse(map[1], out airspeed) ? airspeed : 0f;
                        break;
                    case EnumDatas.Altitude:
                        float altitude;
                        data.Altitude = float.TryParse(map[1], out altitude) ? altitude : 0f;
                        break;
                    case EnumDatas.TurnRate:
                        float turn;
                        data.TurnRate = float.TryParse(map[1], out turn) ? turn : 0f;
                        break;
                    case EnumDatas.VerticalSpeed:
                        float vspeed;
                        data.VerticalSpeed = float.TryParse(map[1], out vspeed) ? vspeed : 0f;
                        break;
                    case EnumDatas.LateralG:
                        float latg;
                        data.LateralG = float.TryParse(map[1], out latg) ? latg : 0f;
                        break;
                    case EnumDatas.VerticalG:
                        float verg;
                        data.VerticalG = float.TryParse(map[1], out verg) ? verg : 0f;
                        break;
                    default:
                        break;
                }
            }
        }
        NewDataReceived?.Invoke(data);
    }
    public void Stop()
    {
        isInitialize = false;
    }
}
