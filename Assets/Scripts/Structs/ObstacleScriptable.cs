using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Obstacle", menuName = "ScriptableObjects/Obstacle", order = 1)]
public class ObstacleScriptable : ScriptableObject
{
    public string name;
    public string[] resolvers;
    public string[] drops;
    public Sprite sprite;
    public Vector3 spriteSize;
}
