using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Obstacle obstacle = Obstacle.Instanciate("Slime", new Vector2(3, 3));
    }

}
