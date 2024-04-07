using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Aura : MonoBehaviour
{
    public UnitProperties parentUnit;
    public virtual IEnumerator GetAura(Dictionary<string, int> inpData)
    {
        yield return null;
    }
}
