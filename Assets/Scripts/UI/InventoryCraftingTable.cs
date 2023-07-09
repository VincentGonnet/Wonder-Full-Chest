using UnityEngine;
using UnityEngine.UI;

public class InventoryCraftingTable : MonoBehaviour
{
    private Vector2 nextSize = Vector2.zero;
    private float errorMaskAlpha;
    private float errorMaskAlphaSpeed;

    private void Update()
    {
        if (nextSize == Vector2.zero) {
            return;
        }

        errorMaskAlpha = Mathf.SmoothDamp(errorMaskAlpha, 0, ref errorMaskAlphaSpeed, 1);
        if (errorMaskAlpha < 1) {
            errorMaskAlpha = 0;
            RectTransform rectTransform = this.GetComponent<RectTransform>();
            rectTransform.sizeDelta = this.nextSize;
            return;
        }

        Image image = this.GetComponent<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, errorMaskAlpha);
    }

    public void DrawRecipe(ItemType[] currentRecipe)
    {
        foreach(Transform child in this.transform) {
            Destroy(child.gameObject);
        }
        float displayWidth = currentRecipe.Length * 32f + (currentRecipe.Length - 1) * 16f;
        if (this.nextSize != Vector2.zero) {
            this.nextSize = new Vector2(displayWidth, 40);
        }
        else {
            RectTransform rectTransform = this.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(displayWidth, 40);
        }

        float startDisplayAt = -displayWidth / 2;
        for (int i = 0; i < currentRecipe.Length; i++) {
            GameObject gameObject = Instantiate(
                GameResources.PREFAB_CRAFT_INVENTORY_PREVIEW,
                new Vector3(startDisplayAt + i * (32 + 16) + 32f/2, 0) + this.transform.position,
                Quaternion.identity,
                this.transform);
            gameObject.GetComponent<Image>().sprite = currentRecipe[i].texture;
        }
    }

    public void ShowError()
    {
        //this.nextSize = this.GetComponent<RectTransform>().sizeDelta;
        //this.errorMaskAlpha = 255;
    }
}
