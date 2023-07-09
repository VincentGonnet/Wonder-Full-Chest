using UnityEngine;

public class Obstacle : MovingObject
{
    public ObstacleScriptable data;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = data.sprite;
        gameObject.transform.localScale = data.spriteSize;
        GetComponent<Animator>()?.SetInteger("EnemyId", data.enemyId);
        GetComponent<Rigidbody2D>().gravityScale = 5;
        gameObject.name = data.name;
    }
}
