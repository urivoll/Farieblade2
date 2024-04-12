public class WitchSplittingProtection : AbstractSpell
{
    public float Value = 0.2f;
    void Start()
    {
        Value += fromUnit.grade * 0.01f;
        if (transform.parent.gameObject.name == "Debuffs")
        {
            parentUnit.resistance -= Value;
        }
        if (PlayerData.language == 0)
        {
            nameText = "Armor Splitting";
            SType = "Debuff";
            description = $"Witch of the Crimson Fields shatters the enemy's armor. Damage to target increased.\r\nEnergy required: 1\r\nDuration: 2\r\nResistance weakening: -{Value}%";
        }
        else
        {
            nameText = "Раскол брони";
            SType = "Проклятье";
            description = $"Ведьма багровых полей расщепляет броню противника. Урон по цели повышен.\r\nНеобходимая энергия: 1\r\nДлительность: 2\r\nОслабление сопротивления: -{Value}%";
        }
    }
    public override void EndDebuff()
    {
        parentUnit.resistance += Value;
    }
}
