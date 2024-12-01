using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityData : MonoBehaviour
{
    public int vidaMaxima = 100;
    public int vidaActual;
    public int superGaugeMax = 15;
    public int superGauge;

    public int points;

    [Header("Combat Properties")]
    public int iFramesDuration = 30;  // Duración de invulnerabilidad en frames
    public int staggerFrames = 20;    // Duración del stagger en frames
    public float knockbackForce = 5f; // Fuerza base del knockback
    public float knockbackUpForce = 2f; // Fuerza vertical del knockback

    private int currentIFrames = 0;   // Contador actual de iframes
    private int currentStagger = 0;   // Contador actual de stagger
    private bool isStaggered = false; // Estado de stagger
    private bool isInvulnerable = false; // Estado de invulnerabilidad

    private Animator animator;
    public SpriteRenderer renderSprite;
    private Rigidbody rb;

    void Start()
    {
        vidaActual = vidaMaxima;
        superGauge = 0;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Actualizar contadores
        if (currentIFrames > 0)
        {
            currentIFrames--;
            if (currentIFrames <= 0)
            {
                isInvulnerable = false;
                // Opcional: restaurar la opacidad normal del sprite
                Color c = renderSprite.color;
                c.a = 1f;
                renderSprite.color = c;
            }
            else
            {
                // Efecto visual de parpadeo durante iframes
                float alpha = Mathf.PingPong(Time.time * 10f, 1f);
                Color c = renderSprite.color;
                c.a = alpha;
                renderSprite.color = c;
            }
        }

        if (currentStagger > 0)
        {
            currentStagger--;
            if (currentStagger <= 0)
            {
                isStaggered = false;
                // Opcional: trigger de animación de recuperación
                if (animator != null)
                    animator.SetBool("Hurt", false);
            }
        }
    }

    public void reducirVida(int danio, Vector3 attackerPosition)
    {
        // Si está en iframes, ignorar el daño
        if (isInvulnerable) return;

        vidaActual -= danio;
        if (vidaActual < 0)
        {
            vidaActual = 0;
        }

        // Aplicar efectos de hit
        ApplyHitEffects(attackerPosition);
    }

    private void ApplyHitEffects(Vector3 attackerPosition)
    {
        // Activar iframes
        currentIFrames = iFramesDuration;
        isInvulnerable = true;

        // Activar stagger
        currentStagger = staggerFrames;
        isStaggered = true;
        if (animator != null)
            animator.SetBool("Hurt", true);

        // Aplicar knockback
        ApplyKnockback(attackerPosition);
    }

    private void ApplyKnockback(Vector3 attackerPosition)
    {
        if (rb != null)
        {
            // Calcular dirección del knockback
            Vector3 knockbackDirection = (transform.position - attackerPosition).normalized;
            // Aplicar fuerza
            rb.velocity = Vector3.zero; // Resetear velocidad actual
            rb.AddForce(
                new Vector3(
                    knockbackDirection.x * knockbackForce,
                    knockbackUpForce,
                    0
                ),
                ForceMode.Impulse
            );
        }
    }

    public void aumentarSuper(int super)
    {
        superGauge += super;
        if (superGauge > superGaugeMax)
        {
            superGauge = superGaugeMax;
        }
        if (superGauge < 0)
        {
            superGauge = 0;
        }
    }

    // Método para verificar si el personaje puede realizar acciones
    public bool CanAct()
    {
        return !isStaggered;
    }
}