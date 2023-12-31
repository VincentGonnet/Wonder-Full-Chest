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
        //obstacleSpeed = GameObject.Find("GameManager").GetComponent<GameManager>().obstacleSpeed;
        obstacleSpeed = GameManager.instance == null ? 1 : GameManager.instance.obstacleSpeed;
        //osuSpeed = GameObject.Find("GameManager").GetComponent<GameManager>().osuSpeed;
        osuSpeed = GameManager.instance == null ? 1 : GameManager.instance.osuSpeed;
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
        float speed = GameManager.instance.isPaused ? 0 : Constants.SCROLLING_SPEED * relativeSpeed * (isAffectedBySlowdown ? GameManager.instance.timeDilation : 1);
        this.transform.position += speed * Time.fixedDeltaTime * Vector3.left;
    }
}
