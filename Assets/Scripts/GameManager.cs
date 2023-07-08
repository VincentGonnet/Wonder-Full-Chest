using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public int playerHearts = 3;
    [SerializeField] public float obstacleSpeed = 2f;
    [SerializeField] public float osuSpeed = 2;
    public int wave;
    public void DamagePlayer()
    {
        this.playerHearts -= 1;
        if (this.playerHearts <= 0) {
            // TODO: Game Over
        }
    }
}
