using UnityEngine;

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
    public AudioClip LoadShellClip;
    public AudioClip PumpActionClip;
    public AudioClip hitClip;
    public AudioClip musicClip;
    public AudioClip gameOverMusicClip;
    public AudioClip victoryMusicClip;
    private void Awake()
    {
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
        PlayMusic(musicClip);
    }

    public void PlayMusic(AudioClip music)
    {
        if (music != null && MusicSource != null)
        {
            MusicSource.clip = music;
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

}
