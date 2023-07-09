using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class CraftRecipes : MonoBehaviour
{
    [SerializeField] public GameObject prefabGridCell;

    [SerializeField] public GameObject prefabGridText;

    [SerializeField] public GameObject prefabGridImage;

    [SerializeField] public GameObject prefabGridArrow;

    [SerializeField] public GameObject prefabEnemyType;

    [SerializeField] public Sprite spriteSelectedCell;

    [SerializeField] public int maxRow = 7;

    public int position = 0;
    private ItemCraft[] objs;
    private ItemCraft[] objsFiltered;

    void Start() {
        objs = Resources.LoadAll<ItemCraft>("Recipes");

        SwitchPage(0);
    }

    public ItemCraft validateItems(ItemType[] it){
        if(it == null || it.Length == 0) return null;
        List<ItemCraft> objectsFound = new List<ItemCraft>();
        foreach (ItemCraft obj in objs) {
            if (obj.inputs.Length != it.Length)
            continue;
            bool found = true;
            for (int i = 0; i < it.Length; i++) {
                if (it[i] != obj.inputs[i]) {
                    found = false;
                    break;
                }
            }
            if (found) {
                return obj;
            }
        }
        return null;
    }

    public ItemCraft[] filterItems(ItemType[] it){
        if(it == null || it.Length == 0) return objs;
        List<ItemCraft> objectsFound = new List<ItemCraft>();
        foreach (ItemCraft obj in objs) {
            bool found = true;
            if(obj.inputs.Length >= it.Length) {
                for (int i = 0; i < it.Length; i++) {
                    if (it[i] != obj.inputs[i]) {
                        found = false;
                    }
                }
            } else {
                found = false;
            }

            if (found) {
                objectsFound.Add(obj);
            }
        }
        return objectsFound.OrderBy(c => c.inputs.Length).ToArray();
    }

    public void Navigate(InputAction.CallbackContext value)  {
        Debug.Log(Math.Round(value.ReadValue<float>()));
        if (value.performed) SwitchPage((int) Math.Floor(value.ReadValue<float>()));
    }

    public void SwitchPage(int value)
    {
        ItemType[] filter = GameObject.Find("CraftInventory").GetComponent<CraftInventory>().currentRecipe.ToArray();
        
        objsFiltered = filterItems(filter);

        position += value;

        if (position < 0)
        {
            position += objsFiltered.Length;
        } else {
            position %= objsFiltered.Length;
        }

        if (objsFiltered.Length > 0) BuildRecipes(objsFiltered, position);
    }

    // Start is called before the first frame update
    void BuildRecipes(ItemCraft[] objs, int position)
    {
        // Clear current build (delete all children from parent)
        int x = 0;
        GameObject[] allChildren = new GameObject[this.transform.childCount];

        // Find all child obj and store to that array
        foreach (Transform child in this.transform)
        {
            allChildren[x] = child.gameObject;
            x += 1;
        }

        // Now destroy them
        foreach (GameObject child in allChildren)
        {
            DestroyImmediate(child.gameObject);
        }

        // Pass from scale to grid 1 * 7 with all children
        float scaleX = this.GetComponent<RectTransform>().sizeDelta.x;
        float scaleY = this.GetComponent<RectTransform>().sizeDelta.y / maxRow;

        // Create column items
        for (int i = 0; i < Math.Min(objs.Length, maxRow); i++)
        {
            ItemCraft foundItem = objs[(position + i) % objs.Length];

            GameObject item = Instantiate(prefabGridCell);
            item.name = "Item" + (i + 1).ToString();
            item.transform.SetParent(this.transform);
            RectTransform rt = item.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(scaleX, scaleY);
            rt.anchorMin = new Vector2(0.5f, 1);
            rt.anchorMax = new Vector2(0.5f, 1);
            rt.anchoredPosition = new Vector2(0, (-(scaleY / 2) - scaleY * i));
            if (((Math.Min(objs.Length, maxRow) - 1) / 2) == i) item.GetComponent<Image>().sprite = spriteSelectedCell;

            for (int j = 0; j < foundItem.inputs.Length; j++)
            {
                GameObject image2 = Instantiate(prefabGridImage);
                image2.name = "Image";
                image2.transform.SetParent(item.transform);
                RectTransform rt3 = image2.GetComponent<RectTransform>();
                rt3.anchorMin = new Vector2(0, 0.5f);
                rt3.anchorMax = new Vector2(0, 0.5f);
                rt3.anchoredPosition = new Vector2(20 + (44 * (foundItem.inputs.Length - j - 1)) + 10, 0);
                image2.GetComponent<Image>().sprite = foundItem.inputs[j].texture;
                image2.transform.localScale = new Vector2(0.8f, 0.8f);

                if (j != (foundItem.inputs.Length - 1))
                {
                    GameObject image3 = Instantiate(prefabGridArrow);
                    image3.name = "Arrow";
                    image3.transform.SetParent(item.transform);
                    RectTransform rt4 = image3.GetComponent<RectTransform>();
                    rt4.anchorMin = new Vector2(0, 0.5f);
                    rt4.anchorMax = new Vector2(0, 0.5f);
                    rt4.anchoredPosition = new Vector2(43 * (foundItem.inputs.Length - j - 1) + 10, 0);
                    image3.transform.localScale = new Vector2(0.8f, 0.8f);
                }
            }

            GameObject image = Instantiate(prefabGridImage);
            image.name = "Image";
            image.transform.SetParent(item.transform);
            RectTransform rt2 = image.GetComponent<RectTransform>();
            rt2.anchorMin = new Vector2(1, 0.5f);
            rt2.anchorMax = new Vector2(1, 0.5f);
            rt2.anchoredPosition = new Vector2(-35, 0);
            image.GetComponent<Image>().sprite = foundItem.output.texture;
            image.transform.localScale = new Vector2(0.8f, 0.8f);

            GameObject enemyTypeBubble = Instantiate(this.prefabEnemyType, Vector2.zero, Quaternion.identity, item.transform);
            enemyTypeBubble.transform.GetChild(0).GetComponent<Image>().sprite = foundItem.bubbleInfo;
            enemyTypeBubble.transform.GetChild(0).GetComponent<Animator>().SetFloat("offset", (float)(i * 0.4 - Math.Truncate(i * 0.4)));
            RectTransform rect = enemyTypeBubble.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(1, 0.5f);
            rect.anchorMax = new Vector2(1, 0.5f);
            rect.anchoredPosition = new Vector2(-5, 15);
            enemyTypeBubble.transform.localScale = new Vector2(0.8f, 0.8f);
        }

    }

}