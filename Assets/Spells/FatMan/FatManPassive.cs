using System;

public class FatManPassive : AbstractSpell
{
    public float Value;

    void Start()
    {
        Value = 0.2f + (fromUnit.grade * 0.01f);
        if (PlayerData.language == 0)
        {
            nameText = "Dungeon Unity";
            SType = "Passive";
            description = $"Fat Man receives a damage bonus for each ally from his faction.\r\nDamage: +{Convert.ToInt32(Value * 100)}%";
        }
        else
        {
            nameText = "�������� ����������";
            SType = "���������";
            description = $"������� �������� ����� � ����� �� ������� �������� �� ��� �������.\r\n����: +{Convert.ToInt32(Value * 100)}%";
        }
    }
}
