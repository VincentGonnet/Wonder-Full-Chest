using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
}
