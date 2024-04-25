using System;
public class FermorFireBall : AbstractSpell
{
    public float withProsent;
    void Start()
    {
        withProsent = prosentDamage * fromUnit.damage;
        if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Fire ball";
                SType = "Ranged ability";
                description = $"Fermor casts a Fire ball that deals {Convert.ToInt32(withProsent)} damage.\r\nEnergy required: 4";
            }
            else
            {
                nameText = "Огненная шар";
                SType = "Способность дальней дистанции";
                description = $"Фермор вызывает Огненный шар, который наносит {Convert.ToInt32(withProsent)} ед урона.\r\nНеобходимая энергия: 4";
            }
        }
    }
}
