using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class FlightSymmetry : SkyArUiBase
{
    public TMP_Text leftValue;
    public TMP_Text rightValue;
    public override void SetNewData(SkyArData data)
    {
        float roll = data.Roll / 10f;
        float vg = data.VerticalG;/// 10f;
        float lg = data.LateralG;/// 100f;

        float a1 = CalculateA1(roll, vg);
        a1 = (float)Math.Round(a1, 2);
        if (float.IsNaN(a1))
            a1 = 0;
        float a2 = CalculateA2(roll, lg);
        a2 = (float)Math.Round(a2, 2);
        if (float.IsNaN(a2))
            a2 = 0;

        leftValue.text = a1.ToString();
        rightValue.text = a2.ToString();
    }

    private float CalculateA1(float roll, float verticalG)
    {
        return roll - (Mathf.Atan(Mathf.Sqrt((verticalG / 9.81f) - 1f) * Mathf.Rad2Deg));
    }

    private float CalculateA2(float roll, float lateralG)
    {
        return roll - (Mathf.Atan(lateralG / (9.81f * Mathf.Cos(roll * Mathf.Deg2Rad)) * Mathf.Rad2Deg));
    }
}
