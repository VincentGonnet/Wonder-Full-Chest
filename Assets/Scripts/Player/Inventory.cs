using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Item currentItem;
    public Item nextItem;
    public void SetNextItem(Item nextItem)
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

        this.currentItem = this.nextItem;
        this.nextItem = null;
    }

    private void CheckPassEnemy(GameObject obstacleGO)
    {
        Obstacle obstacle = obstacleGO.GetComponent<Obstacle>();
        string[] resolvers = obstacle.data.resolvers;
        if (resolvers.Any(this.currentItem.type.itemName.Contains))
        {
            // TODO: Properly kill the enemy, unlock next craft.
            Destroy(obstacleGO);
        }
    }
}
