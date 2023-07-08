using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public int playerHearts = 3;
    private List<Obstacle> obstacles = new();
    
    private void Start()
    {
        this.SpawnWave();
    }

    private void FixedUpdate()
    {
        // Spawn wave if the last one is finished
        if (this.obstacles.Last().transform.position.x < -11) {
            this.SpawnWave();
        }
    }

    void SpawnWave()
    {
        foreach(Obstacle obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }
        obstacles.Clear();
        for (int i = 0; i < 5; i++) {
            obstacles.Add(Obstacle.Instanciate("Rabbit", new Vector2(11 + i*3, 1)));
        }
    }

    public void DamagePlayer()
    {
        this.playerHearts -= 1;
        if (this.playerHearts <= 0) {
            // TODO: Game Over
        }
    }
}
