using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] LayerMask interactableLayer;
    [SerializeField] float interactDistance = 1f;
    private RaycastHit2D hit;

    void FixedUpdate()
    {
        hit = Physics2D.Raycast(transform.position, transform.up, interactDistance, interactableLayer);
        if (hit.collider != null)
        {
            Debug.DrawRay(transform.position, transform.up * interactDistance, Color.red);
        } else {
            Debug.DrawRay(transform.position, transform.up * interactDistance, Color.green);
        }
        
    }
}
