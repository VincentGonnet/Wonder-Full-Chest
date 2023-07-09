using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using TMPro;

public class PlayerSelectionHandler : MonoBehaviour
{
    System.IDisposable listener; 
    [SerializeField] GameObject m_PlayerPrefab;

    private int nbKeyboard = 0;
    private int nbGamepad = 0;

    // Start is called before the first frame update
    void OnEnable()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().isPaused = true;
        // Start a listener
        listener = InputSystem.onAnyButtonPress
        .Call(ctrl =>
        {
            var device = ctrl.device;

            if (device is Keyboard) {
                PlayerInput.Instantiate(m_PlayerPrefab, controlScheme: "Keyboard"+(nbKeyboard+1).ToString(), pairWithDevice: device);
                nbKeyboard += 1;
                GameObject.Find("Player" + (nbKeyboard + nbGamepad).ToString() + "Input").GetComponent<TextMeshProUGUI>().text = "Keyboard";
            } else if(device is Gamepad) {
                PlayerInput.Instantiate(m_PlayerPrefab, controlScheme: "Gamepad", pairWithDevice: device);
                nbGamepad += 1;
                GameObject.Find("Player" + (nbKeyboard + nbGamepad).ToString() + "Input").GetComponent<TextMeshProUGUI>().text = "Gamepad";
            }

            if(nbKeyboard + nbGamepad == 2) {
                listener.Dispose();
                StartCoroutine(Hide());
            }
        });
    }

    IEnumerator Hide() {
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
        GameObject.Find("GameManager").GetComponent<GameManager>().isPaused = false;
    }

    void OnDisable()
    {
        listener.Dispose();
    }
}
