using UnityEngine;

[CreateAssetMenu(menuName = "SO/Character Spells")]
public class CharacterSpell : ScriptableObject
{
    public string[] Name => _name;
    public MageType Type => _type;
    public string[] Description => _description;
    public Sprite Sprite => _sprite;

    [SerializeField] private string[] _name;
    [SerializeField] private MageType _type;
    [SerializeField] private string[] _description;
    [SerializeField] private Sprite _sprite;
}
