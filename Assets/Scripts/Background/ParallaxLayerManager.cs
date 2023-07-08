using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public class ParallaxLayerManager : MonoBehaviour
{
    private readonly List<GameObject> backgrounds = new();
    [SerializeField] private int layerId;
    private PrefabWithWidth prefab;

    private void Start()
    {
        this.prefab = GameResources.PREFAB_BACKGROUND_LAYERS[layerId];
        // We need at least one background to avoid crashes in Update
        this.backgrounds.Add(Object.Instantiate(
            this.prefab.gameObject,
            new Vector3(-11, 0) + this.transform.position,
            Quaternion.identity,
            this.transform));
    }

    private void FixedUpdate()
    {
        if (this.backgrounds.Last().transform.position.x + this.prefab.width / 2 < 100) {
            float position = this.backgrounds.Last().transform.position.x + this.prefab.width;
            GameObject gameObject = Object.Instantiate(
                this.prefab.gameObject,
                new Vector3(position, 0) + this.transform.position,
                Quaternion.identity,
                this.transform);
            // Because it won't update this frame
            gameObject.GetComponent<MovingObject>().FixedUpdate();
            this.backgrounds.Add(gameObject);
        }
        if (this.backgrounds.First().transform.position.x + this.prefab.width / 2 < -11) {
            Object.Destroy(backgrounds[0]);
            this.backgrounds.RemoveAt(0);
        }
    }
}
