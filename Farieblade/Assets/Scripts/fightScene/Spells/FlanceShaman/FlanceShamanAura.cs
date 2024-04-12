using System;
using UnityEngine;

public class FlanceShamanAura : AbstractSpell
{
    [HideInInspector] public float Value = 0.2f;
    void Start()
    {
        Value += (fromUnit.grade * 0.01f);
        if (PlayerData.language == 0)
        {
            nameText = "The power of the earth";
            SType = "Aura";
            description = $"Shaman Flance strengthens allies with the power of the earth, which increases their damage by {Convert.ToInt32(Value * 100)}%.";
        }
        else
        {
            nameText = "���� �����";
            SType = "����";
            description = $"����� ������� ������� ��������� ����� �����, ������� ����������� �� ���� �� {Convert.ToInt32(Value * 100)}%.";
        }
    }
}
