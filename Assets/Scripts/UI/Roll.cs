using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Roll : SkyArUiBase
{
    public SkyArUiBase pitch;
    public TMP_Text RollIndicator;

    private float maxAngle = 65f;

    public override void SetNewData(SkyArData data)
    {
        float angle = Mathf.Clamp(data.Roll / 10f, maxAngle * -1f, maxAngle);
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

        pitch.transform.rotation = rotation;
        transform.rotation = rotation;
        RollIndicator.text = angle.ToString("00");
    }
}
