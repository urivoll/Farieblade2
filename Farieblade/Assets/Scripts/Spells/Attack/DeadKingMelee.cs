using System;

public class DeadKingMelee : AbstractSpell
{
    private float withProsent;
    void Start()
    {
        withProsent = prosentDamage * fromUnit.damage;
        if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Dead Man's Strike";
                SType = "Melee ability";
                description = $"The dead king accumulates the power of the dead in his sword, causing great damage when attacking.\r\nEnergy required: 3\r\nDamage: {Convert.ToInt32(withProsent)}";
            }
            else
            {
                nameText = "Удар мертвеца";
                SType = "Способность ближней дистанции";
                description = $"Мертвый король, копит силу мертвецов в своем мече, при атаке наносится большой урон.\r\nНеобходимая энергия: 3\r\nУрон:{Convert.ToInt32(withProsent)} ед.";
            }
        }
    }
}
