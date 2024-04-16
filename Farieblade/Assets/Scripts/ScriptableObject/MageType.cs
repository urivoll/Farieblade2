using UnityEngine;

[CreateAssetMenu(menuName = "SO/Mage Type")]
public class MageType : ScriptableObject
{
    public string[] Name => _name;
    public int Id => _id;
    public Sprite Sprite => _sprite;

    [SerializeField] private string[] _name;
    [SerializeField] private int _id;
    [SerializeField] private Sprite _sprite;
}
