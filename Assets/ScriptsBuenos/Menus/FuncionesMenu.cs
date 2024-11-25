using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FuncionesMenu : MonoBehaviour
{
    public void PlayGame(string NombreNivel)
    {
        SceneManager.LoadScene(NombreNivel);


    }
    

    public void Salir()
    {
        Application.Quit();
        Debug.Log("Aqui se cierra el juego");
    }


}
