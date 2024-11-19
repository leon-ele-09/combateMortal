using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // Permite editarlo desde el Inspector
public class Personaje
{
    public string nombre; // Nombre del personaje
    public int salud;     // Salud del personaje
    public int dano;      // Da�o que puede hacer
    public int vida;      // N�mero de vidas
    public Sprite imagen; // Imagen del personaje
}