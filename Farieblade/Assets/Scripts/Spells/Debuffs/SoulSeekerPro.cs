using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoulSeekerPro : AbstractSpell
{
    private int value;
    void Start()
    {
        value = 10 + fromUnit.grade;
        if (PlayerData.language == 0)
        {
            nameText = "Hypnosis";
            SType = "Debuff";
            description = $"The Soul Seeker hypnotizes the target. The creature loses some damage and, during its turn, hits (if possible) the enemy with the highest health. After his turn, the curse is removed.\r\nEnergy required: 1\r\nDamage: -{value}%";
        }
        else
        {
            nameText = "������";
            SType = "���������";
            description = $"�������� ��� ������������� ����. �������� ������ ����� ����� �, �� ����� ������ ����, ���� (���� ��� ��������) ����� � ����� ������� ����������� ��������. ����� ��� ���� ��������� ���������.\r\n����������� �������: 2\r\n����: -{value}%";
        }
    }
    public override IEnumerator HitEffect(Dictionary<string, int> inpData)
    {
        UnitProperties targetUnit = Turns.circlesMap[inpData["side"], inpData["place"]].newObject;
        yield return new WaitForSeconds(timeBeforeShoot);
        Instantiate(Effect, targetUnit.pathBulletTarget.position, Quaternion.identity);
        yield return new WaitForSeconds(0.4f);
        Turns.hitDone = true;
    }
}
