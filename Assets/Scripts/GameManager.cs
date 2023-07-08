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

    // Update is called once per frame
    void SpawnWave()
    {
        obstacles.Clear();
        for (int i = 0; i < 5; i++) {
            obstacles.Add(Obstacle.Instanciate("Door", new Vector2(11 + i*3, 1)));
        }
    }
}
