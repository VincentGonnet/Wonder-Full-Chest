using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [SerializeField] public int playerHearts = 3;
    [SerializeField] public float obstacleSpeed = 1f;
    [SerializeField] public float osuSpeed = 2;
    [SerializeField] public Camera terrainCamera;
    public int wave;

    public float timeDilation = 1;
    private float targetTimeSpeed = 1;
    private float timeDilationSpeed;
    private Vignette vg;
    private float currentFieldOfView = 20f;
    private float fovVelocity;
    private float vgVelocity;
    private float currentVignetteIntensity = 0f;
    private GameObject inputManagerP1;
    private GameObject inputManagerP2;

    private GameManager()
    {
        GameManager.instance = this;
    }

    private void Start()
    {
        GameObject.Find("Terrain").GetComponent<Volume>().profile.TryGet<Vignette>(out vg);
             
    }

    public void SendReverseCommand()
    {
        if (GameObject.FindGameObjectsWithTag("PlayerInput").Count() > 0) {
            Debug.Log("FOUND ! SWAP");
            inputManagerP1 = GameObject.FindGameObjectsWithTag("PlayerInput")[0];
            inputManagerP2 = GameObject.FindGameObjectsWithTag("PlayerInput")[1];
            inputManagerP1.GetComponent<PlayerInputHandler>().SwapRoles();
            inputManagerP2.GetComponent<PlayerInputHandler>().SwapRoles();
        }  
        
    }

    private void Update()
    {
        terrainCamera.fieldOfView = Mathf.SmoothDamp(terrainCamera.fieldOfView, currentFieldOfView, ref fovVelocity, 0.3f);

        vg.intensity.value = Mathf.SmoothDamp(vg.intensity.value, currentVignetteIntensity, ref vgVelocity, 0.5f) ;
    }

    public void ZoomCamera()
    {
        currentFieldOfView = 15f;
    }

    public void UnZoomCamera()
    {
        currentFieldOfView = 20f;
    }

    public void ShowVignette()
    {
        currentVignetteIntensity = 0.1f;
    }

    public void HideVignette()
    {
        currentVignetteIntensity = 0f;
    }

    public void SlowDownTime()
    {
        this.targetTimeSpeed = 0.3f;
    }

    public void ResetTimeSpeed()
    {
        this.targetTimeSpeed = 1;
    }
    
    public void DamagePlayer()
    {
        Debug.Log("Took damage");
        this.playerHearts -= 1;
        if (this.playerHearts <= 0) {

        }
    }

    public void FixedUpdate()
    {
        this.timeDilation = Mathf.SmoothDamp(this.timeDilation, this.targetTimeSpeed, ref timeDilationSpeed, 1.0f);
    }
}
