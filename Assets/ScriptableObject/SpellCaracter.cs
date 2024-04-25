using UnityEngine;

[CreateAssetMenu(menuName = "SO/Spell Caracter")]
public class SpellCaracter : ScriptableObject
{
    [SerializeField] private string[] _name;
    [SerializeField] private string[] _description;
    [SerializeField] private Sprite _icon;
}
