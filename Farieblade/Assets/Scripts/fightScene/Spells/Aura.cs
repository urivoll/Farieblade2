using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class Aura : MonoBehaviour
{
    public UnitProperties parentUnit;
    protected CharacterPlacement _characterPlacement;

    [Inject]
    private void Construct(CharacterPlacement characterPlacement)
    {
        _characterPlacement = characterPlacement;
    }

    public virtual IEnumerator GetAura(Dictionary<string, int> inpData)
    {
        yield return null;
    }
}
