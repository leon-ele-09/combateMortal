using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CombatManager : MonoBehaviour
{
    public float roundTime = 100f;  // Total round time in seconds
    private float timer;

    public EntityData player1;     // Reference to player 1's data (includes current health)
    public EntityData player2;     // Reference to player 2's data (includes current health)

    public Transform p1_t;
    public Transform p2_t;

    public TextMeshProUGUI timerText;  // Text for showing the timer
    public TextMeshProUGUI pointText;  // Text for showing the timer


    public Slider player1HealthSlider;  // Slider for player 1's health
    public Slider player2HealthSlider;  // Slider for player 2's health

    public Slider superP1;
    public Slider superP2;

    private float player1CurrentSuper;
    private float player2CurrentSuper;
    private float player1CurrentHealth;  // To track the smoothed health for player 1
    private float player2CurrentHealth;  // To track the smoothed health for player 2
    private float healthChangeSpeed = 5f;  // Controls the speed of health bar transition

    public Image s1_p1;
    public Image s2_p1;
    public Image s1_p2;
    public Image s2_p2;

    private int player1Stocks = 0;
    private int player2Stocks = 0;

    private Vector3 initial_p1;
    private Vector3 initial_p2;

    public GameObject fadePanel;

    public float bounceDuration = 0.5f;  // Duration of the bounce effect
    public float bounceScale = 1.2f;  // How much bigger the text will scale
    public float bounceSpeed = 0.5f; // Speed factor for scaling

    private Vector3 originalScale; // To store the original scale for resetting

    string current;

    void Start()
    {
        timer = roundTime;
        pointText.text = player1.points.ToString();
        originalScale = pointText.transform.localScale;
        current = pointText.text;

        // Initialize the sliders to the players' max health
        player1HealthSlider.maxValue = player1.vidaMaxima;
        player2HealthSlider.maxValue = player2.vidaMaxima;

        superP1.maxValue = player1.superGaugeMax;
        superP2.maxValue = player2.superGaugeMax;

        // Initialize smoothed health values
        player1CurrentHealth = player1.vidaActual;
        player2CurrentHealth = player2.vidaActual;

        player1CurrentSuper = player1.superGauge;
        player2CurrentSuper = player2.superGauge;

        initial_p1 = p1_t.position;
        initial_p2 = p2_t.position;

        fadePanel.GetComponent<Image>().color = new Color(0, 0, 0, 0);
    }

    void FixedUpdate()
    {
        HandleCombatTimers();
        CheckHealthStatus();
        UpdateTimerDisplay();
        UpdateHealthSliders();  // Update health sliders smoothly
        UpdatePointDisplay();
    }

    private void UpdateTimerDisplay()
    {
        // Update the timer text, rounded up to the nearest whole number
        timerText.text = Mathf.Ceil(timer).ToString();
    }

    private void UpdatePointDisplay()
    {
        // Update the text with the current points
        

        if(current != player1.points.ToString()) {

            current = pointText.text;

            pointText.text = player1.points.ToString();

            // Start the bounce effect when the points are updated
            StartCoroutine(BounceEffect());

        }
        
    }


    private IEnumerator BounceEffect()
    {
        // Ensure the text starts with its original scale (in case of too many points)
        pointText.transform.localScale = originalScale;

        // Scale up the text a bit for the bounce effect
        Vector3 targetScale = originalScale * bounceScale;
        float timeElapsed = 0f;

        // Scale up quickly using a smooth function (faster)
        while (timeElapsed < bounceDuration)
        {
            float t = timeElapsed / bounceDuration;
            pointText.transform.localScale = Vector3.Lerp(originalScale, targetScale, Mathf.SmoothStep(0f, 1f, t * bounceSpeed));
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure the text is fully scaled up at the end of the scaling up phase
        pointText.transform.localScale = targetScale;

        // Now, scale back down to the original size more slowly
        timeElapsed = 0f;
        while (timeElapsed < bounceDuration)
        {
            float t = timeElapsed / bounceDuration;
            pointText.transform.localScale = Vector3.Lerp(targetScale, originalScale, Mathf.SmoothStep(0f, 1f, t * bounceSpeed));
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Finally, ensure it's back to the original scale
        pointText.transform.localScale = originalScale;
    }



    private void UpdateHealthSliders()
    {
        // Smoothly interpolate current health to the player's actual health using Lerp
        player1CurrentHealth = Mathf.Lerp(player1CurrentHealth, player1.vidaActual, Time.deltaTime * healthChangeSpeed);
        player2CurrentHealth = Mathf.Lerp(player2CurrentHealth, player2.vidaActual, Time.deltaTime * healthChangeSpeed);

        player1CurrentSuper = Mathf.Lerp(player1CurrentSuper, player1.superGauge, Time.deltaTime * healthChangeSpeed);

        player2CurrentSuper = Mathf.Lerp(player2CurrentSuper, player2.superGauge, Time.deltaTime * healthChangeSpeed);


        // Update the sliders' values to reflect the smoothed health
        player1HealthSlider.value = player1CurrentHealth;
        player2HealthSlider.value = player2CurrentHealth;
        superP1.value = player1CurrentSuper;
        superP2.value = player2CurrentSuper;

    }

    private void HandleCombatTimers()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;  // Decrease timer each frame
        }
        else
        {
            EndCombat(0);  // Time is up, end combat
        }
    }

    private void CheckHealthStatus()
    {
        // Check if player 1 or player 2 has lost all health
        if (player1.vidaActual <= 0)
        {
            EndCombat(2);  // Player 2 wins
        }
        else if (player2.vidaActual <= 0)
        {
            EndCombat(1);  // Player 1 wins
        }
    }

    private void EndCombat(int winner)
    {
        // Handle the end of the combat, where 'winner' indicates the winning player
        if (winner == 1 || (winner == 0 && player2.vidaActual <= player1.vidaActual))
        {
            // Check if player 1 has 0 stocks
            if (player1Stocks == 0)
            {
                s1_p1.color = Color.yellow;  // Highlight Player 1's first stock
                player1Stocks++;  // Increment Player 1's stock count
            }
            else
            {
                s2_p1.color = Color.yellow;  // Highlight Player 1's second stock
                StartCoroutine(endOfCombat(1));  // Start the endOfCombat coroutine
                return;  // Exit early, preventing the reset from happening immediately
            }
        }
        else if (winner == 2 || (winner == 0 && player2.vidaActual > player1.vidaActual))
        {
            // Check if player 2 has 0 stocks
            if (player2Stocks == 0)
            {
                s1_p2.color = Color.yellow;  // Highlight Player 2's first stock
                player2Stocks++;  // Increment Player 2's stock count
            }
            else
            {
                s2_p2.color = Color.yellow;  // Highlight Player 2's second stock
                StartCoroutine(endOfCombat(2));  // Start the endOfCombat coroutine
                return;  // Exit early, preventing the reset from happening immediately
            }
        }

        // Start the fade and reset round (only after endOfCombat completes)
        StartCoroutine(FadeToBlackAndResetRound());
    }


    private IEnumerator FadeToBlackAndResetRound()
    {
        // Step 1: Pause the game by setting time scale to 0
        Time.timeScale = 0;

        // Step 2: Fade to black (using the fadePanel)
        Image fadeImage = fadePanel.GetComponent<Image>();
        float fadeDuration = 2f;  // Duration of the fade in seconds
        float timeElapsed = 0f;

        // Fade to black (increase alpha to 1)
        while (timeElapsed < fadeDuration)
        {
            fadeImage.color = new Color(0, 0, 0, Mathf.Lerp(0f, 1f, timeElapsed / fadeDuration));
            timeElapsed += Time.unscaledDeltaTime;  // Use unscaledDeltaTime for the fade
            yield return null;
        }
        fadeImage.color = new Color(0, 0, 0, 1);  // Ensure it is fully black

        // Step 3: While the screen is black, run the ResetRound function
        resetRound();

        // Step 4: Fade back from black (decrease alpha to 0)
        timeElapsed = 0f;
        while (timeElapsed < fadeDuration)
        {
            fadeImage.color = new Color(0, 0, 0, Mathf.Lerp(1f, 0f, timeElapsed / fadeDuration));
            timeElapsed += Time.unscaledDeltaTime;  // Use unscaledDeltaTime for the fade
            yield return null;
        }
        fadeImage.color = new Color(0, 0, 0, 0);  // Ensure it's fully transparent again

        // After fading out, continue the game normally (reset the round timer, etc.)
        Time.timeScale = 1;
    }


    private IEnumerator endOfCombat(int winner)
    {
        // Load the "MenuPrincipal" scene, which will automatically unload the current scene

        PlayerPrefs.SetInt("Player1Points", player1.points);

        if (winner == 1)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Winner");

        }

        if (winner == 2)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Loser");

        }
        // Optionally, you can yield here if you need some delay before switching
        // (for example, you may want a short pause or transition animation before loading the next scene)
        yield return null;  // Empty yield, can be replaced with a delay if needed
    }

    public void endOfCombatGUI(int winner)
    {
        // Load the "MenuPrincipal" scene, which will automatically unload the current scene

        PlayerPrefs.SetInt("Player1Points", player1.points);

        if (winner == 1)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Winner");

        }

        if (winner == 2)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Loser");

        }
        
    }




    private void resetRound()
    {
        // Reset health and super gauges for both players
        player1.vidaActual = player1.vidaMaxima;
        player2.vidaActual = player2.vidaMaxima;
        player1.superGauge = 0;
        player2.superGauge = 0;

        // Reset player positions
        p1_t.position = initial_p1;
        p2_t.position = initial_p2;

        // Reset the round timer
        timer = roundTime;
    }
}