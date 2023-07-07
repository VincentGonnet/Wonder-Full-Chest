using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle
{
    private ObstacleScriptable data;
    private GameObject gameObject;
    private string name;
    private Vector2 position;
    public Obstacle(string obstacleName, Vector2 worldPos)
    {
        this.data = Resources.Load<ObstacleScriptable>("ScriptableObjects/"+obstacleName);
        this.name = obstacleName;
        this.position = worldPos;
        Init();
    }

    public void Init()
    {
        if (this.data == null) {
            Debug.LogError("Couldn't load obstacle : ScriptableObject not found");
            return;
        }

        GameObject go = new GameObject(this.name);
        go.AddComponent<SpriteRenderer>().sprite = this.data.sprite;
        go.transform.localScale = this.data.spriteSize;
        go.AddComponent<BoxCollider2D>();
        go.transform.position = new Vector3(this.position.x, this.position.y, 0);
    }
}
