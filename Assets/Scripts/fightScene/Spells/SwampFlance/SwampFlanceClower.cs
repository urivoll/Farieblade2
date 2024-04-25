using System;
using UnityEngine;

public class SwampFlanceClower : AbstractSpell
{
    [HideInInspector] public float Value = 0.2f;
    void Start()
    {
        Value += (fromUnit.grade * 0.01f);
        if (PlayerData.language == 0)
        {
            nameText = "Mountain clover";
            SType = "Aura";
            description = $"Swamp Flance distributes the mountain clover to his allies before the battle. Flances in your squad gain {Convert.ToInt32(Value * 100)}% extra health.";
        }
        else
        {
            nameText = "������ ������";
            SType = "����";
            description = $"�������� ����� ����� ���� ������� ����� ��������� ������ ������. ������ � ����� ������ �������� �������������� �������� �� {Convert.ToInt32(Value * 100)}%.";
        }
    }
}
