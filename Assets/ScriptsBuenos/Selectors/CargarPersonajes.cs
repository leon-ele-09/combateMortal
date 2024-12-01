using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargarPersonajes : MonoBehaviour
{
    public GameObject Entity;  // The entity to which we will assign the animator controller
    public RuntimeAnimatorController GokuAnimatorController;  // Goku's Animator Controller
    public RuntimeAnimatorController ToadAnimatorController;  // Toad's Animator Controller
    public RuntimeAnimatorController WoodyAnimatorController; // Woody's Animator Controller

    public Sprite toadProjec;
    public Sprite gokuProjec;
    public Sprite woodyProjec;


    public bool Goku;
    public bool Toad;
    public bool Woody;

    private Animator entityAnimator;

    private SpriteRenderer spriteProjectile;

    // Reference to the Animator of the Entity

    // Start is called before the first frame update
    void Start()
    {
        // Get the Animator component from the entity (assuming Entity is not null)
        if (Entity != null)
        {
            entityAnimator = Entity.GetComponent<Animator>();
            spriteProjectile = Entity.GetComponent<NPCScript>().projectilePrefab.GetComponentInChildren<SpriteRenderer>();

        }

        // Load the saved preferences
        Goku = PlayerPrefs.GetInt("EnemyGoku", 0) == 1;
        Toad = PlayerPrefs.GetInt("EnemyToad", 1) == 1;
        Woody = PlayerPrefs.GetInt("EnemyWoody", 0) == 1;

        // Assign the correct animator controller based on the boolean flags
        if (entityAnimator != null)
        {
            if (Goku)
            {
                entityAnimator.runtimeAnimatorController = GokuAnimatorController;
                spriteProjectile.sprite = gokuProjec;
            }
            else if (Toad)
            {
                entityAnimator.runtimeAnimatorController = ToadAnimatorController;
                spriteProjectile.sprite = toadProjec;
            }
            else if (Woody)
            {
                entityAnimator.runtimeAnimatorController = WoodyAnimatorController;
                spriteProjectile.sprite = woodyProjec;
            }
        }
    }
}
