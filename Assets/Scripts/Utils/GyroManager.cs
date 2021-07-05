using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroManager : MonoBehaviour
{
    private static GyroManager _instance;
    public static GyroManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private bool isInitialize = false;
    public bool IsInitialize { get { return isInitialize; } }
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    private void Start()
    {
        StartCoroutine(InitGyro());
    }

    IEnumerator InitGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
            Input.compass.enabled = true;
            Input.gyro.updateInterval = 1f / 60f;

            yield return new WaitForSeconds(1f);

            isInitialize = true;
        }
    }

    public Quaternion GetGyroRotation()
    {
        if (isInitialize)
            return Input.gyro.attitude;
        else
            return Quaternion.identity;
    }

    public float GetCompassRotation()
    {
        if (isInitialize)
            return Input.compass.trueHeading;
        else
            return 0f;
    }
}
