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
    private GameObject craftInventory;

    void Awake()
    {
        // Cannot use GameObject serialize field bc of type mismatch.
        topCircleTargetOSU = GameObject.Find("CircleTargetTop");
        bottomCircleTargetOSU = GameObject.Find("CircleTargetBottom");

        craftInventory = GameObject.Find("CraftInventory");
    }
    
    void Start()
    {
        if (GameObject.Find("PlayerInput(Clone)") != null) {
            if (GameObject.Find("PlayerInput(Clone)").GetComponent<PlayerInputHandler>().index == 0) {
                index = 1;
                this.GetComponent<PlayerInput>().defaultActionMap = "Craft";
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


    public void Case1(CallbackContext context)
    {
        Debug.Log(index);
        if (index == 1)
        {
            craftInventory.GetComponent<CraftInventory>().OnCase1(context);
        }
    }

    public void Case2(CallbackContext context)
    {
        Debug.Log(index);
        if (index == 1)
        {
            craftInventory.GetComponent<CraftInventory>().OnCase2(context);
        }
    }

    public void Case3(CallbackContext context)
    {
        if (index == 1)
        {
            craftInventory.GetComponent<CraftInventory>().OnCase3(context);
        }
    }


}
