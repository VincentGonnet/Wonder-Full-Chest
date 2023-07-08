using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public class LayerManager : MonoBehaviour
{
    private readonly List<GameObject> backgrounds = new();
    [SerializeField] private int layerId;
    private PrefabWithWidth prefab;

    void Start()
    {
        this.prefab = GameResources.PREFAB_BACKGROUND_LAYERS[layerId];
        // We need at least one background to avoid crashes in Update
        this.backgrounds.Add(Object.Instantiate(
            this.prefab.gameObject,
            new Vector3(-11, 0),
            Quaternion.identity,
            this.transform));
    }

    void FixedUpdate()
    {
        if (this.backgrounds.First().transform.position.x + this.prefab.width / 2 < -11) {
            Object.Destroy(backgrounds[0]);
            this.backgrounds.RemoveAt(0);
        }
        if (this.backgrounds.Last().transform.position.x + this.prefab.width / 2 < 11) {
            float position = this.backgrounds.Last().transform.position.x + this.prefab.width;
            this.backgrounds.Add(Object.Instantiate(
                this.prefab.gameObject,
                new Vector3(position, 0) + this.transform.position,
                Quaternion.identity,
                this.transform));
        }
    }
}
