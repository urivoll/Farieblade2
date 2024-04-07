using System;
public class ZombieMelee : AbstractSpell
{
    public float withProsent;
    void Start()
    {
        withProsent = prosentDamage * fromUnit.damage;
        if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Strong claw strike";
                SType = "Melee ability";
                description = $"The zombie swings and strikes the target with its claws, dealing {Convert.ToInt32(withProsent)} damage.\r\nEnergy required: 3";
            }
            else
            {
                nameText = "Сильный удар когтями";
                SType = "Способность ближней дистанции";
                description = $"Зомби размахивается и бьет цель когтями, нанося {Convert.ToInt32(withProsent)} ед. урона.\r\nНеобходимая энергия: 3";
            }
        }
    }
}
