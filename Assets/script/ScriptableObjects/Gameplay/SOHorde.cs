using UnityEngine;

public abstract class SOHorde : ScriptableObject
{
    public float waitTimeBetween = 5.0f;
    public GameObject enemyToSpawn;

    [Range(1, 100)]
    public int amountOfEnemies = 1;
}
