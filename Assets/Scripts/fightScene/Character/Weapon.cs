using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class Weapon : MonoBehaviour
{
    public int DamageType => _damageType;
    public int Damage => _damage; 
    public int Accuracy => _accuracy;
    public int Times => _times;
    public int Type => _type;
    public int DamageDefault => _damageDefault;
    public int AccuracyDefault => _accuracyDefault;

    public float TimeBeforeHit => _timeBeforeHit;
    public float BehiendTimes => _behiendTimes;

    protected int _damageType;
    protected int _damage;
    protected int _accuracy;
    protected int _type;
    protected int _times;
    protected int _damageDefault;
    protected int _accuracyDefault;

    protected float _timeBeforeHit;
    protected float _behiendTimes;

    protected AudioClip _soundBeforeHit;

    [Inject] protected CharacterPlacement _characterPlacement;

    private UnitProperties _unitProperties;

    public void Init(UnitProperties unitProperties, CharacterAttributes attributes)
    {
        _accuracy = attributes.Accuracy;
        _accuracyDefault = attributes.Accuracy;
        _damage = (int)attributes.Damage;
        _damageDefault = (int)attributes.Damage;
        _type = attributes.Type.Id;
        _times = attributes.HitCount;
        _unitProperties = unitProperties;
    }

    public void AttackedUnit(List<MakeMove> attack)
    {
        _unitProperties.Animation.TryGetAnimation("attack");
        StartCoroutine(Attack(_unitProperties, attack));
    }

    public virtual IEnumerator Attack(UnitProperties from, List<MakeMove> inpData) { yield return null; }
}
