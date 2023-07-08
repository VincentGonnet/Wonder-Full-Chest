using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class RythmManager : MonoBehaviour
{
    private bool buttonPressed;
    private bool buttonHold;
    public TriggerExit exit;
    public int score = 0;

    private int failed = 0;

    void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.CompareTag("Circle") && buttonPressed) {
            Destroy(other.gameObject);
            score = score++;
        } else if(other.gameObject.CompareTag("Long") && !buttonPressed) {
            buttonHold = false;
        }

        if (buttonHold && failed == 0) {
            failed = 1;
        }

        if (failed == 1 && buttonHold) {
            failed = 2;
        }

        Debug.Log("Exit:"+exit.exit);
        Debug.Log("buttonHold :"+buttonHold);
        Debug.Log("failed :"+failed);


        if (other.gameObject.CompareTag("Long") && exit.exit && buttonHold && failed == 1) {
            Debug.Log("gg");
            score++;
            Destroy(other.gameObject);
        }
    }

    // void OnTriggerExit2D(Collider2D other) {
    //     Destroy(other.gameObject);
    //     if (buttonHold) {
    //         score++;
    //     }
    // }

    public void OnButtonPressed(InputAction.CallbackContext context) {
        buttonPressed = context.started;
        if (buttonPressed) {
            buttonHold = true;
        }
    }
}
