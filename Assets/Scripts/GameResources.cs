using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GameResources
{
    public static readonly PrefabWithWidth[] PREFAB_BACKGROUND_LAYERS = {
        new PrefabWithWidth("Prefabs/Background/Layer0"),
        new PrefabWithWidth("Prefabs/Background/Layer1"),
        new PrefabWithWidth("Prefabs/Background/Layer2")
    };


}

public class PrefabWithWidth
{
    public readonly GameObject gameObject;
    public readonly float width;

    public PrefabWithWidth(String path)
    {
        this.gameObject = Resources.Load<GameObject>(path);
        SpriteRenderer spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        this.width = spriteRenderer.bounds.size.x;
        Debug.Log(this.width);
    }
}