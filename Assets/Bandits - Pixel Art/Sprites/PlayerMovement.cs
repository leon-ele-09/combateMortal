using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movement variables
    public float jumpForce = 15f;
    public float moveSpeed = 5f;          
    // Ground check variables
    public Transform groundCheck;
    public Transform attackPoint;
    public float groundCheckRadius = 0.2f;
    public float attackRadius = 0.2f;
    public LayerMask groundLayer;
    public LayerMask playerLayer;
    public bool isGrounded;

    // Components
    private Rigidbody2D rb;
    private Animator animator;
    public SpriteRenderer renderSprite;
    private BoxCollider2D boxCollider;

    // Variable para la pose
    public Poses poses;
    public float inputDelay = 1.5f; // Retraso entre entradas
    private float lastActionTime; // Tiempo de la última acción
    private bool waitForAction;

    // Start is called before the first frame update
    void Start()
    {
        waitForAction = true;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        isGrounded = true;
        lastActionTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        // Calcular la posición inferior del sprite
        
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        animator.SetBool("Grounded", isGrounded);

        // Manejar la entrada de la pose
        if (Time.time - lastActionTime >= inputDelay)
        {

            if(HandlePose(poses.pose) == 1)
            lastActionTime = Time.time; // Actualizar el tiempo de la última acción
        }

        // Horizontal movement (A/D keys)
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Update animator based on movement
        if (Input.GetKey(KeyCode.D))
        {
            renderSprite.flipX = true;
            animator.SetInteger("AnimState", 2);
        }
        // Walking animation if moving

        else if (Input.GetKey(KeyCode.A))
        {
            renderSprite.flipX = false;
            animator.SetInteger("AnimState", 2);
        }

        else
        {
            animator.SetInteger("AnimState", 0);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {

            int backwards;

            if (renderSprite.flipX) { backwards = 1; }
            else { backwards = -1; }

            

            animator.SetBool("Attack", true);
            rb.position = new Vector2(rb.position.x + backwards * 1.5f, rb.position.y);
            Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, attackRadius, playerLayer);
            if (hit != null)
            {
                // Si se detecta un jugador, llama a su método para hacer la animación de golpe
                hit.GetComponent<NPCBehavior>().TakeDamage(); // Asegúrate de tener este método en el script del jugador golpeado
            }

        }

        if (Input.GetKeyDown(KeyCode.U))
        {

            
            animator.SetBool("Attack", true);
            Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, attackRadius, playerLayer);
            if (hit != null)
            {
                // Si se detecta un jugador, llama a su método para hacer la animación de golpe
                hit.GetComponent<NPCBehavior>().TakeDamage(); // Asegúrate de tener este método en el script del jugador golpeado
            }


        }

        if (Input.GetKeyDown(KeyCode.K) && isGrounded)
        {

            animator.SetBool("Hurt", true);
            int backwards;
            if (renderSprite.flipX) { backwards = -1; }
            else { backwards = 1; }

            rb.velocity = new Vector2(backwards * (moveSpeed + 5), 5);
            rb.position = new Vector2(rb.position.x + backwards * 1.5f, rb.position.y);
            isGrounded = false;

        }


    }

    private int HandlePose(int pose)
    {
        //print("HandlePose");
        switch (pose)
        {
            case -1:
                // No hay acción
                animator.SetBool("Attack", false);
                waitForAction = true;
                return 0;
                
            case 0:
                // Golpe ligero
                if (waitForAction)
                {

                    animator.SetBool("Attack", true);
                    Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, attackRadius, playerLayer);
                    if (hit != null)
                    {
                        // Si se detecta un jugador, llama a su método para hacer la animación de golpe
                        hit.GetComponent<NPCBehavior>().TakeDamage(); // Asegúrate de tener este método en el script del jugador golpeado
                    }
                    waitForAction = false;
                }
                break;
            case 1:
                // Golpe fuerte
                if (waitForAction)
                {
                    int backwards;

                    if (renderSprite.flipX) { backwards = 1; }
                    else { backwards = -1; }



                    animator.SetBool("Attack", true);
                    rb.position = new Vector2(rb.position.x + backwards * 1.5f, rb.position.y);
                    Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, attackRadius, playerLayer);
                    if (hit != null)
                    {
                        // Si se detecta un jugador, llama a su método para hacer la animación de golpe
                        hit.GetComponent<NPCBehavior>().TakeDamage(); // Asegúrate de tener este método en el script del jugador golpeado
                    }
                    waitForAction = false;
                }
                break;
            case 2:
                // Empuje
                if (isGrounded && waitForAction)
                {
                    animator.SetBool("Hurt", true);
                    int backwards;
                    if (renderSprite.flipX) { backwards = -1; }
                    else { backwards = 1; }

                    rb.velocity = new Vector2(backwards * (moveSpeed + 5), 5);
                    rb.position = new Vector2(rb.position.x + backwards * 1.5f, rb.position.y);
                    isGrounded = false;
                }
                break;


        }
        return 1;

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
