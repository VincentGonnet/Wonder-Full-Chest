using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movementVector;
    private Quaternion rotation;
    private Vector2 smoothedInput;
    private Vector2 movementInputSmoothedVelocity;

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
        rb.MoveRotation(rotation);
    }

    private void OnMove(InputValue inputValue)
    {
        movementVector = inputValue.Get<Vector2>();
    }

    // Gamepad right stick input.
    private void OnLook(InputValue inputValue)
    {
        smoothedInput = Vector2.SmoothDamp(smoothedInput, inputValue.Get<Vector2>(), ref movementInputSmoothedVelocity, 0.1f);
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, smoothedInput);
        rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
    }

    // Mouse position input.
    private void OnMouse(InputValue inputValue)
    {
        // Set rotation towards mouse position.
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(inputValue.Get<Vector2>());
        Vector2 direction = mousePosition - rb.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

}
