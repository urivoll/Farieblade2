using System;
public class MoonBless : AbstractSpell
{
    public int Value = 5;
    private int TempValue;
    void Start()
    {
        Value += fromUnit.grade;
        if (transform.parent.gameObject.name == "Debuffs")
        {
            TempValue = Value;
            parentUnit.accuracy += TempValue;
            if (parentUnit.accuracy > 100)
            {
                TempValue = parentUnit.accuracy + Value - 100;
                parentUnit.accuracy = 100;
            }
            if (PlayerData.language == 0)
            {
                nameText = "Moonlight";
                SType = "Buff";
                description = $"This character is enhanced by moonlight, the effect gives an additional {Convert.ToInt32(Value)} to accuracy.";
            }
            else
            {
                nameText = "������ ����";
                SType = "����������� ����������";
                description = $"���� �������� ������ ������ ������, ������ ���� �������������� {Convert.ToInt32(Value)} ��. � ��������.";
            }
        }
        else if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Moonlight";
                SType = "Buff";
                description = $"Moonlight gives an ally an additional {Convert.ToInt32(Value)} to accuracy.\r\nEnergy required: 2\nDuration: 3";
            }
            else
            {
                nameText = "������ ����";
                SType = "����������� ����������";
                description = $"������ ���� ���� �������� �������������� {Convert.ToInt32(Value)} ��. � ��������.\r\n����������� �������: 2\n������������: 3";
            }
        }
    }
    public override void EndDebuff()
    {
        if(parentUnit != null) parentUnit.accuracy -= TempValue;
    }
}
