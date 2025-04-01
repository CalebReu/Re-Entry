using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Sources")]

    public AudioSource sfxSource;
    public AudioSource MusicSource;

    [Header("Audio Clips")]

    public AudioClip playerShootClip;
    public AudioClip playerTripleShotClip;
    public AudioClip playerShotgunClip;
    public AudioClip enemyShootClip;
    public AudioClip explosionClip;
    public AudioClip hitClip;
    public AudioClip musicClip;
    public AudioClip gameOverMusicClip;
    public AudioClip victoryMusicClip;
    public AudioClip buttonClickClip;
    private void Awake()
    {
        SetButtonSounds(); // sets the button sounds for all buttons in the scene.
        DontDestroyOnLoad(gameObject); // this makes sure the object is not destroyed when loading a new scene.
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        PlayMusic();
    }
    public void SetButtonSounds()
    {
        Button[] buttons = FindObjectsByType<Button>(FindObjectsSortMode.None);

        foreach (var b in buttons)
        {
            UnityAction l = delegate { OnClick(); };
            b.onClick.AddListener(l);
        }
    }

    public void PlayMusic()
    {
        int sceneNumber = SceneHandler.Instance.getScene(); // gets the current scene
        Debug.Log("music for scene " + sceneNumber + " is playing.");
        if (musicClip != null && MusicSource != null)
        {
            switch (sceneNumber)
            {
                case 5: MusicSource.clip = (gameOverMusicClip != null) ? gameOverMusicClip : musicClip; break; //sets the music to game over
                case 6: MusicSource.clip = (victoryMusicClip != null) ? victoryMusicClip : musicClip; break; // sets the music to victory!
                default: MusicSource.clip = musicClip; break; // just uses the normal music
            }
            MusicSource.loop = true;
            MusicSource.Play();
        }
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void OnClick()
    {
        PlaySound(buttonClickClip);
    }
}
