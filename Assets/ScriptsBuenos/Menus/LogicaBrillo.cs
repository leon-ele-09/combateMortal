using UnityEngine;
using UnityEngine.UI;

public class LogicaBrillo : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Image panelBrillo;
    private float sliderValue;

    void Awake()
    {
        // Verificar que tenemos todas las referencias necesarias
        if (slider == null)
        {
            Debug.LogError("Error: Slider no está asignado en LogicaBrillo!");
            enabled = false;
            return;
        }

        if (panelBrillo == null)
        {
            Debug.LogError("Error: Panel de brillo no está asignado en LogicaBrillo!");
            enabled = false;
            return;
        }
    }

    void Start()
    {
        // Cargar el valor guardado del brillo
        sliderValue = PlayerPrefs.GetFloat("Brillo", 0.5f);

        // Asignar el valor al slider
        if (slider != null)
        {
            slider.value = sliderValue;
        }

        // Actualizar el panel con el valor inicial
        ActualizarBrillo(sliderValue);
    }

    public void ChangeSlider(float valor)
    {
        if (panelBrillo == null) return;

        sliderValue = valor;
        PlayerPrefs.SetFloat("Brillo", sliderValue);
        PlayerPrefs.Save();

        ActualizarBrillo(sliderValue);
    }

    private void ActualizarBrillo(float valor)
    {
        if (panelBrillo == null) return;

        Color nuevoColor = panelBrillo.color;
        nuevoColor.a = valor;
        panelBrillo.color = nuevoColor;
    }
}