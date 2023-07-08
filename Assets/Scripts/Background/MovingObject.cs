using UnityEngine;

public class MovingObject : MonoBehaviour
{
    // Used for a parallax effect
    [SerializeField] private float relativeSpeed = 2f;
    private float obstacleSpeed;
    private float osuSpeed;

    public void Awake()
    {
        obstacleSpeed = GameObject.Find("GameManager").GetComponent<GameManager>().obstacleSpeed;
        osuSpeed = GameObject.Find("GameManager").GetComponent<GameManager>().osuSpeed;
    }
    
    public void FixedUpdate()
    {
        if (this.TryGetComponent<Rigidbody2D>(out Rigidbody2D component)) {
            if (this.TryGetComponent<Obstacle>(out Obstacle obstacleComp)) {
                component.velocity = new Vector2(-obstacleSpeed, 0);
            } else {
                component.velocity = new Vector2(-osuSpeed, 0);
            }
        } else {
            this.transform.position -= Constants.SCROLLING_SPEED * Time.fixedDeltaTime * relativeSpeed * Vector3.right;
        }
    }
}
