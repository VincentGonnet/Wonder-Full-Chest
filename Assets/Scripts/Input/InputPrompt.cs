using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class InputPrompt : MonoBehaviour
{
    [SerializeField] InputActionMap actionMap;
    private int countStart = 0;
    public void Generate()
    {
        if (countStart == 0) {
            countStart = 1;
            return;
        }

        PlayerInput input1 = PlayerInput.all[0];
        PlayerInput input2 = PlayerInput.all[1];

        Debug.Log(input1.currentActionMap.name);
        Debug.Log(input2.currentActionMap.name);


        Transform tr1 = GameObject.Find("CircleTarget0").transform.GetChild(0);
        Transform tr2 = GameObject.Find("CircleTarget1").transform.GetChild(0);
        Transform tr3 = GameObject.Find("CircleTarget2").transform.GetChild(0);
        Transform tr4 = GameObject.Find("CircleTarget3").transform.GetChild(0);


        if (input1.currentActionMap.name == input1.actions.FindActionMap("Rhythm").name && input1.currentControlScheme.Contains("Keyboard")) {
            tr1.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Keys/Q");
            tr2.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Keys/W");
            tr3.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Keys/E");
            tr4.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Keys/R");
        }

        if (input2.currentActionMap.name == input2.actions.FindActionMap("Rhythm").name && input2.currentControlScheme.Contains("Keyboard")) {
            tr1.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Keys/U");
            tr2.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Keys/I");
            tr3.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Keys/O");
            tr4.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Keys/P");
        }


        tr1.gameObject.transform.position = GameObject.Find("CircleTarget0").transform.position;
        tr2.gameObject.transform.position = GameObject.Find("CircleTarget1").transform.position;
        tr3.gameObject.transform.position = GameObject.Find("CircleTarget2").transform.position;
        tr4.gameObject.transform.position = GameObject.Find("CircleTarget3").transform.position;

    }
}
