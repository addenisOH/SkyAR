using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Altitude : SkyArUiBase
{
    public ScrollingGrid scrollingRange;
    public TMP_Text AltitudeFeetText;

    private float altitude = 0f;

    public override void SetNewData(SkyArData data)
    {
        altitude = data.Altitude * 3.28084f;
        scrollingRange?.ScrollElements(altitude);
        AltitudeFeetText.text = altitude.ToString("0000");
    }
}
