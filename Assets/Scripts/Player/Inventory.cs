using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] SpriteRenderer itemRenderer;
    
    public ItemCraft currentItem;
    public ItemCraft nextItem;

    private void Start()
    {
        this.currentItem = Resources.Load<ItemCraft>("Recipes/Sword");
        this.RenderItem();
    }
    public void SetNextItem(ItemCraft nextItem)
    {
        this.nextItem = nextItem;
    }
    public bool UseCurrentItem(GameObject obstacleGO)
    {
        bool passed = CheckPassEnemy(obstacleGO);

        this.currentItem = this.nextItem;
        this.RenderItem();
        this.nextItem = null;
        return passed;
    }

    private bool CheckPassEnemy(GameObject obstacleGO)
    {
        if (this.currentItem is null) {
            return false;
        }
        Obstacle obstacle = obstacleGO.GetComponent<Obstacle>();
        ItemBase[] resolvers = obstacle.data.resolvers;
        if (resolvers.Any((item) => item.itemName == this.currentItem.output.itemName))
        {
            return true;
        } else {
            return false;
        }
    }

    private void RenderItem()
    {
        if (currentItem is null) {
            this.itemRenderer.sprite = null;
            return;
        }
        this.itemRenderer.sprite = currentItem.output.texture;
        this.itemRenderer.transform.localPosition = currentItem.output.positionInHand;
        this.itemRenderer.transform.localRotation = Quaternion.Euler(0, 0, currentItem.output.orientationInHand);
    }
}
