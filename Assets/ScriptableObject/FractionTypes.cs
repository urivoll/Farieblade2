using UnityEngine;

[CreateAssetMenu(menuName = "SO/Fraction Types")]
public class FractionTypes : ScriptableObject
{
    public string[] Name => _name;
    public Sprite Icon => _icon;
    public int Id => _id;
    public Sprite CombatBg => _combatBg;
    public Sprite MenuBg => _menuBg;

    [SerializeField] private string[] _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _id;
    [SerializeField] private Sprite _combatBg;
    [SerializeField] private Sprite _menuBg;
}
