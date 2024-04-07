using System;
public class GoblinTrap : AbstractSpell
{
    public int Value = 10;
    public float Value2 = 0.15f;
    public int tempInitiative;
    public int tempDamage;
    void Start()
    {
        Value += fromUnit.grade;
        Value2 += fromUnit.grade * 0.01f;
        parentUnit.initiative -= Value;
        tempDamage = Convert.ToInt32(parentUnit.damage * Value2);
        parentUnit.damage -= tempDamage;
        parentUnit.HpDamage("dmg");
        if (transform.parent.gameObject.name == "Debuffs")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Goblin Trap";
                SType = "Debuff";
                description = $"The character is in a trap, characteristics have been reduced.\r\nDamage: -{Convert.ToInt32(Value2 * 10)}%\r\nInitiative: -{Value}";
            }
            else
            {
                nameText = "Гоблинский капкан";
                SType = "Проклятье";
                description = $"Персонаж в капкане, характеристики снижены.\r\nУрон: -{Convert.ToInt32(Value2 * 10)}%\r\nИнициатива: -{Value}";
            }
        }
    }
    public override void EndDebuff()
    {
        base.EndDebuff();
        parentUnit.initiative += Value;
        parentUnit.damage += tempDamage;
        parentUnit.HpDamage("hpdmg");
        Destroy(gameObject);
    }
}
