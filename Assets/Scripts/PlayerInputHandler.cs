using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] public int index = 1;
    private GameObject targetOSU1;
    private GameObject targetOSU2;
    private GameObject targetOSU3;
    private GameObject targetOSU4;
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
        targetOSU1 = GameObject.Find("CircleTarget1");
        targetOSU2 = GameObject.Find("CircleTarget2");
        targetOSU3 = GameObject.Find("CircleTarget3");
        targetOSU4 = GameObject.Find("CircleTarget4");
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

    public void SwapRoles() {
        PlayerInput.all[0].currentActionMap = this.GetComponent<PlayerInput>().actions.FindActionMap(currentInputScheme[1]);
        PlayerInput.all[1].currentActionMap = this.GetComponent<PlayerInput>().actions.FindActionMap(currentInputScheme[0]);
        PlayerInput.all[0].actions.FindActionMap(currentInputScheme[1]).Enable();
        PlayerInput.all[1].actions.FindActionMap(currentInputScheme[0]).Enable();
        currentInputScheme = new string[] { currentInputScheme[1], currentInputScheme[0] };
    }

    public void OnTestJ1(CallbackContext context) {
        Debug.Log(index);
    }

    public void RhythmButton1(CallbackContext context)
    {
        targetOSU1.GetComponent<CircleTarget>().OnButtonPressed(context);
    }

    public void RhythmButton2(CallbackContext context)
    {
       targetOSU2.GetComponent<CircleTarget>().OnButtonPressed(context);
    }

    public void RhythmButton3(CallbackContext context)
    {
       targetOSU3.GetComponent<CircleTarget>().OnButtonPressed(context);
    }

    public void RhythmButton4(CallbackContext context)
    {
       targetOSU4.GetComponent<CircleTarget>().OnButtonPressed(context);
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

    public void Navigate(CallbackContext context)
    {
        GameObject.Find("CraftAvailableRecipes").GetComponent<CraftRecipes>().Navigate(context);
    }

    public void Validate(CallbackContext context)
    {
        GameObject.Find("CraftInventory").GetComponent<CraftInventory>().ValidateRecipe(context);
    }
}