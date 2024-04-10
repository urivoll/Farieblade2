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
            nameText = "��� ����������";
            SType = "���������";
            description = $"���������� ����� �� ����� ����� ����� ���� � �������� ����� �������� ����.";
        }
    }
    public override IEnumerator AfterStep(Dictionary<string, int> inpData)
    {
        parentUnit.pathAnimation.TryGetAnimation("death");
        yield return new WaitForSeconds(0.5f);
        Turns.listUnitAll.Remove(parentUnit);
        if (parentUnit.Side == 0) Turns.listUnitLeft.Remove(parentUnit);
        else if (parentUnit.Side == 1) Turns.listUnitRight.Remove(parentUnit);
        yield return new WaitForSeconds(0.1f);
        Turns.finishEndEvent = true;
        Destroy(parentUnit.pathParent.gameObject);
    }
}
