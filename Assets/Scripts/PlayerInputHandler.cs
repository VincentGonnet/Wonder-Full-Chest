using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] public int index = 1;
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
        //if (GameObject.Find("PlayerInput(Clone)") != null) {
        //    if (GameObject.Find("PlayerInput(Clone)").GetComponent<PlayerInputHandler>().index == 1) {
        //        index = 0;
        //        this.GetComponent<PlayerInput>().defaultActionMap = "Rythm";
        //    }
        //}
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
        if (index == 1)
        {
            craftInventory.GetComponent<CraftInventory>().OnCase1(context);
        }
    }

    public void Case2(CallbackContext context)
    {
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

    public void Case4(CallbackContext context)
    {
        if (index == 1)
        {
            craftInventory.GetComponent<CraftInventory>().OnCase4(context);
        }
    }

    public void Case5(CallbackContext context)
    {
        if (index == 1)
        {
            craftInventory.GetComponent<CraftInventory>().OnCase5(context);
        }
    }

    public void Case6(CallbackContext context)
    {
        if (index == 1)
        {
            craftInventory.GetComponent<CraftInventory>().OnCase6(context);
        }
    }

    public void Case7(CallbackContext context)
    {
        if (index == 1)
        {
            craftInventory.GetComponent<CraftInventory>().OnCase7(context);
        }
    }

    public void Case8(CallbackContext context)
    {
        if (index == 1)
        {
            craftInventory.GetComponent<CraftInventory>().OnCase8(context);
        }
    }

    public void Case9(CallbackContext context)
    {
        if (index == 1)
        {
            craftInventory.GetComponent<CraftInventory>().OnCase9(context);
        }
    }

    public void Case10(CallbackContext context)
    {
        if (index == 1)
        {
            craftInventory.GetComponent<CraftInventory>().OnCase10(context);
        }
    }

    public void Case11(CallbackContext context)
    {
        if (index == 1)
        {
            craftInventory.GetComponent<CraftInventory>().OnCase11(context);
        }
    }

    public void Case12(CallbackContext context)
    {
        if (index == 1)
        {
            craftInventory.GetComponent<CraftInventory>().OnCase12(context);
        }
    }
}
