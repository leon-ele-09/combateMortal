using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SeleccionMapa : MonoBehaviour
{
    public void PlayGame(string NombreNivel)
    {

        if (PlayerPrefs.GetInt("Mapa1", 0) == 1) {

            SceneManager.LoadScene("EscenaBuena");

        }

        if (PlayerPrefs.GetInt("Mapa2", 0) == 1)
        {

            SceneManager.LoadScene("EscenaBuena1");

        }


    }
    

    public void Salir()
    {
        Application.Quit();
        Debug.Log("Aqui se cierra el juego");
    }


}
