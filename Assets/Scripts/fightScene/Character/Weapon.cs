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

    protected int _damageType;
    protected float _timeBeforeHit;
    protected AudioClip _soundBeforeHit;
    protected CharacterPlacement _characterPlacement;
    protected float _behiendTimes;
    protected int _damage;
    protected int _accuracy;
    protected int _state;
    public int _times;
    private UnitProperties _unitProperties;

    public void Instantiate()
    {
        /*        accuracy = pathParent.accuracy;
                damage = pathParent.damage;
                state = pathParent.state;
                times = pathParent.times;*/
        _unitProperties = GetComponent<UnitProperties>();
    }
    public void AttackedUnit(List<MakeMove> attack)
    {
        _unitProperties.Animation.TryGetAnimation("attack");
        StartCoroutine(Attack(_unitProperties, attack));
    }

    [Inject]
    private void Construct(CharacterPlacement characterPlacement)
    {
        _characterPlacement = characterPlacement;
    }
    public virtual IEnumerator Attack(UnitProperties from, List<MakeMove> inpData) { yield return null; }
}
