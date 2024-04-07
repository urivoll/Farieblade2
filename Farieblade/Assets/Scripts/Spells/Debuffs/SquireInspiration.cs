using System;
public class SquireInspiration : AbstractSpell
{
    private float Value = 0.15f;
    private int TempValue;
    void Start()
    {
        Value += (fromUnit.grade * 0.01f);
        if (transform.parent.gameObject.name == "Debuffs")
        {
            TempValue = Convert.ToInt32(parentUnit.damage * Value);
            parentUnit.damage += TempValue;
            parentUnit.HpDamage("dmg");

            if (PlayerData.language == 0)
            {
                nameText = "Inspiration";
                SType = "Buff";
                description = $"This character has a good fighting spirit. Damage increased.\nDamage Boost: {Convert.ToInt32(Value * 100)}%";
            }
            else
            {
                nameText = "Воодушевление";
                SType = "Усиливающее заклинание";
                description = $"Этот персонаж с хорошим боевым духом. Урон увеличен.\nУсиление урона: {Convert.ToInt32(Value * 100)}%";
            }
        }
        else if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Inspiration";
                SType = "Buff";
                description = $"The squire raises the morale of his comrades. Damage increases.\r\nEnergy required: 2\nDuration: 3\nDamage Boost: {Convert.ToInt32(Value * 100)}%";
            }
            else
            {
                nameText = "Воодушевление";
                SType = "Усиливающее заклинание";
                description = $"Скваер поднимает боевой дух своим товарищам. Урон увеличивается.\r\nНеобходимая энергия: 2\nДлительность: 3\nУсиление урона: {Convert.ToInt32(Value * 100)}%";
            }
        }
    }
    public override void EndDebuff()
    {
        if (transform.parent.gameObject.name == "Debuffs")
        {
            parentUnit.damage -= TempValue;
            parentUnit.HpDamage("dmg");
        }
    }
}
