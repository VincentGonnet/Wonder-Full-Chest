using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class RythmManager : MonoBehaviour
{
    public bool buttonPressed;
    public bool buttonHold;

    void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.CompareTag("Circle") && buttonPressed) {
            Destroy(other.gameObject);
        } else if(other.gameObject.CompareTag("Long") && buttonHold) {
            Destroy(other.gameObject);
        }
            
    }

    public void OnButtonPressed(InputAction.CallbackContext context) {
        Debug.Log("button");
        buttonPressed = context.started;
        buttonHold = context.performed;
    }
}
