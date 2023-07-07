using UnityEngine;

class Parallax : MonoBehaviour
{
    [SerializeField] private float relativeSpeed;
    
    private void FixedUpdate()
    {
        this.transform.position -= Constants.SCROLLING_SPEED * Time.fixedDeltaTime * relativeSpeed * Vector3.right;
    }
}
