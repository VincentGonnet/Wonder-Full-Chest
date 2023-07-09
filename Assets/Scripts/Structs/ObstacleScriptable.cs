using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Obstacle", menuName = "ScriptableObjects/Obstacle", order = 1)]
public class ObstacleScriptable : ScriptableObject
{
    public ItemBase[] resolvers;
    public Sprite sprite;
    public Vector3 spriteSize;
    public int enemyId; // For animations.
}
