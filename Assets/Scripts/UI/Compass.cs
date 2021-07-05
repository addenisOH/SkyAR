using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Compass : SkyArUiBase
{
    public RawImage compassImage;
    public TMP_Text compassText;

    private float angle = 0f;

    public override void SetNewData(SkyArData data)
    {
        angle = Mathf.Clamp(data.Yaw, 0, 360);
        compassImage.uvRect = new Rect(angle / 360f, 0, 1, 1);
        compassText.text = angle.ToString("000");
    }
}
