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
            TempValue = Convert.ToInt32(parentUnit.damage * Value);
            parentUnit.damage -= TempValue;
            parentUnit.HpDamage("dmg");
            for (int i = 0; i < 3; i += 2)
            {
                UnitProperties wolf = Turns.circlesMap[parentUnit.sideOnMap, i].newObject;
                if (wolf != null && wolf.pathParent.ID == -2)
                {
                    wolf.damage += Convert.ToInt32(TempValue * Value2);
                    wolf.HpDamage("dmg");
                    Instantiate(Effect2, wolf.pathBulletTarget.position, Quaternion.identity);
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
                nameText = "Морозная связь";
                SType = "Проклятье";
                description = $"Этот персонаж передал часть своей атаки снежному волку.";
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
                nameText = "Морозная связь";
                SType = "Проклятье";
                description = $"Дух монаха забирает {Convert.ToInt32(Value * 100)}% урона у выбранного существа и отдает своему волку, урон при этом умножается на {Value}\r\nНеобходимая энергия: 2";
            }
        }
    }
    public override void EndDebuff()
    {
        parentUnit.damage += TempValue;
        parentUnit.HpDamage("dmg");
    }
}
