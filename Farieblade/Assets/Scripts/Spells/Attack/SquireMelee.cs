using System;
public class SquireMelee : AbstractSpell
{
    public float withProsent;
    void Start()
    {
        withProsent = prosentDamage * fromUnit.damage;
        if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Double punch";
                SType = "Melee ability";
                description = $"The Squire makes a double attack.\r\nEnergy required: 2\r\nDamage per hit: {Convert.ToInt32(withProsent)} damage.";
            }
            else
            {
                nameText = "Двойной удар";
                SType = "Способность ближней дистанции";
                description = $"Скваер проводит двойную атаку.\r\nНеобходимая энергия: 2\r\nУрон за удар: {Convert.ToInt32(withProsent)} ед.";
            }
        }
    }
    public override void SwishMethod(int count)
    {
        fromUnit.Model.transform.Find("AttackSwish").gameObject.SetActive(true);
    }
}
