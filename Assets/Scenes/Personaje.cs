using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // Permite editarlo desde el Inspector
public class Personaje
{
    public string nombre; // Nombre del personaje
    public int salud;     // Salud del personaje
    public int dano;      // Daño que puede hacer
    public int vida;      // Número de vidas
    public Sprite imagen; // Imagen del personaje
}