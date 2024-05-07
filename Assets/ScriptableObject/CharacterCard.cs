using UnityEngine;
[CreateAssetMenu(menuName = "SO/Character Card")]

public class CharacterCard : ScriptableObject
{
    [SerializeField] private StringArray[] _name;
    [SerializeField] private StringArray[] _description;
    //[SerializeField] private StringArray[] _;
}
