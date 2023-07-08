using UnityEngine;

public class Obstacle : MovingObject
{
    public ObstacleScriptable data;
    public static Obstacle Instanciate(string name, Vector2 worldPos)
    {
        GameObject gameObject = Object.Instantiate(Resources.Load("Prefabs/Obstacle") as GameObject, worldPos, Quaternion.identity);
        Obstacle obstacle = gameObject.GetComponent<Obstacle>();
        obstacle.data = Resources.Load<ObstacleScriptable>("ScriptableObjects/"+name);
        return obstacle;
    }

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = data.sprite;
        gameObject.transform.localScale = data.spriteSize;
        GetComponent<Animator>().SetInteger("EnemyId", data.enemyId);
        GetComponent<Rigidbody2D>().gravityScale = 5;
        gameObject.name = data.name;
    }
}
