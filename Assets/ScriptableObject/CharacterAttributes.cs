using UnityEngine;

[CreateAssetMenu(menuName = "SO/Character Attributes")]
public class CharacterAttributes : ScriptableObject
{
    public int Hp => _hp;
    public int Damage => _damage;
    public int Accuarcy => _accuarcy;
    public int Initiative => _initiative;
    public int HitCount => _hitCount;
    public int SpellCoast => _spellCoast;
    public int State => _state;
    public CharacterType Type => _type;
    public CharacterRole Role => _role;
    public GradeClasses Grade => _grade;
    public FractionTypes Fraction => _fraction;
    public ElementalTypes Vulnerability => _vulnerability;
    public ElementalTypes Resist => _resist;
    public ElementalTypes DamageType => _damageType;

    [SerializeField] private int _hp;
    [SerializeField] private int _damage;
    [SerializeField] private int _accuarcy;
    [SerializeField] private int _initiative;
    [SerializeField] private int _hitCount;
    [SerializeField] private int _spellCoast;
    [SerializeField] private int _state;
    [SerializeField] private CharacterType _type;
    [SerializeField] private CharacterRole _role;
    [SerializeField] private GradeClasses _grade;
    [SerializeField] private FractionTypes _fraction;
    [SerializeField] private ElementalTypes _vulnerability;
    [SerializeField] private ElementalTypes _resist;
    [SerializeField] private ElementalTypes _damageType;
}
