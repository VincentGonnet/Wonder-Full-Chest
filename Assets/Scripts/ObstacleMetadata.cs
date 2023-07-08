using UnityEngine;
using Object = UnityEngine.Object;

public class ObstacleMetadata
{
    public static readonly ObstacleMetadata RABBIT = new ObstacleMetadata(GameResources.PREFAB_OBSTACLE_RABBIT, 1, 1);
    public static readonly ObstacleMetadata FISH = new ObstacleMetadata(GameResources.PREFAB_OBSTACLE_FISH, 1, 1);
    public static readonly ObstacleMetadata SLIME = new ObstacleMetadata(GameResources.PREFAB_OBSTACLE_SLIME, 1, 1);
    // These should be ordered in the order of difficulty, in increasing order
    public static readonly ObstacleMetadata[] OBSTACLES = { RABBIT, FISH, SLIME };

    public readonly GameObject prefab;
    public readonly int minWaveSpawn;
    public readonly int difficulty;

    private ObstacleMetadata(GameObject prefab, int minWaveSpawn, int difficulty)
    {
        this.prefab = prefab;
        this.minWaveSpawn = minWaveSpawn;
        this.difficulty = difficulty;
    }

    public GameObject Spawn(Vector2 position, Transform parent)
    {
        return Object.Instantiate(this.prefab, position, Quaternion.identity, parent);
    }
}