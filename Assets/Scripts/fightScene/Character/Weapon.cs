using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class Weapon : MonoBehaviour
{
    public int DamageType => _damageType;
    public int Damage => _damage; 
    public int Times => _times;
    public int State => _state;
    public int Accuracy => _accuracy;
    public int DamageDefault => _damageDefault;
    public int AccuracyDefault => _accuracyDefault;

    protected int _damageType;
    protected float _timeBeforeHit;
    protected AudioClip _soundBeforeHit;
    [Inject] protected CharacterPlacement _characterPlacement;
    protected float _behiendTimes;
    protected int _damage;
    protected int _accuracy;
    protected int _state;
    protected int _times;

    protected int _damageDefault;
    protected int _accuracyDefault;

    private UnitProperties _unitProperties;

    public void Instantiate(int accuracy, int damage, int state, int times)
    {
        _accuracy = accuracy;
        _accuracyDefault = accuracy;
        _damage = damage;
        _damageDefault = damage;
        _state = state;
        _times = times;
        _unitProperties = GetComponent<UnitProperties>();
    }

    public void AttackedUnit(List<MakeMove> attack)
    {
        _unitProperties.Animation.TryGetAnimation("attack");
        StartCoroutine(Attack(_unitProperties, attack));
    }

    public virtual IEnumerator Attack(UnitProperties from, List<MakeMove> inpData) { yield return null; }
}
