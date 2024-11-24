using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    void Start()
    {
        // No instanciar inmediatamente, esperar la selección
        if (PlayerStorage.playerPrefab != null)
        {
            GameObject player = Instantiate(PlayerStorage.playerPrefab,
                                         transform.position,
                                         transform.rotation);
        }
    }
}
