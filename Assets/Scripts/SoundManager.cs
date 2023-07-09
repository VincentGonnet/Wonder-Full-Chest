using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }

    [SerializeField] private AudioSource mainMenuTrack;
    [SerializeField] private AudioSource gameMenuTrack;

    private AudioSource currentTrack;

    private float volume;
    private float volumeSpeed;
    
    private void Awake()
    {
        if (instance == null) {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else {
            Destroy(this.gameObject);
        }
    }

    public void FixedUpdate()
    {
        if (currentTrack is null) {
            return;
        }
        if (gameMenuTrack.time < 5) {
            volume = Mathf.SmoothDamp(volume, 1, ref volumeSpeed, 5);
            currentTrack.volume = volume;
        }
        if (gameMenuTrack.time > gameMenuTrack.clip.length - 5) {
            volume = Mathf.SmoothDamp(volume, 0, ref volumeSpeed, 5);
            currentTrack.volume = volume;
        }
    }

    public void StartMainMenuMusic()
    {
        gameMenuTrack.Stop();
        mainMenuTrack.Play();
        currentTrack = mainMenuTrack;
    }

    public void StartGameMusic()
    {
        mainMenuTrack.Stop();
        gameMenuTrack.Play();
        currentTrack = gameMenuTrack;
    }
}
