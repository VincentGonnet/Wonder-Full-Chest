using UnityEngine;

public class MovingObject : MonoBehaviour
{
    // Used for a parallax effect
    [SerializeField] public float relativeSpeed = 1f;
    [SerializeField] private bool isAffectedBySlowdown;
    private float obstacleSpeed;
    private float osuSpeed;

    public void Awake()
    {
        obstacleSpeed = GameObject.Find("GameManager").GetComponent<GameManager>().obstacleSpeed;
        osuSpeed = GameObject.Find("GameManager").GetComponent<GameManager>().osuSpeed;
        if (this.TryGetComponent<Rigidbody2D>(out Rigidbody2D component)) {
            if (this.TryGetComponent<Obstacle>(out Obstacle obstacleComp)) {
                relativeSpeed = obstacleSpeed;
            } else {
                relativeSpeed = osuSpeed;
            }
        }
    }
    
    public void FixedUpdate()
    {
        if (!GameManager.instance.scroll && isAffectedBySlowdown) {
            return;
        }
        float speed = Constants.SCROLLING_SPEED * relativeSpeed * (isAffectedBySlowdown ? GameManager.instance.timeDilation : 1);
        if (this.TryGetComponent<Rigidbody2D>(out Rigidbody2D component)) {
            component.velocity = speed * Vector3.left;
        } else {
            this.transform.position += speed * Time.fixedDeltaTime * Vector3.left;
        }
    }
}
