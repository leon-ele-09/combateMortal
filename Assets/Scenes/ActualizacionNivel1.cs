using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActualizacionNivel1 : MonoBehaviour
{
    private Personaje personajeHeroe;
    private Personaje personajeVillano;

    void Start()
    {
        // Cargar los datos del h�roe y villano seleccionados
        CargarDatos();

        // Aqu� puedes inicializar el juego con los personajes cargados
        if (personajeHeroe != null && personajeVillano != null)
        {
            // L�gica del juego con los personajes seleccionados
            Debug.Log($"H�roe: {personajeHeroe.nombre} | Villano: {personajeVillano.nombre}");
            // Iniciar el combate, colocar los personajes en el escenario, etc.
        }
        else
        {
            Debug.LogError("No se pudieron cargar los datos del h�roe o villano.");
        }
    }

    // M�todo para cargar los datos de los personajes desde PlayerPrefs
    private void CargarDatos()
    {
        if (PlayerPrefs.HasKey("PersonajeNombre") && PlayerPrefs.HasKey("EnemigoNombre"))
        {
            personajeHeroe = new Personaje
            {
                nombre = PlayerPrefs.GetString("PersonajeNombre"),
                salud = PlayerPrefs.GetInt("PersonajeSalud"),
                dano = PlayerPrefs.GetInt("PersonajeDano")
            };

            personajeVillano = new Personaje
            {
                nombre = PlayerPrefs.GetString("EnemigoNombre"),
                salud = PlayerPrefs.GetInt("EnemigoSalud"),
                dano = PlayerPrefs.GetInt("EnemigoDano")
            };
        }
        else
        {
            Debug.LogError("No se han guardado los datos del h�roe y villano.");
        }
    }
}