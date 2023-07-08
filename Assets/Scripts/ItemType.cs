using UnityEngine;

[CreateAssetMenu(fileName = "ItemType", menuName = "ItemType", order = 0)]
public class ItemType : ScriptableObject {
    public string itemName;
    public Sprite texture;
    public int gridPosition;
}