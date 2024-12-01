using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SpriteAnimation
{
    public string name;
    public Sprite[] frames;
    public float frameRate = 12f;
    public bool loop = true;
    public bool flipX = false;
    public bool flipY = false;
}

public class CustomAnimator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private List<SpriteAnimation> animations = new List<SpriteAnimation>();

    private SpriteAnimation currentAnimation;
    private float frameTimer;
    private int currentFrameIndex;
    private bool isPlaying = false;

    // Cache para optimización
    private Dictionary<string, SpriteAnimation> animationCache = new Dictionary<string, SpriteAnimation>();

    private void Awake()
    {
        // Obtener el SpriteRenderer si no está asignado
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Inicializar el cache de animaciones
        foreach (var anim in animations)
        {
            if (!animationCache.ContainsKey(anim.name))
            {
                animationCache.Add(anim.name, anim);
            }
        }
    }

    public void PlayAnimation(string animationName, bool resetIfSame = false)
    {
        // Si la animación ya está reproduciéndose y no queremos resetearla
        if (currentAnimation != null &&
            currentAnimation.name == animationName &&
            isPlaying &&
            !resetIfSame)
        {
            return;
        }

        // Buscar la animación en el cache
        if (animationCache.TryGetValue(animationName, out SpriteAnimation newAnimation))
        {
            currentAnimation = newAnimation;
            currentFrameIndex = 0;
            frameTimer = 0f;
            isPlaying = true;

            // Aplicar configuración de flip
            spriteRenderer.flipX = currentAnimation.flipX;
            spriteRenderer.flipY = currentAnimation.flipY;

            // Establecer el primer frame
            if (currentAnimation.frames.Length > 0)
            {
                spriteRenderer.sprite = currentAnimation.frames[0];
            }
        }
        else
        {
            Debug.LogWarning($"Animation '{animationName}' not found!");
        }
    }

    public void StopAnimation()
    {
        isPlaying = false;
        currentAnimation = null;
        currentFrameIndex = 0;
        frameTimer = 0f;
    }

    public void PauseAnimation()
    {
        isPlaying = false;
    }

    public void ResumeAnimation()
    {
        if (currentAnimation != null)
        {
            isPlaying = true;
        }
    }

    private void Update()
    {
        if (!isPlaying || currentAnimation == null || currentAnimation.frames.Length == 0)
            return;

        frameTimer += Time.deltaTime;
        float frameInterval = 1f / currentAnimation.frameRate;

        while (frameTimer >= frameInterval)
        {
            frameTimer -= frameInterval;
            AdvanceFrame();
        }
    }

    private void AdvanceFrame()
    {
        currentFrameIndex++;

        if (currentFrameIndex >= currentAnimation.frames.Length)
        {
            if (currentAnimation.loop)
            {
                currentFrameIndex = 0;
            }
            else
            {
                currentFrameIndex = currentAnimation.frames.Length - 1;
                isPlaying = false;
                OnAnimationComplete();
                return;
            }
        }

        spriteRenderer.sprite = currentAnimation.frames[currentFrameIndex];
    }

    // Evento que se dispara cuando termina una animación
    private void OnAnimationComplete()
    {
        // Aquí puedes agregar lógica adicional cuando termine una animación
        Debug.Log($"Animation {currentAnimation.name} completed");
    }

    // Método para añadir animaciones en runtime
    public void AddAnimation(string name, Sprite[] frames, float frameRate = 12f, bool loop = true)
    {
        var newAnimation = new SpriteAnimation
        {
            name = name,
            frames = frames,
            frameRate = frameRate,
            loop = loop
        };

        animations.Add(newAnimation);

        if (!animationCache.ContainsKey(name))
        {
            animationCache.Add(name, newAnimation);
        }
    }

    // Método para obtener el nombre de la animación actual
    public string GetCurrentAnimationName()
    {
        return currentAnimation?.name;
    }

    // Método para verificar si una animación está reproduciéndose
    public bool IsPlaying(string animationName = null)
    {
        if (animationName == null)
            return isPlaying;

        return isPlaying && currentAnimation?.name == animationName;
    }

    // Método para modificar el frame rate en runtime
    public void SetFrameRate(float newFrameRate)
    {
        if (currentAnimation != null)
        {
            currentAnimation.frameRate = newFrameRate;
        }
    }

    // Método para voltear el sprite
    public void SetFlip(bool flipX, bool flipY)
    {
        if (currentAnimation != null)
        {
            currentAnimation.flipX = flipX;
            currentAnimation.flipY = flipY;
            spriteRenderer.flipX = flipX;
            spriteRenderer.flipY = flipY;
        }
    }
}