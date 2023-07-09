using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class InputPrompt : MonoBehaviour
{
    [SerializeField] float keySize = 0.7f;
    [SerializeField] float keySize2 = 0.3f;
    [SerializeField] Sprite Q;
    [SerializeField] Sprite A;
    [SerializeField] Sprite S;
    [SerializeField] Sprite D;
    [SerializeField] Sprite W;
    [SerializeField] Sprite E;
    [SerializeField] Sprite R;
    [SerializeField] Sprite U;
    [SerializeField] Sprite I;
    [SerializeField] Sprite O;
    [SerializeField] Sprite P;
    [SerializeField] Sprite J;
    [SerializeField] Sprite K;
    [SerializeField] Sprite L;
    [SerializeField] Sprite RightCrtl;
    [SerializeField] Sprite LeftCtrl;
    [SerializeField] Sprite Space;
    [SerializeField] Sprite B;
    [SerializeField] Sprite xb_X;
    [SerializeField] Sprite xb_Y;
    [SerializeField] Sprite xb_A;
    [SerializeField] Sprite xb_B;
    [SerializeField] Sprite xb_Gachette_L;
    [SerializeField] Sprite xb_Gachette_R;
    private int countStart = 0;

    GameObject inputRow;
    GameObject inputColumn;
    GameObject CSlot1;
    GameObject CSlot2;
    GameObject CSlot3;
    GameObject CSlot4;
    GameObject CSlot5;
    GameObject CSlot6;
 
    private void Start()
    {
        CSlot1 = GameObject.Find("CSlot1");
        CSlot2 = GameObject.Find("CSlot2");
        CSlot3 = GameObject.Find("CSlot3");
        CSlot4 = GameObject.Find("CSlot4");
        CSlot5 = GameObject.Find("CSlot5");
        CSlot6 = GameObject.Find("CSlot6");
    }
    public void Generate()
    {
        PlayerInput input1 = PlayerInput.all[0];
        PlayerInput input2 = PlayerInput.all[1];

        Transform tr1 = GameObject.Find("CircleTarget0").transform.GetChild(0);
        Transform tr2 = GameObject.Find("CircleTarget1").transform.GetChild(0);
        Transform tr3 = GameObject.Find("CircleTarget2").transform.GetChild(0);
        Transform tr4 = GameObject.Find("CircleTarget3").transform.GetChild(0);

        CSlot1 = GameObject.Find("CSlot1");
        CSlot2 = GameObject.Find("CSlot2");
        CSlot3 = GameObject.Find("CSlot3");
        CSlot4 = GameObject.Find("CSlot4");
        CSlot5 = GameObject.Find("CSlot5");
        CSlot6 = GameObject.Find("CSlot6");

        inputRow = GameObject.Find("InputRow");
        inputColumn = GameObject.Find("InputColumn");

        // There is a controller, we replace keyboard2 by keyboard1 keys
        if (input1.currentControlScheme.Contains("Gamepad") || input2.currentControlScheme.Contains("Gamepad"))
        {
            U = Q;
            I = W;
            O = E;
            P = R;
            J = A;
            K = S;
            L = D;
            B = LeftCtrl;
            RightCrtl = Space;
        }


        if (input1.currentActionMap.name == input1.actions.FindActionMap("Rhythm").name && input1.currentControlScheme.Contains("Keyboard")) {
            tr1.GetComponent<SpriteRenderer>().sprite = Q;
            tr2.GetComponent<SpriteRenderer>().sprite = W;
            tr3.GetComponent<SpriteRenderer>().sprite = E;
            tr4.GetComponent<SpriteRenderer>().sprite = R;
        }

        if (input1.currentActionMap.name == input1.actions.FindActionMap("Rhythm").name && input1.currentControlScheme.Contains("Gamepad")) {
            tr1.GetComponent<SpriteRenderer>().sprite = xb_A;
            tr2.GetComponent<SpriteRenderer>().sprite = xb_B;
            tr3.GetComponent<SpriteRenderer>().sprite = xb_Y;
            tr4.GetComponent<SpriteRenderer>().sprite = xb_X;
        }
        if (input2.currentActionMap.name == input2.actions.FindActionMap("Rhythm").name && input2.currentControlScheme.Contains("Gamepad")) {
            tr1.GetComponent<SpriteRenderer>().sprite = xb_A;
            tr2.GetComponent<SpriteRenderer>().sprite = xb_B;
            tr3.GetComponent<SpriteRenderer>().sprite = xb_Y;
            tr4.GetComponent<SpriteRenderer>().sprite = xb_X;
        }

        if (input2.currentActionMap.name == input2.actions.FindActionMap("Rhythm").name && input2.currentControlScheme.Contains("Keyboard")) {
            tr1.GetComponent<SpriteRenderer>().sprite = U;
            tr2.GetComponent<SpriteRenderer>().sprite = I;
            tr3.GetComponent<SpriteRenderer>().sprite = O;
            tr4.GetComponent<SpriteRenderer>().sprite = P;
        }

        if (input1.currentActionMap.name == input1.actions.FindActionMap("Craft").name && input1.currentControlScheme.Contains("Keyboard")) {
            CSlot1.GetComponent<SpriteRenderer>().sprite = W;
            CSlot2.GetComponent<SpriteRenderer>().sprite = A;
            CSlot3.GetComponent<SpriteRenderer>().sprite = S;
            CSlot4.GetComponent<SpriteRenderer>().sprite = D;
            CSlot5.GetComponent<SpriteRenderer>().sprite = LeftCtrl;
            CSlot6.GetComponent<SpriteRenderer>().sprite = Space;
        }
        if (input2.currentActionMap.name == input2.actions.FindActionMap("Craft").name && input2.currentControlScheme.Contains("Keyboard")) {
            CSlot1.GetComponent<SpriteRenderer>().sprite = I;
            CSlot2.GetComponent<SpriteRenderer>().sprite = J;
            CSlot3.GetComponent<SpriteRenderer>().sprite = K;
            CSlot4.GetComponent<SpriteRenderer>().sprite = L;
            CSlot5.GetComponent<SpriteRenderer>().sprite = B;
            CSlot6.GetComponent<SpriteRenderer>().sprite = RightCrtl;
        }

        if (input1.currentActionMap.name == input1.actions.FindActionMap("Craft").name && input1.currentControlScheme.Contains("Gamepad")) {
            CSlot1.GetComponent<SpriteRenderer>().sprite = xb_A;
            CSlot2.GetComponent<SpriteRenderer>().sprite = xb_B;
            CSlot3.GetComponent<SpriteRenderer>().sprite = xb_Y;
            CSlot4.GetComponent<SpriteRenderer>().sprite = xb_X;
            CSlot5.GetComponent<SpriteRenderer>().sprite = xb_Gachette_L;
            CSlot6.GetComponent<SpriteRenderer>().sprite = xb_Gachette_R;
        }
        if (input2.currentActionMap.name == input2.actions.FindActionMap("Craft").name && input2.currentControlScheme.Contains("Gamepad")) {
            CSlot1.GetComponent<SpriteRenderer>().sprite = xb_A;
            CSlot2.GetComponent<SpriteRenderer>().sprite = xb_B;
            CSlot3.GetComponent<SpriteRenderer>().sprite = xb_Y;
            CSlot4.GetComponent<SpriteRenderer>().sprite = xb_X;
            CSlot5.GetComponent<SpriteRenderer>().sprite = xb_Gachette_L;
            CSlot6.GetComponent<SpriteRenderer>().sprite = xb_Gachette_R;
        }


        tr1.gameObject.transform.position = GameObject.Find("CircleTarget0").transform.position;
        tr2.gameObject.transform.position = GameObject.Find("CircleTarget1").transform.position;
        tr3.gameObject.transform.position = GameObject.Find("CircleTarget2").transform.position;
        tr4.gameObject.transform.position = GameObject.Find("CircleTarget3").transform.position;

        tr1.transform.localScale = new Vector3(keySize, keySize, 1);
        tr2.transform.localScale = new Vector3(keySize, keySize, 1);
        tr3.transform.localScale = new Vector3(keySize, keySize, 1);
        tr4.transform.localScale = new Vector3(keySize, keySize, 1);

        CSlot1.transform.localScale = new Vector3(keySize2, keySize2, 1);
        CSlot2.transform.localScale = new Vector3(keySize2, keySize2, 1);
        CSlot3.transform.localScale = new Vector3(keySize2, keySize2, 1);
        CSlot4.transform.localScale = new Vector3(keySize2, keySize2, 1);
        CSlot5.transform.localScale = new Vector3(keySize2, keySize2, 1);
        CSlot6.transform.localScale = new Vector3(keySize2, keySize2, 1);
    }

    public void SwapCslotLoc(bool swapped)
    {
        if (!swapped) {
            inputColumn.transform.position = new Vector3(-18.21f, 7.21f, 1);
            inputRow.transform.position = new Vector3(-21.21f, 3.5f, 1);
        } else {
            inputColumn.transform.position = new Vector3(-35.27f, 7.21f, 1);
            inputRow.transform.position = new Vector3(-34.4f, 3.5f, 1);
        }
    }
}
