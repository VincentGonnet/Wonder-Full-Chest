using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChooseInputLayout : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        if (!GameObject.Find("OptionsHolder").GetComponent<MainSettings>().shareKeyboard) {
            this.gameObject.GetComponent<PlayerInputManager>().enabled = true;
            this.gameObject.GetComponent<PlayerInput>().enabled = false;
        }
    }
}
