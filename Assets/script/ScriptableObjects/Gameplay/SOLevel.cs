using UnityEngine;

[CreateAssetMenu(fileName = "SOLevel_", menuName = "ScriptableObjects/Level", order = 2)]
public class SOLevel : ScriptableObject
{
    public SOSequence[] sequences;

    public SOSequence this[int index]
    {
        get => sequences[index];
    }
}
