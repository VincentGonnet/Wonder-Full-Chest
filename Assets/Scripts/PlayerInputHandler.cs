using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] public int index = 0;
    
    void Start()
    {
        if (GameObject.Find("PlayerInput(Clone)") != null) {
            if (GameObject.Find("PlayerInput(Clone)").GetComponent<PlayerInputHandler>().index == 0) {
                index = 1;
            }
        }
    }

    // Use index to differenciate players.

    public void OnTestJ1(CallbackContext context) {
        Debug.Log(index);
    }
}
