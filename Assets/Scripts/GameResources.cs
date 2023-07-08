using System;
using UnityEngine;

public static class GameResources
{
    public static readonly PrefabWithWidth[] PREFAB_BACKGROUND_LAYERS = {
        new PrefabWithWidth("Prefabs/Background/Layer0"),
        new PrefabWithWidth("Prefabs/Background/Layer1"),
        new PrefabWithWidth("Prefabs/Background/Layer2")
    };

    public static GameObject PREFAB_OBSTACLE_RABBIT = Resources.Load<GameObject>("Prefabs/Obstacles/Rabbit");
    public static GameObject PREFAB_OBSTACLE_FISH = Resources.Load<GameObject>("Prefabs/Obstacles/Fish");
    public static GameObject PREFAB_OBSTACLE_SLIME = Resources.Load<GameObject>("Prefabs/Obstacles/Slime");
    
    public static PrefabWithWidth PREFAB_RYTHM_CIRCLE = new PrefabWithWidth("Prefabs/Rythm/RythmCircle");
    public static PrefabWithWidth PREFAB_RYTHM_LONG_CIRCLE = new PrefabWithWidth("Prefabs/Rythm/RythmLongVariant");
}

public class PrefabWithWidth
{
    public readonly GameObject gameObject;
    public readonly float width;

    public PrefabWithWidth(string path)
    {
        this.gameObject = Resources.Load<GameObject>(path);
        SpriteRenderer spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        this.width = spriteRenderer.bounds.size.x;
    }
}