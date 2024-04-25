using System;
public class LizardWarriorBall : AbstractSpell
{
    public float withProsent;
    void Start()
    {
        withProsent = prosentDamage * fromUnit.damage;
        if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Javelin-throwing";
                SType = "Ranged ability";
                description = $"The lizard warrior throws his spear at the enemy, thereby inflicting {Convert.ToInt32(withProsent)} damage.\r\nEnergy required: 2";
            }
            else
            {
                nameText = "Метание копья";
                SType = "Способность дальней дистанции";
                description = $"Ящер воин метает свое копье в противника тем самым нанося {Convert.ToInt32(withProsent)} ед. урона.\r\nНеобходимая энергия: 2";
            }
        }
    }
}
