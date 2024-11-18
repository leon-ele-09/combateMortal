using UnityEngine;
using UnityEngine.UI;



public class ManejoDatos : MonoBehaviour
{
    public int vidaMaxima = 100;
    public int vidaActual;

    public float moveSpeed = 5f;

    public float groundCheckRadius = 0.2f;
    public float attackRadius = 0.2f;

    // Referencias a las barras de vida
    public Slider barraDeVida;
    //public Slider barraDeVidaOponente;

    // Referencia al texto del contador
    

    void Start()
    {
        // Inicializar vida
        vidaActual = vidaMaxima;
        barraDeVida.maxValue = vidaMaxima;
        //barraDeVidaOponente.maxValue = vidaMaxima;
        ActualizarBarrasDeVida();
    }

    void Update()
    {
        // Actualizar el contador de tiempo
        
    }

    public void ReducirHP(int cantidad)
    {
        vidaActual -= cantidad;
        if (vidaActual < 0) vidaActual = 0;
        ActualizarBarrasDeVida();
    }

    public void RestablecerHP()
    {
        vidaActual = vidaMaxima;
        ActualizarBarrasDeVida();
    }

    void ActualizarBarrasDeVida()
    {
        barraDeVida.value = vidaActual;
    }

    
}