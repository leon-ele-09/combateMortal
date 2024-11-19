using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SeleccionPersonaje : MonoBehaviour
{
    [Header("Configuración de Personajes")]
    public Personaje[] personajes;         // Arreglo de personajes
    private Personaje personajeHeroe; // Personaje seleccionado como héroe
    private Personaje personajeVillano; // Personaje seleccionado como villano

    public Image[] imagenPersonaje;  // Imágenes de los personajes
    public Button[] botonesSeleccion; // Botones para seleccionar los personajes
    public Button botonJugar;        // Botón para jugar

    // Colores para resaltar la selección
    private Color colorNormal = Color.white;
    private Color colorSeleccionado = new Color(0.8f, 1f, 0.8f);  // Verde para héroe
    private Color colorEnemigoSeleccionado = new Color(1f, 0.8f, 0.8f); // Rojo para villano

    private void Start()
    {
        // Validar las referencias
        ValidarReferencias();

        // Inicializar la UI con la información de los personajes
        InicializarUI();

        // Desactivar el botón de jugar hasta que se seleccione un personaje
        if (botonJugar != null)
            botonJugar.interactable = false;
    }

    // Validación de referencias en el Inspector
    private void ValidarReferencias()
    {
        if (personajes == null || personajes.Length == 0)
        {
            Debug.LogError("No hay personajes configurados!");
            return;
        }

        if (imagenPersonaje.Length != personajes.Length || botonesSeleccion.Length != personajes.Length)
        {
            Debug.LogError("Las referencias UI no coinciden con el numero de personajes!");
        }
    }

    // Inicialización de la UI para mostrar los personajes
    private void InicializarUI()
    {
        for (int i = 0; i < personajes.Length; i++)
        {
            if (personajes[i] != null)
            {
                // No está seleccionado, mostrar información normal
                MostrarInformacionPersonaje(i, false, false);
            }
        }
    }

    // Muestra la informacion de un personaje
    private void MostrarInformacionPersonaje(int indice, bool esHeroe, bool esVillano)
    {
        if (indice < 0 || indice >= personajes.Length) return;

        // Imagen
        if (imagenPersonaje[indice] != null)
        {
            imagenPersonaje[indice].sprite = personajes[indice].imagen;

            // Usar el operador ternario para determinar el color
            imagenPersonaje[indice].color =
                esHeroe ? colorSeleccionado :
                esVillano ? colorEnemigoSeleccionado : colorNormal;
        }
    }

    // Método para seleccionar un personaje
    public void SeleccionarPersonaje(int indice)
    {
        // Verificar que el índice esté dentro del rango de los personajes
        if (indice < 0 || indice >= personajes.Length)
        {
            Debug.LogError("Índice fuera de rango.");
            return;
        }

        // Si el personaje seleccionado es el héroe, desmarcarlo y seleccionar el nuevo héroe
        if (personajeHeroe == personajes[indice])
        {
            personajeHeroe = null;
            MostrarInformacionPersonaje(indice, false, false);  // Desmarcar como héroe
            Debug.Log($"Héroe desmarcado: {personajes[indice].nombre}");
        }
        // Si el personaje seleccionado es el villano, desmarcarlo y seleccionar el nuevo villano
        else if (personajeVillano == personajes[indice])
        {
            personajeVillano = null;
            MostrarInformacionPersonaje(indice, false, false);  // Desmarcar como villano
            Debug.Log($"Villano desmarcado: {personajes[indice].nombre}");
        }
        else
        {
            // Si el héroe no está seleccionado, seleccionamos al héroe
            if (personajeHeroe == null)
            {
                personajeHeroe = personajes[indice];
                MostrarInformacionPersonaje(indice, true, false);  // Resaltar como héroe
                Debug.Log($"Héroe seleccionado: {personajeHeroe.nombre}");
            }
            // Si el héroe ya está seleccionado y el villano no lo está, seleccionamos al villano
            else if (personajeVillano == null)
            {
                // No se puede seleccionar el mismo personaje para héroe y villano
                if (personajeHeroe == personajes[indice])
                {
                    Debug.LogError("No puedes seleccionar el mismo personaje como héroe y villano.");
                    return;
                }
                personajeVillano = personajes[indice];
                MostrarInformacionPersonaje(indice, false, true);  // Resaltar como villano
                Debug.Log($"Villano seleccionado: {personajeVillano.nombre}");
            }
        }

        // Activar el botón de jugar si ambos personajes están seleccionados
        if (personajeHeroe != null && personajeVillano != null && botonJugar != null)
        {
            botonJugar.interactable = true;
        }
    }

    // Método para resetear la selección de héroe y villano
    public void ResetearSeleccion()
    {
        // Restablecer los roles de los personajes
        personajeHeroe = null;
        personajeVillano = null;

        // Inicializar la UI (restaurar colores)
        InicializarUI();

        // Desactivar el botón de jugar hasta que se seleccione un héroe y un villano
        if (botonJugar != null)
            botonJugar.interactable = false;
    }

    // Método para jugar y cargar la escena
    public void Jugar()
    {
        if (personajeHeroe == null || personajeVillano == null)
        {
            Debug.LogWarning("¡No se ha seleccionado un héroe o un villano!");
            return;
        }

        try
        {
            // Guardar la información del héroe y villano
            PlayerPrefs.SetString("PersonajeNombre", personajeHeroe.nombre);
            PlayerPrefs.SetInt("PersonajeSalud", personajeHeroe.salud);
            PlayerPrefs.SetInt("PersonajeDano", personajeHeroe.dano);

            PlayerPrefs.SetString("EnemigoNombre", personajeVillano.nombre);
            PlayerPrefs.SetInt("EnemigoSalud", personajeVillano.salud);
            PlayerPrefs.SetInt("EnemigoDano", personajeVillano.dano);

            PlayerPrefs.Save();

            // Cambiar a la escena de juego
            SceneManager.LoadScene("Nivel1");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error al guardar o cargar la escena: {e.Message}");
        }
    }
}
