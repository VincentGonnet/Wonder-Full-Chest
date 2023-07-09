using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentData : MonoBehaviour
{
    // Start() and Update() methods deleted - we don't need them right now

    public static PersistentData Instance;

    public bool hasFinishedTutorial = false;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
