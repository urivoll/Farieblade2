using UnityEngine;

[CreateAssetMenu(menuName = "SO/Fraction Types")]
public class FractionTypes : ScriptableObject
{
    [SerializeField] private string[] _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _id;
    [SerializeField] private Sprite _combatBg;
    [SerializeField] private Sprite _menuBg;
}
