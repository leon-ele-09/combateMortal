using System;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Components
    public float moveSpeed = 12f;
    private Rigidbody rb;
    private Animator animator;
    public SpriteRenderer renderSprite;
    private BoxCollider boxCollider;
    public float inputDelay = 1.5f;
    public bool isGrounded;
    
    private float lastActionTime;

    /*
     aniadir colision de golpe, reducir vida, aumentar carga, aniadir muerte usando las funciones de ManejoDatos
     - IA para el NPC, se acerca y se pone a golpear, cuando esta en rango tiene un X chance de hacer un dodge
     - Meter colision entre jugador y NPC para que no se traspasen
     - Meter knockback con los ataques
     
     */
    public Transform attackPoint;
    public float attackRadius = 0.2f;

    public LayerMask groundLayer;
    public LayerMask playerLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    // Datos
    public EntityData datos;
    // Super ataques

    public GameObject projectilePrefab;  // 3D projectile prefab
    public float projectileSpeed = 10f;
    private LayerMask enemyLayer;


    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
        isGrounded = true;
        enemyLayer = playerLayer;
        lastActionTime = Time.time;

        // Para evitar que se rote el rigidbody
        rb.constraints = RigidbodyConstraints.FreezeRotation |
                        RigidbodyConstraints.FreezePositionZ;


        rb.useGravity = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        
    }

    void Update()
    {


        CheckGrounded();


        //HandleMovement();


        if (Time.time - lastActionTime >= inputDelay && isGrounded)
        {
            if (HandleCombat() == 1)
            {

                lastActionTime = Time.time;

            }
        }
        HandleVisuals();




    }
    void OnDrawGizmos()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }



    private void HandleMovement()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        // Aceleración
        Vector3 newVelocity = new Vector3(
            moveInput * moveSpeed,
            rb.velocity.y,
            0f
        );


        rb.velocity = newVelocity;
    }

    private int HandleCombat()
    {

        if (!datos.CanAct()) return 0;

        // Ataque ligero
        if (Input.GetKeyDown(KeyCode.J))
        {
            int backwards;
            if (renderSprite.flipX) { backwards = 1; }
            else { backwards = -1; }
            animator.SetBool("Attack", true);
            rb.AddForce(new Vector3(backwards * 5f, 1.5f, 0f), ForceMode.Impulse);
            Collider[] hits = Physics.OverlapSphere(attackPoint.position, attackRadius, playerLayer);

            foreach (Collider hit in hits)
            {
                if (hit != null)
                {
                    EntityData targetData = hit.GetComponent<EntityData>();
                    if (targetData != null)
                    {
                        targetData.reducirVida(2, transform.position);
                        datos.aumentarSuper(1);
                    }
                }
            }
            return 1;
        }

        // Ataque pesado
        if (Input.GetKeyDown(KeyCode.K))
        {
            animator.SetBool("Attack", true);
            Collider[] hits = Physics.OverlapSphere(attackPoint.position, attackRadius, playerLayer);

            foreach (Collider hit in hits)
            {
                if (hit != null)
                {
                    EntityData targetData = hit.GetComponent<EntityData>();
                    if (targetData != null)
                    {
                        targetData.reducirVida(5, transform.position);
                        datos.aumentarSuper(2);
                    }
                }
            }
            return 1;
        }


        // Bloqueo
        if (Input.GetKeyDown(KeyCode.L))
        {
            animator.SetBool("Hurt", true);

            int backwards = renderSprite.flipX ? -1 : 1;

            // Apply a force for knockback
            rb.AddForce(new Vector3(backwards * 6f, 4.5f, 0f), ForceMode.Impulse);

            // Ensure the grounded state is updated correctly
            isGrounded = false;

            return 1;
        }

        // Special

        if (Input.GetKeyDown(KeyCode.I))
        {
            // Determine the direction the player is facing (forward in the Z-axis)
            if (datos.superGauge >= 3)
            {
                // Instantiate the projectile at the attackPoint
                GameObject projectile = Instantiate(projectilePrefab, attackPoint.position, Quaternion.identity);
                Vector3 direction = new Vector3(2f, 0f, 0f);
                // Apply a force to the projectile to move it in the right direction
                Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
                projectileRb.velocity = direction * projectileSpeed;  // Moving the projectile along the Z-axis

                // Set the projectile's destroy-on-collision behavior
                ProjectileBehavior projectileScript = projectile.GetComponent<ProjectileBehavior>();
                if (projectileScript != null)
                {
                    projectileScript.enemyLayer = enemyLayer;  // Set the enemy layer to check for collisions
                }
                datos.aumentarSuper(-3);
                return 1;
            }
        }





        return 0;

    } 

    private void HandleVisuals()
    {
        
        
        
        animator.SetInteger("AnimState", 0); // Idle state when not moving
        

        animator.SetBool("Grounded", isGrounded);
    }

    private void CheckGrounded()
    {
        // Use 3D physics instead of 2D
        isGrounded = Physics.CheckSphere(
            groundCheck.position,
            groundCheckRadius,
            groundLayer,
            QueryTriggerInteraction.Ignore
        );


    }

    
}