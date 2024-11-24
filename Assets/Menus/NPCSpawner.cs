using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    void Start()
    {
        if (EnemyStorage.enemyPrefab != null)
        {
            GameObject npc = Instantiate(EnemyStorage.enemyPrefab,
                                      transform.position,
                                      transform.rotation);
        }
    }
}
