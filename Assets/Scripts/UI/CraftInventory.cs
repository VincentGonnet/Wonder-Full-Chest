using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Linq;
using UnityEngine.Rendering;
using UnityEngine.InputSystem;

public class CraftInventory : MonoBehaviour
{
    [SerializeField] public GameObject prefabGridCell;

    [SerializeField] public GameObject prefabGridText;

    [SerializeField] public GameObject prefabGridImage;
    [SerializeField] public InventoryCraftingTable craftingTablePreview;

    private ItemType[] objs;
    private Transform currentHightlight;
    public List<ItemType> currentRecipe = new();

    // Start is called before the first frame update
    void Start()
    {
        // Get Obj List to display
        objs = Resources.LoadAll<ItemType>("Items").OrderBy(c => c.gridPosition).ToArray();

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
                ItemType foundItem = objs.SingleOrDefault(item => item.gridPosition == i*3 + j);

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
                text.GetComponent<TextMeshProUGUI>().text = foundItem.itemName;

                GameObject image = Instantiate(prefabGridImage);
                image.name = "Image";
                image.transform.SetParent(item.transform);
                RectTransform rt4 = image.GetComponent<RectTransform>();
                rt4.anchoredPosition = new Vector2(0, 0);
                image.GetComponent<Image>().sprite = foundItem.texture;
            }
        }
    }

    // Select Grid Cell (gridRow & gridColumn start at 0)
    void SelectGridCell(int gridRow, int gridColumn)
    {
        if (currentHightlight)
        {
            currentHightlight.GetComponent<Image>().color = Color.red;
        }

        Transform row = this.transform.GetChild(gridRow);
        Transform column = row.GetChild(gridColumn);

        column.GetComponent<Image>().color = Color.blue;
        currentHightlight = column;

        // Add To Current Recipe
        currentRecipe.Add(objs[gridRow*3 + gridColumn]);

        // Set recipe on screen
        Debug.Log("on vient d'ajouter un élément");
        Debug.Log(currentRecipe.Count);
        craftingTablePreview.DrawRecipe(this.currentRecipe.ToArray());

        // Set last time action
        StopAllCoroutines();
        StartCoroutine(DismissRecipe());

        // Update recipes
        GameObject.Find("CraftAvailableRecipes").GetComponent<CraftRecipes>().SwitchPage(0);

       if(GameObject.Find("CraftAvailableRecipes").GetComponent<CraftRecipes>().filterItems(currentRecipe.ToArray()).Length == 0){
            currentRecipe.Clear();

            // Set recipe on screen
            craftingTablePreview.DrawRecipe(this.currentRecipe.ToArray());

            // Update recipes
            GameObject.Find("CraftAvailableRecipes").GetComponent<CraftRecipes>().SwitchPage(0);
       }
    }

    IEnumerator DismissRecipe() {
        yield return new WaitForSeconds(3f);

        if(currentRecipe != null && currentRecipe.Count > 0) {
            currentRecipe.Clear();

            // Set recipe on screen
            craftingTablePreview.DrawRecipe(this.currentRecipe.ToArray());

            // Update recipes
            GameObject.Find("CraftAvailableRecipes").GetComponent<CraftRecipes>().SwitchPage(0);
        }
    }

    public void ValidateRecipe(InputAction.CallbackContext context) {
        if(context.performed) {
            ItemCraft itemCraft = GameObject.Find("CraftAvailableRecipes").GetComponent<CraftRecipes>().validateItems(currentRecipe.ToArray());
            if(itemCraft != null) {
                //TODO: send item to hand
            }else{
                //TODO: Indicate incorrect
            }

            currentRecipe.Clear();

            // Set recipe on screen
            craftingTablePreview.DrawRecipe(this.currentRecipe.ToArray());

            // Update recipes
            GameObject.Find("CraftAvailableRecipes").GetComponent<CraftRecipes>().SwitchPage(0);
        }
    }

    public void OnCase1() {
        SelectGridCell(0, 0);
    }

    public void OnCase2() {
        SelectGridCell(0, 1);
    }

    public void OnCase3() {
        SelectGridCell(0, 2);
    }

    public void OnCase4()
    {
        SelectGridCell(1, 0);
    }

    public void OnCase5()
    {
        SelectGridCell(1, 1);
    }

    public void OnCase6()
    {
        SelectGridCell(1, 2);
    }

    public void OnCase7()
    {
        SelectGridCell(2, 0);
    }

    public void OnCase8()
    {
        SelectGridCell(2, 1);
    }

    public void OnCase9()
    {
        SelectGridCell(2, 2);
    }

    public void OnCase10()
    {
        SelectGridCell(3, 0);
    }

    public void OnCase11()
    {
        SelectGridCell(3, 1);
    }

    public void OnCase12()
    {
        SelectGridCell(3, 2);
    }
}