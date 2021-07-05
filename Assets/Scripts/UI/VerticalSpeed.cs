using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VerticalSpeed : SkyArUiBase
{
    public Image indicator;
    public float maxHeight = 340f;
    public float maxVerticalSpeed = 1000f;

    private float verticalSpeed = 0f;
    private float delta = 0f;
    private float width;

    private void Start()
    {
        delta = maxHeight / maxVerticalSpeed;
        width = indicator.rectTransform.sizeDelta.x;
    }

    public override void SetNewData(SkyArData data)
    {
        verticalSpeed = data.VerticalSpeed * 6;
        verticalSpeed = Mathf.Clamp(verticalSpeed, maxVerticalSpeed *-1f , maxVerticalSpeed);

        if (verticalSpeed < 0f)
        {
            indicator.rectTransform.pivot = new Vector2(0f, 1f);
            indicator.transform.localPosition = new Vector2(indicator.transform.localPosition.x, 0f);
        }
        else
        {
            indicator.rectTransform.pivot = new Vector2(0f, 0f);
            indicator.transform.localPosition = new Vector2(indicator.transform.localPosition.x, 0f);
        }

        indicator.rectTransform.sizeDelta = new Vector2(width, Mathf.Abs(verticalSpeed) * delta);
    }
}
