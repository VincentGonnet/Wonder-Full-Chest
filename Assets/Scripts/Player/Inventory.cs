using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public ItemCraft currentItem;
    public ItemCraft nextItem;

    private void Start()
    {
        this.currentItem = Resources.Load<ItemCraft>("Items/Sword");

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
        return CheckPassEnemy(obstacleGO);

        // this.currentItem = this.nextItem;
        // this.nextItem = null;
    }

    private bool CheckPassEnemy(GameObject obstacleGO)
    {
        Obstacle obstacle = obstacleGO.GetComponent<Obstacle>();
        string[] resolvers = obstacle.data.resolvers;
        if (resolvers.Any(this.currentItem.name.Contains))
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
}
