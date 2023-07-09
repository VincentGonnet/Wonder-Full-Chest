using UnityEngine;
using UnityEngine.UI;

public class InventoryCraftingTable : MonoBehaviour
{
    public void DrawRecipe(ItemType[] currentRecipe)
    {
        Debug.Log("on refresh");
        Debug.Log(currentRecipe.Length);
        foreach(Transform child in this.transform) {
            Destroy(child.gameObject);
        }
        float displayWidth = currentRecipe.Length * 32f + (currentRecipe.Length - 1) * 16f;
        float startDisplayAt = -displayWidth / 2;
        for (int i = 0; i < currentRecipe.Length; i++) {
            Debug.Log("ici !");
            Debug.Log(startDisplayAt + i * (32 + 16) + 32f/2);
            GameObject gameObject = Instantiate(
                GameResources.PREFAB_CRAFT_INVENTORY_PREVIEW,
                new Vector3(startDisplayAt + i * (32 + 16) + 32f/2, 0) + this.transform.position,
                Quaternion.identity,
                this.transform);
            gameObject.GetComponent<Image>().sprite = currentRecipe[i].texture;
        }
    }
}
