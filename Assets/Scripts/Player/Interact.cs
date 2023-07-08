using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public float detectionDistance = 5f;
    
    private void LateUpdate() {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.right, this.detectionDistance, LayerMask.GetMask("Obstacles"));
        if (hit.collider != null) {
            Debug.Log("Start QTE Challenge here !");
            // gameObject.GetComponent<Inventory>().UseCurrentItem(hit.collider.gameObject);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawRay(this.transform.position, Vector2.right * this.detectionDistance);
    }
}
