using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] public int playerHearts = 3;

    public int wave;

    public void DamagePlayer()
    {
        Debug.Log("Took damage");
        this.playerHearts -= 1;
        if (this.playerHearts <= 0) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
