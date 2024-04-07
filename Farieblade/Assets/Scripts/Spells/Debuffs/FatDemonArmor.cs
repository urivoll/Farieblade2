using System;
using System.Xml.Linq;
public class FatDemonArmor : AbstractSpell
{
    public float Value = 0.1f;
    void Start()
    {
        Value += fromUnit.grade * 0.01f;
        if (transform.parent.gameObject.name == "Debuffs")
        {
            onGame = true;
            duration = 2;
            startNumberTurn = Turns.numberTurn + duration;
            parentUnit.resistance += Value;
        }
        if (PlayerData.language == 0)
        {
            nameText = "Stone Skin";
            SType = "Debuff";
            description = $"The selected creature gains {Convert.ToInt32(Value)}% protection from all types of attacks.\r\nEnergy required: 3\r\nDuration: 2";
        }
        else
        {
            nameText = "�������� ����";
            SType = "���������";
            description = $"��������� �������� �������� ������ �� ����� ����� ���� �� {Convert.ToInt32(Value)}%.\r\n����������� �������: 3\r\n������������: 2";
        }
    }
    public override void EndDebuff()
    {
        parentUnit.resistance -= Value;
    }
}
