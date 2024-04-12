using UnityEngine;

[CreateAssetMenu(menuName = "SO/Elemental Types")]
public class ElementalTypes : ScriptableObject
{
    public string[] Name => _name;
    public Sprite Sprite => _sprite;
    public int Id => _id;

    [SerializeField] private string[] _name;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _id;
}
