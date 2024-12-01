using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FuncionesMenu : MonoBehaviour
{
    public float fadeDuration = 1f; // Duration of the fade
    private Canvas fadeCanvas;
    private Image fadeImage;

    void Start()
    {
        // Create the Canvas
        fadeCanvas = new GameObject("FadeCanvas").AddComponent<Canvas>();
        fadeCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        fadeCanvas.sortingOrder = 1000;  // Make sure it's on top of other UI elements

        // Create the Image for fade effect
        fadeImage = new GameObject("FadeImage").AddComponent<Image>();
        fadeImage.transform.SetParent(fadeCanvas.transform);
        fadeImage.rectTransform.anchorMin = new Vector2(0, 0);
        fadeImage.rectTransform.anchorMax = new Vector2(1, 1);
        fadeImage.rectTransform.offsetMin = Vector2.zero;
        fadeImage.rectTransform.offsetMax = Vector2.zero;
        fadeImage.color = new Color(0, 0, 0, 0); // Set initial transparency

        // Set the fade image to cover the entire screen
        fadeImage.rectTransform.localPosition = Vector3.zero;
    }

    public void PlayGame(string NombreNivel)
    {
        StartCoroutine(FadeAndLoadScene(NombreNivel));
    }

    public void Salir()
    {
        Application.Quit();
        Debug.Log("Aqui se cierra el juego");
    }

    private IEnumerator FadeAndLoadScene(string sceneName)
    {
        // Fade to black
        yield return StartCoroutine(Fade(1f));

        // Load the new scene
        SceneManager.LoadScene(sceneName);

        // Wait for the new scene to load
        yield return new WaitForSeconds(0.5f);

        // Fade back to transparent
        yield return StartCoroutine(Fade(0f));
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float currentAlpha = fadeImage.color.a;
        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            float newAlpha = Mathf.Lerp(currentAlpha, targetAlpha, timeElapsed / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, newAlpha);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure the final alpha is exactly the target
        fadeImage.color = new Color(0, 0, 0, targetAlpha);
    }
}
