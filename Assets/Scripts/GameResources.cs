using System;
using UnityEngine;

public static class GameResources
{
    public static readonly PrefabWithWidth[] PREFAB_BACKGROUND_LAYERS = {
        new PrefabWithWidth("Prefabs/Background/Ground"),
        new PrefabWithWidth("Prefabs/Background/Shadows"),
        new PrefabWithWidth("Prefabs/Background/Trees"),
        new PrefabWithWidth("Prefabs/Background/Mountains"),
        new PrefabWithWidth("Prefabs/Background/Sky")
    };

    public static GameObject PREFAB_OBSTACLE_RABBIT = Resources.Load<GameObject>("Prefabs/Obstacles/Rabbit");
    public static GameObject PREFAB_OBSTACLE_FISH = Resources.Load<GameObject>("Prefabs/Obstacles/Fish");
    public static GameObject PREFAB_OBSTACLE_SLIME = Resources.Load<GameObject>("Prefabs/Obstacles/Slime");
    
    public static PrefabWithWidth PREFAB_RYTHM_CIRCLE = new PrefabWithWidth("Prefabs/Rythm/RythmCircle");
    public static GameObject PREFAB_RYTHM_HOLD_CIRCLE = Resources.Load<GameObject>("Prefabs/Rythm/RythmLongVariant");
    public static GameObject PREFAB_RYTHM_HOLD_CIRCLE_SHORT = Resources.Load<GameObject>("Prefabs/Rythm/RythmLongVariant Short");
    public static GameObject PREFAB_RYTHM_HOLD_CIRCLE_LONG = Resources.Load<GameObject>("Prefabs/Rythm/RythmLongVariant Long");
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