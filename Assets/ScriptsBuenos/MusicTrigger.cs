using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    public AudioClip sceneMusic;
    public float fadeDuration = 1.0f;

    void Start()
    {
        MusicManager manager = FindObjectOfType<MusicManager>();
        if (manager != null)
        {
            manager.CrossfadeMusic(sceneMusic, fadeDuration);
        }
    }
}