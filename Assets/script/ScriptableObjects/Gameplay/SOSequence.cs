using UnityEngine;

[CreateAssetMenu(fileName = "SOSequence_", menuName = "ScriptableObjects/Level Sequence", order = 3)]
public class SOSequence : ScriptableObject
{
    public float waitTimeBetweenHordes = 5.0f;
    public SOHorde[] hordes;

    public SOHorde this[int index]
    {
        get => hordes[index];
    }
}
