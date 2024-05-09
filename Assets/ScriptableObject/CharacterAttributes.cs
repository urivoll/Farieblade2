using Spine;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Character Attributes")]
public class CharacterAttributes : ScriptableObject
{
    public float Hp => _hp;
    public float Damage => _damage;
    public int Accuracy => _accuracy;
    public int Initiative => _initiative;
    public int HitCount => _hitCount;
    public int SpellCoast => _spellCoast;
    public int Squad => _squad;
    public CharacterType Type => _type;
    public CharacterRole Role => _role;
    public GradeClasses Rang => _rang;
    public FractionTypes Fraction => _fraction;
    public ElementalTypes Vulnerability => _vulnerability;
    public ElementalTypes Resist => _resist;
    public ElementalTypes DamageType => _damageType;

    [SerializeField] private float _hp;
    [SerializeField] private float _damage;
    [SerializeField] private int _accuracy;
    [SerializeField] private int _initiative;
    [SerializeField] private int _hitCount;
    [SerializeField] private int _spellCoast;
    [SerializeField] private int _squad;
    [SerializeField] private CharacterType _type;
    [SerializeField] private CharacterRole _role;
    [SerializeField] private GradeClasses _rang;
    [SerializeField] private FractionTypes _fraction;
    [SerializeField] private ElementalTypes _vulnerability;
    [SerializeField] private ElementalTypes _resist;
    [SerializeField] private ElementalTypes _damageType;
    public int GetDamage(int level, int grade)
    {
        float tempDamage = _damage;
        float damage;
        tempDamage += _damage * (0.1f * level);
        damage = tempDamage + (tempDamage * (0.4f * grade));
        return (int)damage;
    }
    public int GetHp(int level, int grade)
    {
        float tempHp = _hp;
        float hp;
        tempHp += _hp * (0.2f * level);
        hp = tempHp + (tempHp * (0.4f * grade));
        return (int)hp;
    }
}
