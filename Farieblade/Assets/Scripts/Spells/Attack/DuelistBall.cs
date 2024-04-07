using System;
using System.Collections;
using System.Collections.Generic;

public class DuelistBall : AbstractSpell
{
    private float withProsent;
    void Start()
    {
        withProsent = prosentDamage * fromUnit.damage;
        if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Ball of Energy";
                SType = "Ranged ability";
                description = $"Akara summons a ball of energy that deals {Convert.ToInt32(withProsent)} damage to the enemy and takes away 2 energy\r\nEnergy required: 4";
            }
            else
            {
                nameText = "Шар энергии";
                SType = "Способность дальней дистанции";
                description = $"Акара вызывает шар энергии, который наносит противнику {Convert.ToInt32(withProsent)} ед. урона и отнимает 2 ед. энергии.\r\nНеобходимая энергия: 2";
            }
        }
    }
    public override IEnumerator HitEffect(Dictionary<string, int> inpData)
    {
        yield return null;
        UnitProperties unit = Turns.circlesMap[inpData["side"], inpData["place"]].newObject;
        unit.pathEnergy.SetEnergy(inpData["energy"]);
    }
}
