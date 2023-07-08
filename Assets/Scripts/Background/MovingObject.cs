using UnityEngine;

public class MovingObject : MonoBehaviour
{
    // Used for a parallax effect
    [SerializeField] private float relativeSpeed = 1;
    
    private void FixedUpdate()
    {
        this.transform.position -= Constants.SCROLLING_SPEED * Time.fixedDeltaTime * relativeSpeed * Vector3.right;
    }
}
