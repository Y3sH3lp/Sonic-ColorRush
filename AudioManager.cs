using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Make AudioManager persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Plays a sound effect.
    /// </summary>
    public void PlaySFX(AudioClip clip, float volume = 1.0f)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip, volume);
        }
    }

    /// <summary>
    /// Plays background music.
    /// </summary>
    public void PlayMusic(AudioClip clip, float volume = 1.0f)
    {
        if (clip != null)
        {
            musicSource.clip = clip;
            musicSource.volume = volume;
            musicSource.Play();
        }
    }

}
