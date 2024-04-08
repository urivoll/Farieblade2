using UnityEngine;

[CreateAssetMenu(menuName = "SO/Character Attributes")]
public class CharacterAttributes : ScriptableObject
{
    [SerializeField] private int _hp;
    [SerializeField] private int _damage;
    [SerializeField] private int _accuarcy;
    [SerializeField] private int _initiative;
    [SerializeField] private int _hitCount;
    [SerializeField] private int _spellCoast;
    [SerializeField] private CharacterType _type;
    [SerializeField] private CharacterRole _role;
    [SerializeField] private GradeClasses _grade;
    [SerializeField] private FractionTypes _fraction;
    [SerializeField] private ElementalTypes _vulnerability;
    [SerializeField] private ElementalTypes _resist;
    [SerializeField] private ElementalTypes _damageType;
    //[SerializeField] private 
}
