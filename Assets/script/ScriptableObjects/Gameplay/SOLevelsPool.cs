using UnityEngine;

[CreateAssetMenu(fileName = "SOLevelsPool_", menuName = "ScriptableObjects/Levels Pool", order = 6)]
public class SOLevelsPool : ScriptableObject
{
    public SOLevel[] levels;

    // Indexer declaration
    public SOLevel this[int index]
    {
        get => levels[index];
    }
}
