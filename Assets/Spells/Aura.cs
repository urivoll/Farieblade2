using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Aura : MonoBehaviour
{
    public UnitProperties parentUnit;
    [Inject] protected CharacterPlacement _characterPlacement;

    public virtual IEnumerator GetAura(Dictionary<string, int> inpData)
    {
        yield return null;
    }
}
