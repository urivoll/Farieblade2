using UnityEngine;

[CreateAssetMenu(menuName = "SO/Elemental Types")]
public class ElementalTypes : ScriptableObject
{
    [SerializeField] private string[] _name;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private int _id;
}
