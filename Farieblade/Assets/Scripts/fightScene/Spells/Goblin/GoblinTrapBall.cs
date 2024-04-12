using System;
using UnityEngine;
public class GoblinTrapBall : AbstractSpell
{
    public int value = 10;
    public float vaule2 = 0.15f;
    public float withProsent;
    [SerializeField] private GameObject debuff;
    void Start()
    {
        value += fromUnit.grade;
        vaule2 += fromUnit.grade * 0.01f;
        withProsent = prosentDamage * fromUnit.damage;
        if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Goblin Trap";
                SType = "Debuff";
                description = $"The goblin throws his trap at the enemy, thereby dealing {Convert.ToInt32(withProsent)} damage and also reducing his characteristics.\r\nEnergy required: 2\r\nDuration: 2\r\nDamage: -{Convert.ToInt32(vaule2 * 100)}%\r\nInitiative: -{value}";
            }
            else
            {
                nameText = "���������� ������";
                SType = "���������";
                description = $"������ ������� ���� ������ � ����������, ��� ����� ������� {Convert.ToInt32(withProsent)} ��. �����, � ����� ������� ��� ��������������.\r\n����������� �������: 2\r\n������������: 2\r\n����: -{Convert.ToInt32(vaule2 * 100)}%\r\n����������: -{value}";
            }
        }
    }
}
