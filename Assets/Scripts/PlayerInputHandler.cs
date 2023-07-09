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
    private string[] currentInputScheme = {"Rhythm", "Craft"};

    void Awake()
    {
        // Cannot use GameObject serialize field bc of type mismatch.
        topCircleTargetOSU = GameObject.Find("CircleTargetTop");
        bottomCircleTargetOSU = GameObject.Find("CircleTargetBottom");

        craftInventory = GameObject.Find("CraftInventory");
    }
    
    void Start()
    {
        if (PlayerInput.all.Count == 2) {
            PlayerInput.all[0].currentActionMap = this.GetComponent<PlayerInput>().actions.FindActionMap(currentInputScheme[0]);
            PlayerInput.all[1].currentActionMap = this.GetComponent<PlayerInput>().actions.FindActionMap(currentInputScheme[1]);
            PlayerInput.all[0].actions.FindActionMap(currentInputScheme[0]).Enable();
            PlayerInput.all[1].actions.FindActionMap(currentInputScheme[1]).Enable();
        }
    }

    void SwapRoles() {
        PlayerInput.all[0].currentActionMap = this.GetComponent<PlayerInput>().actions.FindActionMap(currentInputScheme[1]);
        PlayerInput.all[1].currentActionMap = this.GetComponent<PlayerInput>().actions.FindActionMap(currentInputScheme[0]);
        currentInputScheme = new string[] { currentInputScheme[1], currentInputScheme[0] };
    }

    public void OnTestJ1(CallbackContext context) {
        Debug.Log(index);
    }

    public void OSUButton2(CallbackContext context)
    {
        // topCircleTargetOSU.GetComponent<CircleTarget>().OnButtonPressed(context);
        Debug.Log("OSU TOP");
    }

    public void OSUButton1(CallbackContext context)
    {
       // bottomCircleTargetOSU.GetComponent<CircleTarget>().OnButtonPressed(context);
        Debug.Log("OSU BOT");
    }

    private void PerformAction()
    {
        CraftInventory cI = craftInventory.GetComponent<CraftInventory>();
        if (btnS && modifier1) cI.OnCase1();
        else if (btnS) cI.OnCase2();
        else if (btnS && modifier2) cI.OnCase3();
        else if (btnE && modifier1) cI.OnCase4();
        else if (btnE) cI.OnCase5();
        else if (btnE && modifier2) cI.OnCase6();
        else if (btnN && modifier1) cI.OnCase7();
        else if (btnN) cI.OnCase8();
        else if (btnN && modifier2) cI.OnCase9();
        else if (btnW && modifier1) cI.OnCase10();
        else if (btnW) cI.OnCase11();
        else if (btnW && modifier2) cI.OnCase12();
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
        if (context.canceled) {
            StartCoroutine(Unpress("btnS"));
        } else if(context.started){
            btnS = true;
            StartCoroutine(DelayPress());
        }
    }

    public void BtnE(CallbackContext context)
    {
        if (context.canceled) {
            StartCoroutine(Unpress("btnE"));
        } else if(context.started){
            btnE = true;
            StartCoroutine(DelayPress());
        }
    }

    public void BtnW(CallbackContext context)
    {
        if (context.canceled) {
            StartCoroutine(Unpress("btnW"));
        } else if(context.started){
            btnW = true;
            StartCoroutine(DelayPress());
        }
    }

    public void BtnN(CallbackContext context)
    {
        if (context.canceled) {
            StartCoroutine(Unpress("btnN"));
        } else if(context.started){
            btnN = true;
            StartCoroutine(DelayPress());
        }
    }

}