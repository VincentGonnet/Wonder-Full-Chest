using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interact : MonoBehaviour
{
    public float detectionDistance = 10f;
    [SerializeField] private RhythmManager rhythmManager;
    [SerializeField] private CircleTarget circleTargetTop;
    [SerializeField] private CircleTarget circleTargetBottom;
    [SerializeField] private WaveManager waveManager;
    public int totalScore = 0;
    public int circlesHit = 0;
    private bool enteringRange = false;
    
    private void LateUpdate() {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.right, this.detectionDistance, LayerMask.GetMask("Obstacles"));
        if (hit.collider != null && !enteringRange) {
            Debug.Log("Start QTE Challenge here !");
            rhythmManager.GenerateRhythm();
            enteringRange = true;
            // gameObject.GetComponent<Inventory>().UseCurrentItem(hit.collider.gameObject);
        }

        totalScore = circleTargetBottom.score + circleTargetTop.score;
        circlesHit = totalScore + circleTargetBottom.failed + circleTargetTop.failed;
        if (circlesHit == rhythmManager.nbCircles && rhythmManager.nbCircles != 0) {
            if (totalScore == rhythmManager.nbCircles) {
                //Kill the enemy
                Debug.Log("enemy kill");
                Destroy(waveManager.obstacles[0]);
                waveManager.obstacles.RemoveAt(0);
                enteringRange = false;
                rhythmManager.nbCircles = 0;
                circlesHit = 0;
                circleTargetBottom.score = 0;
                circleTargetTop.score = 0;
                circleTargetBottom.failed = 0;
                circleTargetTop.failed = 0;
            } else {
                Debug.Log("enemy killed you");
                Destroy(this.gameObject);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawRay(this.transform.position, Vector2.right * this.detectionDistance);
    }

}
