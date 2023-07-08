using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSettings : MonoBehaviour
{
    // Start() and Update() methods deleted - we don't need them right now

    public static MainSettings Instance;

    public bool shareKeyboard = true;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetSharedKeyboard(bool shared)
    {
        this.shareKeyboard = shared;
    }
}
