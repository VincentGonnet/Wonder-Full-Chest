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

    [SerializeField] public int playerHearts;
    [SerializeField] public float obstacleSpeed = 0.25f;
    [SerializeField] public float osuSpeed = 3f;
    [SerializeField] public Camera terrainCamera;
    [SerializeField] public Transform playerTransform;
    [SerializeField] public GameObject inputPrefab;
    [SerializeField] private TextMeshProUGUI waveCounterText;

    public int wave {
        get => _wave;
        set {
            this.waveCounterText.SetText("Wave : " + (value == 100 ? "Vivaldi" : value));
            _wave = value;
        }
    }
    // NEVER TOUCH THAT, IT SHOULD BE CHANGED WITH THE PROPERTY DEFINED ABOVE
    private int _wave;

    public bool scroll = true;
    public float timeDilation = 1;
    private float targetTimeSpeed = 0.6f;
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
    public bool isPaused = false;
    public GameObject tutoUi;
    private bool uiswapped = false;
    private GameManager()
    {
        GameManager.instance = this;
        
    }
    
    private void Start()
    {
        GameObject.Find("Terrain").GetComponent<Volume>().profile.TryGet<Vignette>(out vg);
        tutoUi = GameObject.FindWithTag("TutoUI");
        tutoUi.SetActive(false);
        SoundManager.instance.StartGameMusic();
        //persistentData = GameObject.Find("PersistentData");

        
        // StartCoroutine(Swap());

    }

    // private IEnumerator Swap()
    // {
    //     yield return new WaitForSeconds(7);
    //     SwapRoles();
    //     StartCoroutine(Swap());

    // }

    private void Update()
    {
        if (cameraZoomed) {
            currentCameraYPosition = -1.24f + playerTransform.localPosition.y - -1.213943f;
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
        currentFieldOfView = 20f;
        currentCameraYPosition = -1.3f;
        cameraZoomed = true;
    }

    public void UnZoomCamera()
    {
        currentFieldOfView = 38f;
        currentCameraYPosition = 0.52f;
        cameraZoomed = false;
    }

    public IEnumerator UnzoomCameraCoroutine()
    {
        yield return new WaitForSeconds(2);
        this.UnZoomCamera();
        this.ResetTimeSpeed();
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
    
    public void SwapRoles()
    {
        StartCoroutine(SwapRolesAnimation());
    }

    IEnumerator SwapRolesAnimation() {
        GameObject.Find("Swap").GetComponent<SwapUIManager>().StartAnimation();
        yield return new WaitForSeconds(0.9f);
        PlayerInput.all[0].gameObject.GetComponent<PlayerInputHandler>().SwapRoles(uiswapped);
        
        // TODO: Make UI bg rotate as well and also, make sure that we adapt to other values
        GameObject.Find("Rhythm").transform.localPosition = new Vector2(uiswapped ? 8.6f : 0.1f, GameObject.Find("Rhythm").transform.localPosition.y);
        
        RectTransform rt = GameObject.Find("CraftInventoryContainer").GetComponent<RectTransform>();
        rt.anchorMin = uiswapped ? new Vector2(0,0) : new Vector2(1,0);
        rt.anchorMax = uiswapped ? new Vector2(0,0) : new Vector2(1,0);
        rt.anchoredPosition = uiswapped ? new Vector3(159f, 206f, 0): new Vector3(-159f, 206f, 0);

        RectTransform rt2 = GameObject.Find("CraftAvailableRecipesContainer").GetComponent<RectTransform>();
        rt2.anchorMin = uiswapped ? new Vector2(0,0) : new Vector2(1,0);
        rt2.anchorMax = uiswapped ? new Vector2(0,0) : new Vector2(1,0);
        rt2.anchoredPosition = uiswapped ? new Vector3(400f, 198f, 0): new Vector3(-400f, 198f, 0);
        uiswapped = !uiswapped;
    }

    public void DamagePlayer()
    {
        Debug.Log("Took damage");
        // Get current heart
        GameObject health = GameObject.Find("Health");
        health.transform.GetChild(--this.playerHearts).GetComponent<Animator>().SetBool("full", false);
        if (this.playerHearts < 1) {
            SoundManager.instance.GameOver();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
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
            case 4:
                GameManager.instance.PlayTutorialStep();
                break;
            case 5:
                GameManager.instance.PlayTutorialStep();
                break;
            default:
                break;
        }
    }
}
