using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataSourceFactory
{
    public static IDataSource GetDataFromDebug()
    {
        return new DataSourceDebug();
    }

    public static IDataSource GetDataFromFile(string pathToFile)
    {
        return new DataSourceFile(pathToFile);
    }
}
