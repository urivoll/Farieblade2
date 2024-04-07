using System;
using System.Collections.Generic;
public class RockProtect : AbstractSpell
{
    public float Value = 0.2f;
    void Start()
    {
        Value += (fromUnit.grade * 0.01f);
        if (transform.parent.gameObject.name == "Debuffs")
        {
            duration = 3;
            Turns.punch += BoostDamage;
            startNumberTurn = Turns.numberTurn + duration;
            if (PlayerData.language == 0)
            {
                nameText = "Stone protection";
                SType = "Buff";
                description = $"The power of the stones protects the character. Physical damage protection increased by {Convert.ToInt32(Value * 100)}%";
            }
            else
            {
                nameText = "�������� ������";
                SType = "����������� ����������";
                description = $"���� ������ �������� ���������. ������ �� ����������� ����� ��������� �� {Convert.ToInt32(Value * 100)}%";
            }
        }
        else if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Stone protection";
                SType = "Buff";
                description = $"The revived stone bestows its power on an ally, thereby increasing his defense against physical damage.\r\nEnergy required: 2\r\nDuration: 3\r\nDefense: +{Convert.ToInt32(Value * 100)}%";
            }
            else
            {
                nameText = "�������� ������";
                SType = "����������� ����������";
                description = $"������� ������ ������ ���� ���� ���� ��������, ��� ����� ������� ��� ������ �� ����������� �����.\r\n����������� �������: 2\r\n������������: 3\r\n������: +{Convert.ToInt32(Value * 100)}%";
            }
        }
    }
    private void BoostDamage(UnitProperties victim, UnitProperties who, List<Dictionary<string, int>> inpData)
    {
        if (victim == parentUnit && who.pathParent.damageType == 4)
        {
            parentUnit.inpDamage -= parentUnit.inpDamage * Value;
        }
    }
    public override void EndDebuff()
    {
        Turns.punch -= BoostDamage;
    }
}
