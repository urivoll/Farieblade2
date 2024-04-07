using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SummonPassive : AbstractSpell
{
    void Start()
    {
        if (PlayerData.language == 0)
        {
            nameText = "Conscript Spirit";
            SType = "Passive";
            description = $"The summoned card moves on its own during battle and disappears when left alone.";
        }
        else
        {
            nameText = "Дух призывника";
            SType = "Пассивная";
            description = $"Призванная карта во время битвы ходит сама и исчезает когда остается одна.";
        }
    }
    public override IEnumerator AfterStep(Dictionary<string, int> inpData)
    {
        parentUnit.pathAnimation.SetCaracterState("death");
        yield return new WaitForSeconds(0.5f);
        Turns.listUnitAll.Remove(parentUnit);
        if (parentUnit.sideOnMap == 0) Turns.listUnitLeft.Remove(parentUnit);
        else if (parentUnit.sideOnMap == 1) Turns.listUnitRight.Remove(parentUnit);
        yield return new WaitForSeconds(0.1f);
        Turns.finishEndEvent = true;
        Destroy(parentUnit.pathParent.gameObject);
    }
}
