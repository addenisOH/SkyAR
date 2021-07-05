using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ScrollingGrid : MonoBehaviour
{
    public GameObject panelPrefab;
    public int minValue = 0;
    public int maxValue = 300;
    public int panelValue = 10;
    public int textDivision = 0;
    public ColorRange[] colors;

    private float previousValue = 0f;
    private float cellSize = 0f;

    private void Awake()
    {
        if (!panelPrefab)
            return;

        for (int i = maxValue; i > minValue - panelValue; i -= panelValue)
        {
            var panel = Instantiate(panelPrefab, transform);
            var panelText = panel.GetComponentInChildren<TMP_Text>();

            if (colors.Length > 0)
            {
                foreach (ColorRange colorRange in colors)
                {
                    if (i >= colorRange.MinValue)
                        panel.GetComponent<Image>().color = colorRange.color;
                }
            }

            string textValue = (textDivision == 0) ? i.ToString("000") : (i / textDivision).ToString();

            panelText.text = textValue;
        }

        var gridLayout = GetComponent<GridLayoutGroup>();
        cellSize = gridLayout.cellSize.y;
    }

    public void ScrollElements(float value)
    {
        float amount = (cellSize / panelValue) * (value - previousValue);

        var newPos = new Vector2(transform.localPosition.x, transform.localPosition.y - amount);
        transform.localPosition = newPos;

        previousValue = value;
    }
}

[Serializable]
public class ColorRange
{
    public int MinValue = 0;
    public Color color;
}
