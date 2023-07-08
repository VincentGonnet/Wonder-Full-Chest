using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public ItemType currentItem;
    public ItemType nextItem;

    private void Start()
    {
        this.currentItem = Resources.Load<ItemType>("Items/Sword");

    }
    public void SetNextItem(ItemType nextItem)
    {
        this.nextItem = nextItem;
    }
    public void UseCurrentItem(GameObject obstacleGO)
    {
        
        if (this.currentItem == null)
        {
            Debug.LogError("Couldn't get current item : not defined");
            return;
        }

        // TODO: Kill/Pass enemy
        CheckPassEnemy(obstacleGO);

        // this.currentItem = this.nextItem;
        // this.nextItem = null;
    }

    private void CheckPassEnemy(GameObject obstacleGO)
    {
        Obstacle obstacle = obstacleGO.GetComponent<Obstacle>();
        string[] resolvers = obstacle.data.resolvers;
        if (resolvers.Any(this.currentItem.itemName.Contains))
        {
            // TODO: Properly kill the enemy, unlock next craft.
            Destroy(obstacleGO);
        } else {
            // TODO: Make player take one hit. 
            GameObject.Find("GameManager").GetComponent<GameManager>().DamagePlayer();
        }
    }
}
