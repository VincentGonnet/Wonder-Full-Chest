using UnityEngine;

[CreateAssetMenu(fileName = "Craft", menuName = "ItemCraft", order = 0)]
public class ItemCraft : ScriptableObject {
    public ItemType[] inputs;
    public ItemBase output;
    public Sprite bubbleInfo;
}