using UnityEngine;

public static class EnemyStorage
{
    public static GameObject enemyPrefab { get; set; }
    public static int selectedEnemyIndex { get; set; }
    public static Vector3 spawnPosition = new Vector3(5f, 0f, 0f);
}