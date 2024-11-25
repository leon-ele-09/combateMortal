using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStorage
{
    public static GameObject playerPrefab { get; set; }
    public static int selectedPlayerIndex { get; set; }
    public static Vector3 spawnPosition = new Vector3(-5f, 0f, 0f); // Posición por defecto
}

