using System;
public class RiderMelee : AbstractSpell
{
    public float withProsent;
    public float vaule;
    void Start()
    {
        withProsent = prosentDamage * fromUnit.damage;
        vaule = 4 + (fromUnit.grade * 0.4f);
        if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Strike of light";
                SType = "Melee ability";
                description = $"The horse rears up and the knight strikes with a full swing, dealing {Convert.ToInt32(withProsent)} damage and reducing the enemy's total health by {Convert.ToInt32(vaule)}%.\r\nEnergy required: 3";
            }
            else
            {
                nameText = "Удар света";
                SType = "Способность ближней дистанции";
                description = $"Лошадь встает на дыбы, рыцарь наносит удар с размаху, нанося {Convert.ToInt32(withProsent)} ед. урона и снижая общее здоровье противника на {Convert.ToInt32(vaule)}%.\r\nНеобходимая энергия: 3";
            }
        }
    }
    public override void SwishMethod(int count)
    {
        fromUnit.Model.transform.Find("AttackSwish").gameObject.SetActive(true);
    }
}
