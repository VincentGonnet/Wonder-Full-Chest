using UnityEngine;
using Object = UnityEngine.Object;

public class ObstacleMetadata
{
    public static readonly ObstacleMetadata RABBIT = new ObstacleMetadata(GameResources.PREFAB_OBSTACLE_RABBIT, 1, 1);
    public static readonly ObstacleMetadata FISH = new ObstacleMetadata(GameResources.PREFAB_OBSTACLE_FISH, 1, 1);
    public static readonly ObstacleMetadata SLIME = new ObstacleMetadata(GameResources.PREFAB_OBSTACLE_SLIME, 1, 1);
    public static readonly ObstacleMetadata FARMER = new ObstacleMetadata(GameResources.PREFAB_OBSTACLE_FARMER, 1, 1);
    public static readonly ObstacleMetadata ANGEL = new ObstacleMetadata(GameResources.PREFAB_OBSTACLE_ANGEL, 1, 1);
    public static readonly ObstacleMetadata DEMON = new ObstacleMetadata(GameResources.PREFAB_OBSTACLE_DEMON, 1, 1);
    public static readonly ObstacleMetadata DINOSAUR = new ObstacleMetadata(GameResources.PREFAB_OBSTACLE_DINOSAUR, 1, 1);
    public static readonly ObstacleMetadata FAIRY = new ObstacleMetadata(GameResources.PREFAB_OBSTACLE_FAIRY, 1, 1);
    public static readonly ObstacleMetadata GHOST = new ObstacleMetadata(GameResources.PREFAB_OBSTACLE_GHOST, 1, 1);
    public static readonly ObstacleMetadata SNAKE = new ObstacleMetadata(GameResources.PREFAB_OBSTACLE_SNAKE, 1, 1);
    public static readonly ObstacleMetadata SOLDIER = new ObstacleMetadata(GameResources.PREFAB_OBSTACLE_SOLDIER, 1, 1);
    public static readonly ObstacleMetadata WEREWOLF = new ObstacleMetadata(GameResources.PREFAB_OBSTACLE_WEREWOLF, 1, 1);
    // These should be ordered in the order of difficulty, in increasing order
    public static readonly ObstacleMetadata[] OBSTACLES = { RABBIT, FISH, SLIME, FARMER, ANGEL, DEMON, DINOSAUR, FAIRY, GHOST, SNAKE, SOLDIER, WEREWOLF };

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