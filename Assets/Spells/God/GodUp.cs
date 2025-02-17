using System;
using UnityEngine;
public class GodUp : AbstractSpell
{
    private float Value;
    private float Value2;
    private int TempValue;
    [SerializeField] private GameObject Effect2;
    void Start()
    {
        Value = 0.2f + (fromUnit.grade * 0.01f);
        Value2 = 1.5f + (fromUnit.grade * 0.1f);
        if (transform.parent.gameObject.name == "Debuffs")
        {
            TempValue = Convert.ToInt32(parentUnit.Weapon.Damage * Value);
            //parentUnit.HpCharacter.damage -= TempValue;
            parentUnit.HpCharacter.HpDamage("dmg");
            for (int i = 0; i < 3; i += 2)
            {
                UnitProperties wolf = _characterPlacement.CirclesMap[parentUnit.ParentCircle.Side, i].ChildCharacter;
                if (wolf != null && wolf.Id == -2)
                {
                    //wolf.Weapon.damage += Convert.ToInt32(TempValue * Value2);
                    wolf.HpCharacter.HpDamage("dmg");
                    Instantiate(Effect2, wolf.PathBulletTarget.position, Quaternion.identity);
                    break;
                }
            }
            if (PlayerData.language == 0)
            {
                nameText = "Frost connection";
                SType = "Debuff";
                description = $"This character transferred part of his attack to the snow wolf.";
            }
            else
            {
                nameText = "�������� �����";
                SType = "���������";
                description = $"���� �������� ������� ����� ����� ����� �������� �����.";
            }
        }
        else if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Frost connection";
                SType = "Debuff";
                description = $"The monk's spirit takes {Convert.ToInt32(Value * 100)}% of the damage from the selected creature and gives it to his wolf, and the damage are multiplied by {Value}\r\nEnergy required: 2";
            }
            else
            {
                nameText = "�������� �����";
                SType = "���������";
                description = $"��� ������ �������� {Convert.ToInt32(Value * 100)}% ����� � ���������� �������� � ������ ������ �����, ���� ��� ���� ���������� �� {Value}\r\n����������� �������: 2";
            }
        }
    }
    public override void EndDebuff()
    {
        //parentUnit.Weapon.damage += TempValue;
        parentUnit.HpCharacter.HpDamage("dmg");
    }
}
