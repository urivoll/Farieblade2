using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class Weapon : MonoBehaviour
{
    public int damage => _damage; 
    public int times => _times;
    public int state => _state;
    public int accuracy => _accuracy;

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
