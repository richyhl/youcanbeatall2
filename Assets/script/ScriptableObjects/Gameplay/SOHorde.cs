using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SOHorde : ScriptableObject
{
    public float waitTimeBetween = 5.0f;
    public GameObject enemyToSpawn;

    [Range(1, 100)]
    public int amountOfEnemies = 1;

    public abstract IEnumerator RunHorde();

    internal Transform FindWithTag(Transform root, string tag)
    {
        foreach (Transform t in root.GetComponentsInChildren<Transform>())
        {
            if (t.CompareTag(tag)) return t;
        }
        return null;
    }

    internal Transform[] FindArrayWithTag(Transform root, string tag)
    {
        List<Transform> items = new List<Transform>();
        foreach (Transform t in root.GetComponentsInChildren<Transform>())
        {
            if (t.CompareTag(tag))
                items.Add(t);
        }
        return items.ToArray();
    }
}
