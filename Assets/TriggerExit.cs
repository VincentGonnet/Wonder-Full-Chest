using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerExit : MonoBehaviour
{
    public bool exit = false;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("RythmButton")) {
            exit = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("RythmButton")) {
            exit = false;
        }
    }
}
