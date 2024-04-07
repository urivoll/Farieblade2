public class WitchShyness : AbstractSpell
{
    public int Value = 20;
    void Start()
    {
        Value += fromUnit.grade;
        if (transform.parent.gameObject.name == "Debuffs")
        {
            duration = 3;
            startNumberTurn = Turns.numberTurn + duration;
            parentUnit.initiative -= Value;
        }
        if (PlayerData.language == 0)
        {
            nameText = "Shyness";
            SType = "Debuff";
            description = $"With just one piercing gaze, the witch of the crimson fields can make any opponent shy.\r\nEnergy required: 1\r\nDuration: 3\r\nInitiative: -{Value}";
        }
        else
        {
            nameText = "Застенчивость";
            SType = "Проклятье";
            description = $"Лишь одним пронзительным взглядом ведьма багровых полей может вызвать застеничивость у любого противника.\r\nНеобходимая энергия: 1\r\nДлительность: 3\r\nИнициатива: -{Value}";
        }
    }
    public override void EndDebuff()
    {
        parentUnit.initiative += Value;
    }
}
