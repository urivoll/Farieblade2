using System;
using System.Collections.Generic;
public class TwinsBless : AbstractSpell
{
    private float Value;
    private int Value2;
    private float Value3;
    private int TempValue;
    void Start()
    {
        Value = 0.1f + (fromUnit.grade * 0.01f);
        Value3 = 0.15f + (fromUnit.grade * 0.01f);
        Value2 = 10 + fromUnit.grade;
        if (transform.parent.gameObject.name == "Debuffs")
        {
            duration = 3;
            startNumberTurn = Turns.numberTurn + duration;
            Turns.punch += Cast;
            TempValue = Convert.ToInt32(parentUnit.damage * Value);
            parentUnit.damage -= TempValue;
            parentUnit.initiative += Value2;
            parentUnit.HpDamage("dmg");

            if (PlayerData.language == 0)
            {
                nameText = "Impudence with armor";
                SType = "Buff";
                description = $"The chosen creature, sacrificing its strength, becomes arrogant and armored.";
            }
            else
            {
                nameText = "Наглость с броней";
                SType = "Усиливающее заклинание";
                description = $"Выбранное существо, жертвуя своей силой, становится наглым и бронированным.";
            }
        }
        else if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Impudence with armor";
                SType = "Buff";
                description = $"The chosen creature, sacrificing its strength, becomes arrogant and armored.\r\nEnergy required: 3\nDuration: 3\nDamage: -{Convert.ToInt32(Value * 100)}%\r\nInitiative: +{Value}\r\nDefense: +{Convert.ToInt32(Value3 * 100)}";
            }
            else
            {
                nameText = "Наглость с броней";
                SType = "Усиливающее заклинание";
                description = $"Выбранное существо, жертвуя своей силой, становится наглым и бронированным.\r\nНеобходимая энергия: 3\nДлительность: 3\nУрон: -{Convert.ToInt32(Value * 100)}%\r\nИнициатива: +{Value}\r\nЗащита: +{Convert.ToInt32(Value3 * 100)}%";
            }
        }
    }
    private void Cast(UnitProperties victim, UnitProperties who, List<Dictionary<string, int>> inpData)
    {
        if (victim == parentUnit)
        {
            parentUnit.inpDamage -= parentUnit.inpDamage * Value;
        }
    }
    public override void EndDebuff()
    {
        parentUnit.damage += TempValue;
        parentUnit.initiative -= Value2;
        parentUnit.HpDamage("dmg");
        Turns.punch -= Cast;
    }
}
