public class Defend : AbstractSpell
{
    void Start()
    {
        if (PlayerData.language == 0)
        {
            nameText = "Protection";
            SType = "Buff";
            description = $"This character is protected, damage to him is reduced by 50%.";
        }
        else
        {
            nameText = "«ащита";
            SType = "”силивающа€ способность";
            description = $"Ётот персонаж под защитой, урон по нему уменьшен на 50%.";
        }
    }
}
