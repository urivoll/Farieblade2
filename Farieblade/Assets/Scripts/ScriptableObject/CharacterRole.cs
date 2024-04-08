using UnityEngine;

[CreateAssetMenu(menuName = "SO/Character Role")]
public class CharacterRole : ScriptableObject
{
    [SerializeField] private string[] _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _id;
}
