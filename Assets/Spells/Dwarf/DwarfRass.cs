using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DwarfRass : AbstractSpell
{
    void Start()
    {
        if (PlayerData.language == 0)
        {
            nameText = "Medicine of Dispelling";
            SType = "Buff";
            description = $"The dwarf alchemist gives his ally a dispel potion, thereby removing all negative effects from him.\r\nEnergy required: 1";
        }
        else
        {
            nameText = "�������� �����������";
            SType = "����������� ����������";
            description = $"���� ������� ���� ������ �������� �������� �����������, ��� ����� � ���� ��������� ��� ������������� �������.\r\n����������� �������: 1";
        }
    }
    public override IEnumerator HitEffect(Dictionary<string, int> inpData)
    {
        UnitProperties targetUnit = _characterPlacement.CirclesMap[inpData["side"], inpData["place"]].ChildCharacter;
        yield return new WaitForSeconds(timeBeforeShoot);
        for (int i = 0; i < targetUnit.DebuffList.Count; i++)
        {
            if (targetUnit.DebuffList[i].GetComponent<AbstractSpell>().Type == "Debuff")
                Destroy(targetUnit.DebuffList[i]);
        }
        yield return new WaitForSeconds(1f);
        Turns.hitDone = true;
    }
}
