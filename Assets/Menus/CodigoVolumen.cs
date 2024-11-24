using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodigoVolumen : MonoBehaviour
{

    public Slider slider;
    public float sliderValue;
    public Image imageMute;

    private void Start()
    {
        slider.value = PlayerPrefs.GetFloat("VolumenAudio", 0.5f);
        AudioListener.volume = slider.value;
        RevisarSiEstoyMute();
    }

    public void ChangeSlider(float Valor)
    {
        slider.value = Valor;
        PlayerPrefs.SetFloat("VolumenAudio",sliderValue);
        AudioListener.volume = sliderValue;
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
