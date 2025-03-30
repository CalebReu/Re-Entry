using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Sources")]

    public AudioSource sfxSource;
    public AudioSource MusicSource;

    [Header("Audio Clips")]

    public AudioClip playerShootClip;
    public AudioClip enemyShootClip;
    public AudioClip explosionClip;
    public AudioClip hitClip;
    public AudioClip musicClip;

    private void Awake()
    {
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

    public void PlayMusic()
    {
        if (musicClip != null && MusicSource != null)
        {
            MusicSource.clip = musicClip;
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
