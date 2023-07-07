using UnityEngine;

public class Item : MonoBehaviour {

    public static Item Instantiate(ItemType type, Vector2 position) {
        GameObject obj = Object.Instantiate(Resources.Load("Prefabs/Item") as GameObject, position, Quaternion.identity);
        Item item = obj.GetComponent<Item>();
        item.type = type;
        return item;
    }

    public ItemType type;

    private void Start() {
        GetComponent<SpriteRenderer>().sprite = type.texture;
    }
    
}