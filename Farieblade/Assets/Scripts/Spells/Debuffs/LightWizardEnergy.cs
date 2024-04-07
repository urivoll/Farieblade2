public class LightWizardEnergy : AbstractSpell
{
    void Start()
    {
        if (transform.parent.gameObject.name == "Debuffs")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Wizard's Blessing";
                SType = "Buff";
                description = $"Whenever a character accumulates energy, he will also receive an additional 1 unit. energy.";
            }
            else
            {
                nameText = "Благословение волшебника";
                SType = "Усиливающее заклинание";
                description = $"Всякий раз когда персонаж накапливает энергию, он также получит дополнительную 1 ед. энергии.";
            }
        }
        else if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Wizard's Blessing";
                SType = "Buff";
                description = $"Whenever a character accumulates energy, he will also receive an additional 1 unit. energy.\r\nEnergy required: 3\nDuration: 2";
            }
            else
            {
                nameText = "Благословение волшебника";
                SType = "Усиливающее заклинание";
                description = $"Всякий раз когда персонаж накапливает энергию, он также получит дополнительную 1 ед. энергии.\r\nНеобходимая энергия: 3\nДлительность: 2";
            }
        }
    }
}
