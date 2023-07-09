using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Profiling;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.InputSystem.InputAction;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] public int playerHearts = 3;
    [SerializeField] public float obstacleSpeed = 1f;
    [SerializeField] public float osuSpeed = 2;
    [SerializeField] public Camera terrainCamera;
    [SerializeField] public Transform playerTransform;
    [SerializeField] public GameObject inputPrefab;
    [SerializeField] private TextMeshProUGUI waveCounterText;

    public int wave {
        get => _wave;
        set {
            this.waveCounterText.SetText("Wave : " + value);
            _wave = value;
        }
    }
    // NEVER TOUCH THAT, IT SHOULD BE CHANGED WITH THE PROPERTY DEFINED ABOVE
    private int _wave;

    public bool scroll = true;
    public float timeDilation = 1;
    private float targetTimeSpeed = 1;
    private float timeDilationSpeed;
    private Vignette vg;

    public int tutoStep = 0;
    public bool isInTuto = false;
    private float currentFieldOfView = 38f;
    private float fovVelocity;
    private float vgVelocity;
    private float currentVignetteIntensity;
    private float currentCameraYPosition = 0.52f;
    private float cameraYPositionVelocity = 0.52f;
    private bool cameraZoomed = false;
    private bool onSameKeyboard = true;
    public bool isPaused = false;
    public GameObject tutoUi;

    private GameManager()
    {
        GameManager.instance = this;
    }

    private void Awake()
    {
        if (onSameKeyboard){
            PlayerInput.Instantiate(inputPrefab, controlScheme: "Keyboard1", pairWithDevice: Keyboard.current);
            PlayerInput.Instantiate(inputPrefab, controlScheme: "Keyboard2", pairWithDevice: Keyboard.current);
        }
    }

    private void Start()
    {
        GameObject.Find("Terrain").GetComponent<Volume>().profile.TryGet<Vignette>(out vg);
        tutoUi = GameObject.FindWithTag("TutoUI");
        tutoUi.SetActive(false);
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

    public void PlayTutorialStep()
    {
        tutoUi.SetActive(true);
        tutoUi.transform.GetChild(tutoStep).gameObject.SetActive(true);
        isPaused = true;
        isInTuto = true;
    }

    public void StopTutorialStep()
    {
        int oldStep = GameManager.instance.tutoStep;
        if (!GameManager.instance.isInTuto) return;
        GameObject localTutoUi = GameManager.instance.tutoUi;
        localTutoUi.transform.GetChild(GameManager.instance.tutoStep).gameObject.SetActive(false);
        localTutoUi.SetActive(false);
        GameManager.instance.tutoStep++;
        GameManager.instance.EndTuto();
        GameManager.instance.isInTuto = false;
    }

    public void EndTuto() {
        StartCoroutine(TutoCd());
    }

    // StopTutoStep is executed twice per press otherwise.
    private IEnumerator TutoCd()
    {
        yield return new WaitForSeconds(0.1f);
        GameManager.instance.isPaused = false;
        switch (GameManager.instance.tutoStep) {
            case 1:
                GameManager.instance.PlayTutorialStep();
                break;
            case 2:
                GameManager.instance.PlayTutorialStep();
                break;
            case 3:
                GameManager.instance.PlayTutorialStep();
                break;
            // case 4:
            //     GameManager.instance.PlayTutorialStep();
            //     break;
            default:
                break;
        }
    }
}
