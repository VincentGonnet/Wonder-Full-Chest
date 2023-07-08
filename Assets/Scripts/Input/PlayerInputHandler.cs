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
    private bool modifier1;
    private bool modifier2;
    private bool btnS;
    private bool btnE;
    private bool btnN;
    private bool btnW;

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
           if (GameObject.Find("PlayerInput(Clone)").GetComponent<PlayerInputHandler>().index == 1) {
               index = 0;
               this.GetComponent<PlayerInput>().defaultActionMap = "Rythm";
           }
        }
    }

    // Use index to differenciate players.

    public void SwapRoles()
    {
        if (index == 1) index = 0;
        else index = 1;
    }

    public void TestIndex(CallbackContext context) {
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

    private void PerformAction()
    {
        if (btnS && modifier1) craftInventory.GetComponent<CraftInventory>().OnCase1();
        else if (btnS && modifier2) craftInventory.GetComponent<CraftInventory>().OnCase3();
        else if (btnS) craftInventory.GetComponent<CraftInventory>().OnCase2();
        else if (btnE && modifier1) craftInventory.GetComponent<CraftInventory>().OnCase4();
        else if (btnE && modifier2) craftInventory.GetComponent<CraftInventory>().OnCase6();
        else if (btnE) craftInventory.GetComponent<CraftInventory>().OnCase5();
        else if (btnW && modifier1) craftInventory.GetComponent<CraftInventory>().OnCase10();
        else if (btnW && modifier2) craftInventory.GetComponent<CraftInventory>().OnCase12();
        else if (btnW) craftInventory.GetComponent<CraftInventory>().OnCase11();
        else if (btnN && modifier1) craftInventory.GetComponent<CraftInventory>().OnCase7();
        else if (btnN && modifier2) craftInventory.GetComponent<CraftInventory>().OnCase9();
        else if (btnN) craftInventory.GetComponent<CraftInventory>().OnCase8();
    }
    
    IEnumerator Unpress(string key)
    {
        yield return new WaitForSeconds(0.1f);
        switch (key)
        {
            case "md1":
                modifier1 = false;
                break;
            case "md2":
                modifier2 = false;
                break;
            case "btnS":
                btnS = false;
                break;
            case "btnE":
                btnE = false;
                break;
            case "btnW":
                btnW = false;
                break;
            case "btnN":
                btnN = false;
                break;
            default:
                break;
        }
    }

    IEnumerator DelayPress()
    {
        yield return new WaitForSeconds(0.1f);
        Debug.Log("pressed delayed");
        PerformAction();
    }

    public void Modifier1Input(CallbackContext context)
    {
        if (index != 1) return;

        if (context.canceled) {
            StartCoroutine(Unpress("md1"));
        }
        if (context.started) {
            modifier1 = true;
            PerformAction();
        }
    }
    public void Modifier2Input(CallbackContext context)
    {
        if (index != 1) return;

        if (context.canceled) {
            StartCoroutine(Unpress("md2"));
        }
        if (context.started) {
            modifier2 = true;
            PerformAction();
        }
    }

    public void BtnS(CallbackContext context)
    {
        if (index != 1) return;
        if (context.canceled) {
            StartCoroutine(Unpress("btnS"));
        } else if(context.started){
            btnS = true;
            StartCoroutine(DelayPress());
        }
    }

    public void BtnE(CallbackContext context)
    {
        if (index != 1) return;
        if (context.canceled) {
            StartCoroutine(Unpress("btnE"));
        } else if(context.started){
            btnE = true;
            StartCoroutine(DelayPress());
        }
    }

    public void BtnW(CallbackContext context)
    {
        if (index != 1) return;
        if (context.canceled) {
            StartCoroutine(Unpress("btnW"));
        } else if(context.started){
            btnW = true;
            StartCoroutine(DelayPress());
        }
    }

    public void BtnN(CallbackContext context)
    {
        if (index != 1) return;
        if (context.canceled) {
            StartCoroutine(Unpress("btnN"));
        } else if(context.started){
            btnN = true;
            StartCoroutine(DelayPress());
        }
    }

}