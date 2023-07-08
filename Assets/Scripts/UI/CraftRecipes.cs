using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftRecipes : MonoBehaviour
{
    [SerializeField] public GameObject prefabGridCell;

    [SerializeField] public GameObject prefabGridText;

    [SerializeField] public GameObject prefabGridImage;

    void Start() {
        BuildRecipes(Resources.LoadAll<ItemCraft>("Recipes"));
    }

    // Start is called before the first frame update
    void BuildRecipes(ItemCraft[] objs)
    {
        // Pass from scale to grid 3*4 with all children
        float scaleX = this.GetComponent<RectTransform>().sizeDelta.x;
        float scaleY = this.GetComponent<RectTransform>().sizeDelta.y / 8;

        // Create column items
        for (int i = 0; i < objs.Length; i++)
        {
            ItemCraft foundItem = objs[i];

            GameObject item = Instantiate(prefabGridCell);
            item.name = "Item" + (i + 1).ToString();
            item.transform.SetParent(this.transform);
            RectTransform rt = item.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(scaleX, scaleY);
            rt.anchorMin = new Vector2(0.5f, 1);
            rt.anchorMax = new Vector2(0.5f, 1);
            rt.anchoredPosition = new Vector2(0, (-(scaleY / 2) - scaleY * i));

            GameObject image = Instantiate(prefabGridImage);
            image.name = "Image";
            image.transform.SetParent(item.transform);
            RectTransform rt2 = image.GetComponent<RectTransform>();
            rt2.anchorMin = new Vector2(0, 0.5f);
            rt2.anchorMax = new Vector2(0, 0.5f);
            rt2.anchoredPosition = new Vector2(20, 0);
            image.GetComponent<Image>().sprite = foundItem.output.texture;

            int maxI = 3;
            for (int j = 0; j < foundItem.inputs.Length; j++)
            {
                GameObject image2 = Instantiate(prefabGridImage);
                image2.name = "Image";
                image2.transform.SetParent(item.transform);
                RectTransform rt3 = image2.GetComponent<RectTransform>();
                rt3.anchorMin = new Vector2(1, 0.5f);
                rt3.anchorMax = new Vector2(1, 0.5f);
                rt3.anchoredPosition = new Vector2(-20 - (44 * (foundItem.inputs.Length - j - 1)), 0);
                image2.GetComponent<Image>().sprite = foundItem.inputs[j].texture;

                if (j != (maxI - 1)) { 
                    GameObject text = Instantiate(prefabGridText);
                    text.name = "Text";
                    text.transform.SetParent(item.transform);
                    RectTransform rt4 = text.GetComponent<RectTransform>();
                    rt4.anchorMin = new Vector2(1, 0.5f);
                    rt4.anchorMax = new Vector2(1, 0.5f);
                    rt4.anchoredPosition = new Vector2(-43 * (foundItem.inputs.Length - j - 1), 0);
                    text.GetComponent<TextMeshProUGUI>().text = "+";
                    text.GetComponent<TextMeshProUGUI>().fontSize = 10;
                }
            }
        }

    }

}
