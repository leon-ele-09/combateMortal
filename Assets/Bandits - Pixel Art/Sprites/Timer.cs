using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public Text contadorTexto;
    private float tiempoRestante = 90f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (tiempoRestante > 0)
        {
            tiempoRestante -= Time.deltaTime;
            ActualizarContador();
        }

    }

    void ActualizarContador()
    {
        int minutos = Mathf.FloorToInt(tiempoRestante / 60);
        int segundos = Mathf.FloorToInt(tiempoRestante % 60);
        contadorTexto.text = string.Format("{0:00}:{1:00}", minutos, segundos);
    }

    public void ReiniciarTiempo()
    {
        tiempoRestante = 90f;
        ActualizarContador();
    }


}
