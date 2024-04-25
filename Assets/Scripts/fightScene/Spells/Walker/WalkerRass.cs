using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WalkerRass : AbstractSpell
{
    public GameObject Effect2;
    void Start()
    {
        if (PlayerData.language == 0)
        {
            nameText = "Diffusion";
            SType = "Debuff";
            description = $"Walker removes positive effects from the enemy target.\r\nEnergy required: 2";
        }
        else
        {
            nameText = "�����������";
            SType = "���������";
            description = $"������ ������� ������������� ������� � ��������� ����.\r\n����������� �������: 2";
        }
    }
    public override IEnumerator HitEffect(Dictionary<string, int> inpData)
    {
        UnitProperties targetUnit = _characterPlacement.CirclesMap[inpData["side"], inpData["place"]].ChildCharacter;
        yield return new WaitForSeconds(timeBeforeShoot);
        for (int i = 0; i < targetUnit.DebuffList.Count; i++)
        {
            if (targetUnit.DebuffList[i].GetComponent<AbstractSpell>().Type == "Buff")
                Destroy(targetUnit.DebuffList[i]);
        }
        Instantiate(Effect2, targetUnit.PathBulletTarget.position, Quaternion.identity);
        yield return new WaitForSeconds(0.4f);
        Turns.hitDone = true;
    }
}
