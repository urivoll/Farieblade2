using UnityEngine;

[CreateAssetMenu(menuName = "SO/Elemental Types")]
public class ElementalTypes : ScriptableObject
{
    public string[] Name => _name;
    public Sprite Icon => _icon;
    public int Id => _id;

    [SerializeField] private string[] _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _id;
}
