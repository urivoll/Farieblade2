using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DwarfRass : AbstractSpell
{
    void Start()
    {
        if (PlayerData.language == 0)
        {
            nameText = "Medicine of Dispelling";
            SType = "Buff";
            description = $"The dwarf alchemist gives his ally a dispel potion, thereby removing all negative effects from him.\r\nEnergy required: 1";
        }
        else
        {
            nameText = "Снадобье рассеивания";
            SType = "Усиливающее заклинание";
            description = $"Гном алхимик дает своему союзнику снадобье рассеивания, тем самым с него снимаются все отрицательные эффекты.\r\nНеобходимая энергия: 1";
        }
    }
    public override IEnumerator HitEffect(Dictionary<string, int> inpData)
    {
        UnitProperties targetUnit = Turns.circlesMap[inpData["side"], inpData["place"]].newObject;
        yield return new WaitForSeconds(timeBeforeShoot);
        for (int i = 0; i < targetUnit.idDebuff.Count; i++)
        {
            if (targetUnit.idDebuff[i].GetComponent<AbstractSpell>().Type == "Debuff")
                Destroy(targetUnit.idDebuff[i]);
        }
        yield return new WaitForSeconds(1f);
        Turns.hitDone = true;
    }
}
