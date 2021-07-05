using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AirSpeed : SkyArUiBase
{
    public ScrollingGrid scrollingGrid;
    public TMP_Text speedText;

    private float speed = 0f;

    public override void SetNewData(SkyArData data)
    {
        speed = data.AirSpeed * 0.36f;
        scrollingGrid?.ScrollElements(speed);
        speedText.text = speed.ToString("000");
    }


}
