using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataSource
{
    void Exec();
    void Stop();
    event NewDataHandler NewDataReceived;
    event NewDebugHandler NewDebugReceived;
    bool IsInitialize();
}

public delegate void NewDataHandler(SkyArData data);
public delegate void NewDebugHandler(string text);
