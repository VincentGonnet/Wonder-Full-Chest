using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + Constants.SCROLLING_SPEED * Time.fixedDeltaTime * Vector2.left);
    }
}
