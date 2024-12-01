using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodigoVolumen : MonoBehaviour
{

    public Slider slider;
    public float sliderValue;
    public Image imageMute;
    public AudioSource audioSource;

    private void Start()
    {
        sliderValue = slider.value;
        slider.value = PlayerPrefs.GetFloat("VolumenAudio", 0.5f);
        audioSource.volume = slider.value;
        RevisarSiEstoyMute();
    }

    public void ChangeSlider(float Valor)
    {
        slider.value = Valor;
        sliderValue = slider.value;
        PlayerPrefs.SetFloat("VolumenAudio",sliderValue);
        audioSource.volume = sliderValue;
        RevisarSiEstoyMute();
    }

    public void RevisarSiEstoyMute()
    {
        if (sliderValue == 0)
        {
            imageMute.enabled = true;
        }
        else
        {
            imageMute.enabled = false;
        }
    }
}
