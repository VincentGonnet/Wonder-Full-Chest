using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = System.Random;

public class RhythmManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Transform[] circleTargetTransforms;

    private List<GameObject> circles = new();
    private Random random = new();

    private void Start()
    {
        GenerateRhythm();
    }

    private void FixedUpdate()
    {
    }

    private void GenerateRhythm()
    {
        float inBetweenDistance = Mathf.Pow(1.05f, -this.gameManager.wave) + Constants.MIN_BASE_CIRCLE_RHYTHM_DISTANCE;
        this.GenerateRhythmRow(circleTargetTransforms[0], inBetweenDistance);
        this.GenerateRhythmRow(circleTargetTransforms[1], inBetweenDistance);
    }

    public void DeleteCircle(GameObject circle)
    {
        circles.Remove(circle);
    }

    private void GenerateRhythmRow(Transform targetTransform, float inBetweenDistance)
    {
        for (float x = 0; x < Constants.RHYTHM_DISTANCE; x += inBetweenDistance) {
            int offset = Mathf.FloorToInt((float) this.random.NextDouble() * 10f);
            offset = this.random.Next() == 0 ? offset : -offset;
            x += inBetweenDistance * offset / 10;
            if (this.random.NextDouble() < 1) {
                // Make the left of the long circle be at x
                x += GameResources.PREFAB_RYTHM_LONG_CIRCLE.width / 2;
                this.circles.Add(Object.Instantiate(
                    GameResources.PREFAB_RYTHM_LONG_CIRCLE.gameObject,
                    new Vector3(x + 5, 0) + targetTransform.position,
                    Quaternion.identity,
                    targetTransform));
                // And then set x to the right of the long circle
                x += GameResources.PREFAB_RYTHM_LONG_CIRCLE.width / 2;
            }
            else {
                // Make the left of the circle be at x
                x += GameResources.PREFAB_RYTHM_CIRCLE.width / 2;
                this.circles.Add(Object.Instantiate(
                    GameResources.PREFAB_RYTHM_CIRCLE.gameObject,
                    new Vector3(x + 5, 0) + targetTransform.position,
                    Quaternion.identity,
                    targetTransform));
                // And then set x to the right of the circle
                x += GameResources.PREFAB_RYTHM_CIRCLE.width / 2;
            }
        }
    }
}
