using UnityEngine;
using UnityEngine.InputSystem;

public class CircleTarget : MonoBehaviour
{
    [SerializeField] private RhythmManager rhythmManager;
    
    public int score = 0;
    public int failed = 0;

    private bool canBeActivated = false;
    private bool canBeDeactivated = false;
    private bool isActivated = false;
    private GameObject circle = null;
    private bool skipFail = false;
    
    private void OnTriggerEnter2D(Collider2D other) {
        skipFail = false;
        if (other.gameObject.CompareTag("Circle")) {
            canBeActivated = canBeDeactivated = true;
            circle = other.gameObject;
        } else if (other.gameObject.CompareTag("Long") && other.gameObject.layer == LayerMask.NameToLayer("RhythmCircleTrigger")) {
            canBeActivated = true;
            circle = other.transform.parent.gameObject;
        } else if (!other.gameObject.CompareTag("Long") && other.gameObject.layer == LayerMask.NameToLayer("RhythmCircleTrigger")) {
            canBeDeactivated = true;
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("RhythmCircle")) {
            circle = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Circle")) {
            canBeActivated = canBeDeactivated = false;
            if (!skipFail) OnFail();
            else skipFail = false;
        } else if (!other.gameObject.CompareTag("Long") && other.gameObject.layer == LayerMask.NameToLayer("RhythmCircleTrigger")) {
            canBeDeactivated = false;
        } else if (other.gameObject.CompareTag("Long") && other.gameObject.layer == LayerMask.NameToLayer("RhythmCircleTrigger")) {
            canBeActivated = false;
            if (!isActivated) OnFail();
        }
    }

    public void OnButtonPressed(InputAction.CallbackContext context) {
        if (context.started) {
            if (canBeActivated) {
                isActivated = true;
                OnPress();
            }
            // else {
            //     OnFail();
            // }
        } else if (context.canceled) {
            if (canBeDeactivated) OnSuccess();
            else if (isActivated) OnFail();
        }
    }

    public void OnPress() {
        circle?.GetComponent<Animator>()?.SetTrigger("Press");
    }

    public void OnFail() {
        // Animate as failed
        skipFail = true;
        this.rhythmManager.DeleteCircle(circle);
        Destroy(circle, 4);
        circle?.GetComponent<Animator>()?.SetTrigger("Destroy");
        circle = null;
        isActivated = false;
        failed++;
        Debug.Log("Failed !");
    }

    public void OnSuccess() {
        // Animate as succeeded
        skipFail = true;
        this.rhythmManager.DeleteCircle(circle);
        Destroy(circle, 4);
        circle?.GetComponent<Animator>()?.SetTrigger("Success");
        circle = null;
        isActivated = false;
        score++;
        Debug.Log("GG !");
    }
}