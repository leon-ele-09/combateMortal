using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playSFX : MonoBehaviour
{


    public void OnButtonClick(AudioClip SFX)
    {
        MusicManager manager = FindObjectOfType<MusicManager>();
        manager.PlaySFX(SFX);
    }
}