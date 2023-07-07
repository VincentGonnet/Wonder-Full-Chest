using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private List<Obstacle> obstacles = new();

    // Update is called once per frame
    void SpawnWave()
    {
        for (int i = 0; i < 5; i++) {
            obstacles.Add(new Obstacle());
        }
    }
}
