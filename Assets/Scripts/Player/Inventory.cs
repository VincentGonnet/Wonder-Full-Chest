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
        
        if (this.currentItem == null)
        {
            Debug.LogError("Couldn't get current item : not defined");
            return false;
        }

        // TODO: Kill/Pass enemy
        bool passed = CheckPassEnemy(obstacleGO);

        this.currentItem = this.nextItem;
        this.RenderItem();
        this.nextItem = null;
        return passed;
    }

    private bool CheckPassEnemy(GameObject obstacleGO)
    {
        Obstacle obstacle = obstacleGO.GetComponent<Obstacle>();
        ItemBase[] resolvers = obstacle.data.resolvers;
        if (resolvers.Any(this.currentItem.name.Equals))
        {
            // TODO: Properly kill the enemy, unlock next craft.
            Destroy(obstacleGO);
            return true;
        } else {
            // TODO: Make player take one hit. 
            GameObject.Find("GameManager").GetComponent<GameManager>().DamagePlayer();
            return false;
        }
    }

    private void RenderItem()
    {
        this.itemRenderer.sprite = currentItem.output.texture;
        this.itemRenderer.transform.localPosition = currentItem.output.positionInHand;
        this.itemRenderer.transform.localRotation = Quaternion.Euler(0, 0, currentItem.output.orientationInHand);
    }
}
