using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehavior : MonoBehaviour
{

    // Components
    private Rigidbody2D rb;
    private Animator animator;
    public SpriteRenderer renderSprite;
    private BoxCollider2D boxCollider;
    public ManejoDatos datos;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if(datos.vidaActual == 0)
        {
            animator.SetBool("Death", true);
        }
        else
        {
            animator.SetBool("Death", false);
        }




    }

    public void TakeDamage() {
        datos.ReducirHP(10);

        Debug.Log("Ouch! ");
        
        animator.SetBool("Hurt", true);
        
    
    }
    

}
