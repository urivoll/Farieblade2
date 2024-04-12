using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class Weapon : MonoBehaviour
{
    protected float _timeBeforeHit;
    protected AudioClip _soundBeforeHit;
    protected CharacterPlacement _characterPlacement;
    protected float _behiendTimes;
    [Inject]
    private void Construct(CharacterPlacement characterPlacement)
    {
        _characterPlacement = characterPlacement;
    }
    public virtual IEnumerator Attack(UnitProperties from, List<MakeMove> inpData) { yield return null; }
}
