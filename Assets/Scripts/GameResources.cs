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
    public static GameObject PREFAB_OBSTACLE_FARMER = Resources.Load<GameObject>("Prefabs/Obstacles/Farmer");
    public static GameObject PREFAB_OBSTACLE_ANGEL = Resources.Load<GameObject>("Prefabs/Obstacles/Angel");
    public static GameObject PREFAB_OBSTACLE_DEMON = Resources.Load<GameObject>("Prefabs/Obstacles/Demon");
    public static GameObject PREFAB_OBSTACLE_DINOSAUR = Resources.Load<GameObject>("Prefabs/Obstacles/Dinosaur");
    public static GameObject PREFAB_OBSTACLE_FAIRY = Resources.Load<GameObject>("Prefabs/Obstacles/Fairy");
    public static GameObject PREFAB_OBSTACLE_GHOST = Resources.Load<GameObject>("Prefabs/Obstacles/Ghost");
    public static GameObject PREFAB_OBSTACLE_SNAKE = Resources.Load<GameObject>("Prefabs/Obstacles/Snake");
    public static GameObject PREFAB_OBSTACLE_SOLDIER = Resources.Load<GameObject>("Prefabs/Obstacles/Soldier");
    public static GameObject PREFAB_OBSTACLE_WEREWOLF = Resources.Load<GameObject>("Prefabs/Obstacles/Werewolf");
    
    public static PrefabWithWidth PREFAB_RHYTHM_CIRCLE = new PrefabWithWidth("Prefabs/Rhythm/RhythmCircle");
    public static GameObject PREFAB_RHYTHM_HOLD_CIRCLE = Resources.Load<GameObject>("Prefabs/Rhythm/RhythmLongVariant");
    public static GameObject PREFAB_RHYTHM_HOLD_CIRCLE_SHORT = Resources.Load<GameObject>("Prefabs/Rhythm/RhythmLongVariant Short");
    public static GameObject PREFAB_RHYTHM_HOLD_CIRCLE_LONG = Resources.Load<GameObject>("Prefabs/Rhythm/RhythmLongVariant Long");

    public static Sprite SPRITE_ITEM_CROSS = Resources.Load<Sprite>("Sprites/Items/Cross");
    public static Sprite SPRITE_ITEM_EVIL_HAMMER = Resources.Load<Sprite>("Sprites/Items/Marteau_demoniaque");
    public static Sprite SPRITE_ITEM_FISHING_ROD = Resources.Load<Sprite>("Sprites/Items/Canne_a_peche");
    public static Sprite SPRITE_ITEM_GOLD_BAG = Resources.Load<Sprite>("Sprites/Items/Gold-Bag");
    public static Sprite SPRITE_ITEM_HAMMER = Resources.Load<Sprite>("Sprites/Items/Masse");
    public static Sprite SPRITE_ITEM_HOLY_SWORD = Resources.Load<Sprite>("Sprites/Items/epee_benie");
    public static Sprite SPRITE_ITEM_NET = Resources.Load<Sprite>("Sprites/Items/Net");
    public static Sprite SPRITE_ITEM_PAN = Resources.Load<Sprite>("Sprites/Items/PAN");
    public static Sprite SPRITE_ITEM_POISON = Resources.Load<Sprite>("Sprites/Items/Poison");
    public static Sprite SPRITE_ITEM_SHOES = Resources.Load<Sprite>("Sprites/Items/chaussure");
    public static Sprite SPRITE_ITEM_SILVER_DAGGER = Resources.Load<Sprite>("Sprites/Items/Dague");
    public static Sprite SPRITE_ITEM_SLING_SHOT = Resources.Load<Sprite>("Sprites/Items/lancepierre");
    public static Sprite SPRITE_ITEM_BLOOD_VIAL = Resources.Load<Sprite>("Sprites/Items/Poison_bouteille");
    public static Sprite SPRITE_ITEM_SPEAR = Resources.Load<Sprite>("Sprites/Items/javelot");
    public static Sprite SPRITE_ITEM_SWORD = Resources.Load<Sprite>("Sprites/Items/sword");

    public static GameObject PREFAB_CRAFT_INVENTORY_PREVIEW =
        Resources.Load<GameObject>("Prefabs/Inventory/CraftInventoryPreview");
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