using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Interact : MonoBehaviour
{
    [SerializeField] private RhythmManager rhythmManager;
    [SerializeField] private CircleTarget circleTargetFirst;
    [SerializeField] private CircleTarget circleTargetSecond;
    [SerializeField] private CircleTarget circleTargetThird;
    [SerializeField] private CircleTarget circleTargetFourth;
    [SerializeField] private WaveManager waveManager;
    private readonly float detectionDistance = Constants.DISTANCE_BETWEEN_OBSTACLE + 2;
    public int totalScore = 0;
    public int circlesHit = 0;
    private bool enteringRange = false;
    private void LateUpdate()
    {
        GameManager.instance.scroll = true;
        if (waveManager.obstacles.Count != 0) {
            Transform firstEnemyTransform = waveManager.obstacles.First().transform;
            GameManager.instance.scroll = firstEnemyTransform.position.x - this.transform.position.x > 2;
            if (firstEnemyTransform.position.x - this.transform.position.x < detectionDistance && !enteringRange) {
                if (GameManager.instance.tutoStep == 0) GameManager.instance.PlayTutorialStep();
                rhythmManager.GenerateRhythm();
                GameManager.instance.SlowDownTime();
                GameManager.instance.ZoomCamera();
                GameManager.instance.ShowVignette();
                enteringRange = true;
                // gameObject.GetComponent<Inventory>().UseCurrentItem(hit.collider.gameObject);
            }
        }
        GetComponent<Animator>().SetBool("walking", GameManager.instance.scroll && !GameManager.instance.isPaused);

        totalScore = circleTargetFirst.score + circleTargetSecond.score + circleTargetThird.score + circleTargetFourth.score;
        circlesHit = totalScore + circleTargetFirst.failed + circleTargetSecond.failed + circleTargetThird.failed + circleTargetFourth.failed;
        if (rhythmManager.nbCircles.Count != 0 && circlesHit == rhythmManager.nbCircles.First()) {
            if (waveManager.obstacles[0] == waveManager.obstacles.Last()){
                waveManager.SpawnWave();
                GameManager.instance.SwapRoles();
                StartCoroutine(GameManager.instance.UnzoomCameraCoroutine());
            }
            if (totalScore == rhythmManager.nbCircles.First()) {
                //Kill the enemy
                GameManager.instance.ResetTimeSpeed();
                GameManager.instance.UnZoomCamera();
                GameManager.instance.HideVignette();
                // Check if the right ItemCraft is in hand
                if (!gameObject.GetComponent<Inventory>().UseCurrentItem(waveManager.obstacles[0]))
                {
                    GameManager.instance.DamagePlayer();
                }
            } else {
                GameManager.instance.DamagePlayer();
            }
            waveManager.RemoveObstacle();

            rhythmManager.EndWave();
            enteringRange = false;
            circlesHit = circleTargetFirst.score = circleTargetSecond.score = circleTargetThird.score = circleTargetFourth.score 
                = circleTargetFirst.failed = circleTargetSecond.failed = circleTargetThird.failed = circleTargetFourth.failed = 0;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawRay(this.transform.position, Vector2.right * this.detectionDistance);
    }

}
