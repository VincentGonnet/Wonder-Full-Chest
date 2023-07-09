using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Interact : MonoBehaviour
{
    public float detectionDistance = 10f;
    [SerializeField] private RhythmManager rhythmManager;
    [SerializeField] private CircleTarget circleTargetFirst;
    [SerializeField] private CircleTarget circleTargetSecond;
    [SerializeField] private CircleTarget circleTargetThird;
    [SerializeField] private CircleTarget circleTargetFourth;
    [SerializeField] private WaveManager waveManager;
    public int totalScore = 0;
    public int circlesHit = 0;
    private bool enteringRange = false;
    private void LateUpdate()
    {
        if (waveManager.obstacles.Count != 0 && !enteringRange) {
            Transform firstEnemyTransform = waveManager.obstacles.First().transform;
            if (firstEnemyTransform.position.x - this.transform.position.x < detectionDistance) {
                rhythmManager.GenerateRhythm();
                GameManager.instance.SlowDownTime();
                GameManager.instance.ZoomCamera();
                GameManager.instance.ShowVignette();
                enteringRange = true;
                // gameObject.GetComponent<Inventory>().UseCurrentItem(hit.collider.gameObject);
            }
        }

        totalScore = circleTargetFirst.score + circleTargetSecond.score + circleTargetThird.score + circleTargetFourth.score;
        circlesHit = totalScore + circleTargetFirst.failed + circleTargetSecond.failed + circleTargetThird.failed + circleTargetFourth.failed;
        if (rhythmManager.nbCircles.Count != 0 && circlesHit == rhythmManager.nbCircles.First()) {
            if (waveManager.obstacles[waveManager.currentObstacleIndex] == waveManager.obstacles.Last()){
                waveManager.SpawnWave();
            }
            if (totalScore == rhythmManager.nbCircles.First()) {
                //Kill the enemy
                Debug.Log("enemy kill");
                GameManager.instance.ResetTimeSpeed();
                GameManager.instance.UnZoomCamera();
                GameManager.instance.HideVignette();
                // Check if the right ItemCraft is in hand
                if (gameObject.GetComponent<Inventory>().UseCurrentItem(waveManager.obstacles[waveManager.currentObstacleIndex]))
                {
                    Debug.Log("enemy kill");
                } else {
                     waveManager.RemoveObstacle();
                }
            } else {
                GameManager.instance.DamagePlayer();
            }

            waveManager.RemoveObstacle();
            rhythmManager.EndWave();
            enteringRange = false;
            circlesHit = circleTargetFirst.score = circleTargetSecond.score = circleTargetThird.score = circleTargetFourth.score 
                = circleTargetFirst.failed = circleTargetSecond.failed = circleTargetThird.failed = circleTargetFourth.failed = 0;
            waveManager.currentObstacleIndex++;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawRay(this.transform.position, Vector2.right * this.detectionDistance);
    }

}
