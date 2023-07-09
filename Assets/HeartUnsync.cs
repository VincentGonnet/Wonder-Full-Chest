using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartUnsync : MonoBehaviour
{
    [SerializeField] private float index;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Animator>().SetFloat("unsync", index/10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
