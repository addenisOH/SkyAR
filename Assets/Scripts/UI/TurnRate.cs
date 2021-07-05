using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnRate : SkyArUiBase
{
    public Image indicator;
    public float maxHeight = 77f;
    public float maxTurnRate = 3f;

    private float turnRate = 0f;
    private float delta = 0f;
    private float width;

    private void Start()
    {
        delta = maxHeight / maxTurnRate;
        width = indicator.rectTransform.sizeDelta.x;
    }

    public override void SetNewData(SkyArData data)
    {
        turnRate = data.TurnRate / 10f;
        turnRate = Mathf.Clamp(turnRate, maxTurnRate *-1f , maxTurnRate);

        if (turnRate < 0f)
        {
            indicator.rectTransform.pivot = new Vector2(0f, 0f);
            indicator.transform.localPosition = new Vector2(indicator.transform.localPosition.x, 0f);
        }
        else
        {
            indicator.rectTransform.pivot = new Vector2(0f, 1f);
            indicator.transform.localPosition = new Vector2(indicator.transform.localPosition.x, 0f);
        }

        indicator.rectTransform.sizeDelta = new Vector2(width, Mathf.Abs(turnRate) * delta);
    }
}
