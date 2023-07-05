using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movementVector;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movementVector * movementSpeed * Time.fixedDeltaTime);
    }

    private void OnMove(InputValue inputValue)
    {
        movementVector = inputValue.Get<Vector2>();
    }
}
