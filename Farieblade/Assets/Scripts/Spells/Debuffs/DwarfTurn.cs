using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DwarfTurn : AbstractSpell
{
    void Start()
    {
        if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Potion of Double Initiative";
                SType = "Buff";
                description = $"The Dwarf Alchemist gives an ally a potion of double initiative, thereby allowing the chosen creature to make its move again.\r\nEnergy required: 1";
            }
            else
            {
                nameText = "Зелье двойной инициативы";
                SType = "Усиливающее заклинание";
                description = $"Гном алхимик дает союзнику снадобье двойной инициативы, тем самым выбранное существо может совершить ход вновь.\r\nНеобходимая энергия: 1";
            }
        }
    }
    public override IEnumerator HitEffect(Dictionary<string, int> inpData)
    {
        UnitProperties targetUnit = Turns.circlesMap[inpData["side"], inpData["place"]].newObject;
        yield return new WaitForSeconds(timeBeforeShoot);
        targetUnit.went = false;
        yield return new WaitForSeconds(1f);
        Turns.hitDone = true;
    }
}
