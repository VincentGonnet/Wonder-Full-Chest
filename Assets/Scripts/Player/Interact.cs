using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
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
            GameManager.instance.SlowDownTime();
            GameManager.instance.ZoomCamera();
            GameManager.instance.ShowVignette();
            enteringRange = true;
            // gameObject.GetComponent<Inventory>().UseCurrentItem(hit.collider.gameObject);    
        }

        totalScore = circleTargetBottom.score + circleTargetTop.score;
        circlesHit = totalScore + circleTargetBottom.failed + circleTargetTop.failed;
        if (circlesHit == rhythmManager.nbCircles && rhythmManager.nbCircles != 0) {
            if (waveManager.obstacles[waveManager.currentObstacleIndex] == waveManager.obstacles.Last()){
                waveManager.SpawnWave();
            }
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
                GameManager.instance.ResetTimeSpeed();
                GameManager.instance.UnZoomCamera();
                GameManager.instance.HideVignette();
            } else {
                // Check if the right ItemCraft is in hand
                if (gameObject.GetComponent<Inventory>().UseCurrentItem(waveManager.obstacles[waveManager.currentObstacleIndex]))
                {
                    Debug.Log("enemy kill");
                    Destroy(this.gameObject);
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                } else {
                    Destroy(waveManager.obstacles[waveManager.currentObstacleIndex]); 
                }
            } else {
                GameObject.Find("GameManager").GetComponent<GameManager>().DamagePlayer();
                Destroy(waveManager.obstacles[waveManager.currentObstacleIndex]);
            }

            enteringRange = false;
            rhythmManager.nbCircles = 0;
            circlesHit = 0;
            circleTargetBottom.score = 0;
            circleTargetTop.score = 0;
            circleTargetBottom.failed = 0;
            circleTargetTop.failed = 0;
            waveManager.currentObstacleIndex++;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawRay(this.transform.position, Vector2.right * this.detectionDistance);
    }

}
