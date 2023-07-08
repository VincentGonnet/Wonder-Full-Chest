using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CraftGrid : MonoBehaviour
{
    [SerializeField] public GameObject prefabGridCell;

    [SerializeField] public GameObject prefabGridText;

    [SerializeField] public GameObject prefabGridImage;

    private Transform currentHightlight;

    // Start is called before the first frame update
    void Start()
    {
        // Pass from scale to grid 3*4 with all children
        float scaleX = this.GetComponent<RectTransform>().sizeDelta.x / 3;
        float scaleY = this.GetComponent<RectTransform>().sizeDelta.y / 4;

        // Create row items
        for (int i = 0; i < 4; i++)
        {
            GameObject row = new GameObject("Row" + (i + 1).ToString());
            row.transform.SetParent(this.transform);
            RectTransform rt = row.AddComponent<RectTransform>();
            rt.sizeDelta = new Vector2(this.GetComponent<RectTransform>().sizeDelta.x, scaleY);
            rt.anchorMin = new Vector2(0.5f, 1);
            rt.anchorMax = new Vector2(0.5f, 1);
            rt.anchoredPosition = new Vector2(0, (-(scaleY/2) - scaleY * i));

            // Create column items
            for (int j = 0; j < 3; j++)
            {
                GameObject item = Instantiate(prefabGridCell);
                item.name = "Item" + (j + 1).ToString();
                item.transform.SetParent(row.transform);
                RectTransform rt2 = item.GetComponent<RectTransform>();
                rt2.sizeDelta = new Vector2(scaleX, scaleY);
                rt2.anchoredPosition = new Vector2((-(scaleX) + scaleX * j), 0);

                GameObject text = Instantiate(prefabGridText);
                text.name = "Text";
                text.transform.SetParent(item.transform);
                RectTransform rt3 = text.GetComponent<RectTransform>();
                rt3.anchoredPosition = new Vector2(0, 20);

                GameObject image = Instantiate(prefabGridImage);
                image.name = "Image";
                image.transform.SetParent(item.transform);
                RectTransform rt4 = image.GetComponent<RectTransform>();
                rt4.anchoredPosition = new Vector2(0, -26);
            }
        }

        HighlightGridCell(0, 2);

        HighlightGridCell(2, 1);
    }

    // Highlight Grid Cell (gridRow & gridColumn start at 0)
    void HighlightGridCell(int gridRow, int gridColumn)
    {
        if (currentHightlight)
        {
            currentHightlight.GetComponent<Image>().color = Color.red;
        }

        Transform row = this.transform.GetChild(gridRow);
        Transform column = row.GetChild(gridColumn);

        column.GetComponent<Image>().color = Color.blue;
        currentHightlight = column;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
