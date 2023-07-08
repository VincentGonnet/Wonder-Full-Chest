using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public int playerHearts = 3;

    public void DamagePlayer()
    {
        this.playerHearts -= 1;
        if (this.playerHearts <= 0) {
            // TODO: Game Over
        }
    }
}
