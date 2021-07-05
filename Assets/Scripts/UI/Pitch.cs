using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Pitch : SkyArUiBase
{
    private List<Transform> elements = new List<Transform>();
    private float delta = 0f;

    private float currentPitch = 0f;
    private float previousPitch = 0f;
    public int increment = 5;

    private void Awake()
    {
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            elements.Add(transform.GetChild(0).GetChild(i));
        }

        int elementsNumber = elements.Count - 1;
        float maxY = elements.Last().transform.localPosition.y;
        delta = ((maxY * 2f) / elementsNumber) / increment;
    }

    private void ScrollElements()
    {
        float amount = delta * (currentPitch - previousPitch);

        foreach (Transform item in elements)
        {
            item.localPosition = new Vector2(item.localPosition.x, item.localPosition.y - amount);
        }

        previousPitch = currentPitch;
    }

    public override void SetNewData(SkyArData data)
    {
        currentPitch = Mathf.Clamp(data.Pitch / 10f, -90f, 90f);

        ScrollElements();
    }
}
