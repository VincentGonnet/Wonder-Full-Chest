using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ItemBase", order = 0)]
public class ItemBase : ScriptableObject {
    public string itemName;
    public Sprite texture;
    public Vector2 positionInHand;
    public float orientationInHand;
}