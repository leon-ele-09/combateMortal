using UnityEngine;

[System.Serializable]
public class CharacterStats : MonoBehaviour
{
    public string nombrePersonaje;
    public int vidaMaxima;
    public int superGaugeMax;
    public int iFramesDuration;
    public int staggerFrames;
    public float knockbackForce;
    public float moveSpeed;
    public float projectileSpeed;
    public int danioAtaqueLigero;
    public int danioAtaquePesado;
    public int danioAtaqueEspecial;
    public int costoSuper;
    public float attackRadius = 2f;
    public float inputDelay = 1.5f;
}