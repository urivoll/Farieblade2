using UnityEngine;

[CreateAssetMenu(menuName = "SO/ID Caracter")]
public class IDCaracter : ScriptableObject
{
    public CharacterData[] CharacterData => _characterData;

    [SerializeField] private CharacterData[] _characterData;
}
