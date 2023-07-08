using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = System.Random;

public class RythmManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Transform[] circleTargetTransforms;

    private List<GameObject> circles = new();
    private Random random = new();

    private void Start()
    {
        GenerateRythm();
    }


    private void GenerateRythm()
    {
        float inBetweenDistance = Mathf.Pow(1.05f, -this.gameManager.wave) + Constants.MIN_BASE_CIRCLE_RYTHM_DISTANCE;
        this.GenerateRythmRow(circleTargetTransforms[0], inBetweenDistance);
        this.GenerateRythmRow(circleTargetTransforms[1], inBetweenDistance);
    }

    private void GenerateRythmRow(Transform targetTransform, float inBetweenDistance)
    {
        for (float x = 0; x < Constants.RYTHM_DISTANCE; x += inBetweenDistance) {
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
