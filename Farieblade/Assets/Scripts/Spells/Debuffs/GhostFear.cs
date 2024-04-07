using System.Collections.Generic;
using UnityEngine;
public class GhostFear : AbstractSpell
{
    public int Value = 50;
    void Start()
    {
        Value += fromUnit.grade;
        if (transform.parent.gameObject.name == "Debuffs")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Fear";
                SType = "Debuff";
                description = $"This character is terrified, there is a chance that he will miss the next turn {Value}%";
            }
            else
            {
                nameText = "�����";
                SType = "���������";
                description = $"���� �������� � �����, ���� ��� �� ��������� ��������� ��� {Value}%";
            }
        }
        else if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Fear";
                SType = "Debuff";
                description = $"The ghost scares its victim, thereby giving the enemy a chance to miss the next move.\r\nEnergy required: 1\r\nDuration: 2\r\nChance: {Value}";
            }
            else
            {
                nameText = "�����";
                SType = "���������";
                description = $"������� ������ ���� ������, ��� ����� ��������� ����� ���� ���������� ��������� ���.\r\n����������� �������: 1\r\n������������: 2\r\n����: {Value}%";
            }
        }
    }
    public override void PeriodicMethod(Dictionary<string, int> inpData)
    {
        parentUnit.paralize = true;
        Instantiate(Effect, parentUnit.pathBulletTarget.position, Quaternion.identity);
        parentUnit.pathAnimation.SetCaracterState("hit");
    }
}
