using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = System.Random;

public class RhythmManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Transform[] circleTargetTransforms;

    // Circles are divided into waves (one wave is one enemy)
    private List<List<GameObject>> circles = new();
    private Random random = new();
    public List<int> nbCircles = new();

    public void GenerateRhythm()
    {
        List<GameObject> wave = new();
        float inBetweenDistance = Mathf.Pow(1.1f, -this.gameManager.wave) * 20 + Constants.MIN_BASE_CIRCLE_RHYTHM_DISTANCE;
        float startFrom = circles.Count != 0 && circles.Last().Count != 0
            ? circles.Last().Select((circle) => circle.transform.localPosition.x).Max()
            : 0;
        int circlesCount = 0;
        float speed = 10 - Mathf.Exp((225 - gameManager.wave) / 100f);
        foreach (Transform targetTransform in circleTargetTransforms) {
            this.GenerateRhythmRow(wave, ref circlesCount, targetTransform, inBetweenDistance, speed, startFrom);
        }

        this.circles.Add(wave);
        this.nbCircles.Add(circlesCount);
    }

    public void EndWave()
    {
        circles.RemoveAt(0);
        nbCircles.RemoveAt(0);
    }

    public void DeleteCircle(GameObject circle)
    {
        circles.First().Remove(circle);
    }

    private void GenerateRhythmRow(List<GameObject> wave, ref int circlesCount, Transform targetTransform, float inBetweenDistance, float speed, float startFrom = 0f)
    {
        float x = 0;
        for (x = 0; x < Constants.RHYTHM_DISTANCE; x += inBetweenDistance) {
            int offset = Mathf.FloorToInt((float) this.random.NextDouble() * 10f);
            offset = this.random.Next() == 0 ? offset : -offset;
            x += Constants.MIN_BASE_CIRCLE_RHYTHM_DISTANCE * offset / 10;
            double randomDouble = this.random.NextDouble();
            if (this.random.NextDouble() < 0.4) {
                GameObject prefab = randomDouble < 0.1 ? GameResources.PREFAB_RYTHM_HOLD_CIRCLE_LONG : randomDouble < 0.2 ?
                    GameResources.PREFAB_RYTHM_HOLD_CIRCLE : GameResources.PREFAB_RYTHM_HOLD_CIRCLE_SHORT;
                // Make the left of the long circle be at x
                x += prefab.GetComponent<BoxCollider2D>().size.x / 2 + 0.5f;
                GameObject circle = Object.Instantiate(
                    prefab.gameObject,
                    new Vector3(x + 5, 0) + targetTransform.position +
                    new Vector3(startFrom + Constants.MIN_BASE_CIRCLE_RHYTHM_DISTANCE, 0),
                    Quaternion.identity,
                    targetTransform);
                circle.GetComponent<MovingObject>().relativeSpeed = speed;
                wave.Add(circle);

                // And then set x to the right of the long circle
                x += prefab.GetComponent<BoxCollider2D>().size.x / 2 + 0.5f;
                circlesCount++;
            }
            
            else {
                // Make the left of the circle be at x
                x += GameResources.PREFAB_RYTHM_CIRCLE.width / 2;
                GameObject circle = Object.Instantiate(
                    GameResources.PREFAB_RYTHM_CIRCLE.gameObject,
                    new Vector3(x + 5, 0) + targetTransform.position + new Vector3(startFrom + 10, 0),
                    Quaternion.identity,
                    targetTransform);
                circle.GetComponent<MovingObject>().relativeSpeed = speed;
                wave.Add(circle);
                // And then set x to the right of the circle
                x += GameResources.PREFAB_RYTHM_CIRCLE.width / 2;
                circlesCount++;
            }
        }
    }
}
