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
                nameText = "Лунный свет";
                SType = "Усиливающее заклинание";
                description = $"Этот персонаж усилен лунным светом, эффект дает дополнительные {Convert.ToInt32(Value)} ед. к точности.";
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
                nameText = "Лунный свет";
                SType = "Усиливающее заклинание";
                description = $"Лунный свет дает союзнику дополнительные {Convert.ToInt32(Value)} ед. к точности.\r\nНеобходимая энергия: 2\nДлительность: 3";
            }
        }
    }
    public override void EndDebuff()
    {
        if(parentUnit != null) parentUnit.accuracy -= TempValue;
    }
}
