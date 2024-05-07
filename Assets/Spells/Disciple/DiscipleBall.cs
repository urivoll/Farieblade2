using System;
public class DiscipleBall : AbstractSpell
{
    private float withProsent;
    void Start()
    {
        withProsent = prosentDamage * fromUnit.damage;
        if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Fire Flash";
                SType = "Ranged ability";
                description = $"The student causes a fiery flash that deals {Convert.ToInt32(withProsent)} damage.\r\nEnergy required: 3";
            }
            else
            {
                nameText = "Огненная вспышка";
                SType = "Способность дальней дистанции";
                description = $"Ученик вызывает огненную вспышку, которая наносит {Convert.ToInt32(withProsent)} ед урона.\r\nНеобходимая энергия: 3";
            }
        }
    }
}
