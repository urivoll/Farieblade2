using UnityEngine;

[CreateAssetMenu(menuName = "SO/Fraction Types")]
public class FractionTypes : ScriptableObject
{
    public AudioClip Environment => _environment;
    public string[] Name => _name;
    public string[] Description => _desctiption;
    public Sprite Icon => _icon;
    public int Id => _id;
    public Sprite CombatBg => _combatBg;
    public Sprite MenuBg => _menuBg;

    [SerializeField] private AudioClip _environment;
    [SerializeField] private string[] _name;
    [SerializeField] private string[] _desctiption;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _id;
    [SerializeField] private Sprite _combatBg;
    [SerializeField] private Sprite _menuBg;

}
