using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] public int index = 0;
    private GameObject topCircleTargetOSU;
    private GameObject bottomCircleTargetOSU;

    void Awake()
    {
        // Cannot use GameObject serialize field bc of type mismatch.
        topCircleTargetOSU = GameObject.Find("CircleTargetTop");
        bottomCircleTargetOSU = GameObject.Find("CircleTargetBottom");
    }
    
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

    public void OSUButton2(CallbackContext context)
    {
        if (index == 0)
        {
            topCircleTargetOSU.GetComponent<CircleTarget>().OnButtonPressed(context);
            // Debug.Log("OSU TOP");
        }
    }

    public void OSUButton1(CallbackContext context)
    {
        if (index == 0)
        {
            bottomCircleTargetOSU.GetComponent<CircleTarget>().OnButtonPressed(context);
            // Debug.Log("OSU BOT");
        }
    }

}
