using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Profiling;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [SerializeField] public int playerHearts = 3;
    [SerializeField] public float obstacleSpeed = 1f;
    [SerializeField] public float osuSpeed = 2;
    [SerializeField] public Camera terrainCamera;
    [SerializeField] public Transform playerTransform;
    [SerializeField] public GameObject inputPrefab;
    public int wave;
    public bool scroll = true;
    public float timeDilation = 1;
    private float targetTimeSpeed = 1;
    private float timeDilationSpeed;
    private Vignette vg;

    private float currentFieldOfView = 38f;
    private float fovVelocity;
    private float vgVelocity;
    private float currentVignetteIntensity;
    private float currentCameraYPosition = 0.52f;
    private float cameraYPositionVelocity = 0.52f;
    private bool cameraZoomed = false;
    private bool onSameKeyboard = true;

    private GameManager()
    {
        GameManager.instance = this;
    }

    private void Awake()
    {

    }

    private void Start()
    {
        GameObject.Find("Terrain").GetComponent<Volume>().profile.TryGet<Vignette>(out vg);
    }

    private void Update()
    {
        if (cameraZoomed) {
            currentCameraYPosition = -1.3f + playerTransform.localPosition.y - -1.213943f;
        }
        terrainCamera.fieldOfView = Mathf.SmoothDamp(terrainCamera.fieldOfView, currentFieldOfView, ref fovVelocity, 0.3f);
        terrainCamera.transform.localPosition = new Vector3(
            terrainCamera.transform.localPosition.x,
            Mathf.SmoothDamp(terrainCamera.transform.localPosition.y, currentCameraYPosition, ref cameraYPositionVelocity, 0.3f),
            terrainCamera.transform.localPosition.z);
        vg.intensity.value = Mathf.SmoothDamp(vg.intensity.value, currentVignetteIntensity, ref vgVelocity, 0.5f);
    }

    public void ZoomCamera()
    {
        currentFieldOfView = 16.6f;
        currentCameraYPosition = -1.3f;
        cameraZoomed = true;
    }

    public void UnZoomCamera()
    {
        currentFieldOfView = 38f;
        currentCameraYPosition = 0.52f;
        cameraZoomed = false;
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
        // Get current heart
        GameObject health = GameObject.Find("Health");
        health.transform.GetChild(--this.playerHearts).GetComponent<Animator>().SetBool("full", false);
        if (this.playerHearts < 1) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void FixedUpdate()
    {
        this.timeDilation = Mathf.SmoothDamp(this.timeDilation, this.targetTimeSpeed, ref timeDilationSpeed, 1.0f);
    }
}
