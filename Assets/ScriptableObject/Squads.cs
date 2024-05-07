using UnityEngine;

[CreateAssetMenu(menuName = "SO/Squads")]
public class Squads : ScriptableObject
{
    public StringArray[] Squad => _squad;

    [SerializeField] private StringArray[] _squad;
}
