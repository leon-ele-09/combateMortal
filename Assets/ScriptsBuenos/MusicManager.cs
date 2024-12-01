using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip mainMenuMusic;
    public AudioClip sharedScenesMusic; // For the three sequential scenes
    public AudioClip combatMusic;
    public AudioClip winMusic;
    public AudioClip loseMusic;
    public AudioSource sfxSource;

    private static MusicManager instance;


    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Prevent duplicates
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySFX(AudioClip sfxClip)
    {
        sfxSource.PlayOneShot(sfxClip); // Play sound effect one time
    }

    public void PlayMusic(AudioClip clip)
    {
        if (audioSource.clip == clip) return; // Prevent restart if already playing
        audioSource.clip = clip;
        audioSource.Play();
    }
    public void CrossfadeMusic(AudioClip newClip, float fadeDuration)
    {
        StartCoroutine(FadeMusic(newClip, fadeDuration));
    }

    private IEnumerator FadeMusic(AudioClip newClip, float fadeDuration)
    {
        float startVolume = audioSource.volume;

        // Fade out
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.Stop();
        audioSource.clip = newClip;
        audioSource.Play();

        // Fade in
        while (audioSource.volume < startVolume)
        {
            audioSource.volume += startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }
    }


}

