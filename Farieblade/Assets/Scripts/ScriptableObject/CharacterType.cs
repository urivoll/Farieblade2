using UnityEngine;

[CreateAssetMenu(menuName = "SO/Character Type")]
public class CharacterType : ScriptableObject
{
    [SerializeField] private string[] _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _id;
}
