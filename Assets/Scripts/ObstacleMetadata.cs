using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class ObstacleMetadata
{
    public static readonly ObstacleMetadata RABBIT = new ObstacleMetadata(GameResources.PREFAB_OBSTACLE_RABBIT, GameResources.SCRIPTABLE_RABBIT, 1, 1);
    public static readonly ObstacleMetadata[] OBSTACLES = { ObstacleMetadata.RABBIT };

    public readonly GameObject prefab;
    public readonly ObstacleScriptable scriptableObject;
    public readonly int minWaveSpawn;
    public readonly int difficulty;

    private ObstacleMetadata(GameObject prefab, ObstacleScriptable scriptableObject, int minWaveSpawn, int difficulty)
    {
        this.prefab = prefab;
        this.scriptableObject = scriptableObject;
        this.minWaveSpawn = minWaveSpawn;
        this.difficulty = difficulty;
    }

    public GameObject Spawn(Vector2 position, Transform parent)
    {
        GameObject gameObject = Object.Instantiate(this.prefab, position, Quaternion.identity, parent);
        Obstacle obstacle = gameObject.GetComponent<Obstacle>();
        obstacle.data = this.scriptableObject;
        return gameObject;
    }
}