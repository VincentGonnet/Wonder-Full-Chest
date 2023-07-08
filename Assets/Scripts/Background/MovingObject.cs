using UnityEngine;

public class MovingObject : MonoBehaviour
{
    // Used for a parallax effect
    [SerializeField] private float relativeSpeed = 1;
    
    public void FixedUpdate()
    {
        if (this.TryGetComponent<Rigidbody2D>(out Rigidbody2D component)) {
            component.velocity = new Vector2(-1, 0);
        } else {
            this.transform.position -= Constants.SCROLLING_SPEED * Time.fixedDeltaTime * relativeSpeed * Vector3.right;
        }
    }
}
