using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WalkerRass : AbstractSpell
{
    public GameObject Effect2;
    void Start()
    {
        if (PlayerData.language == 0)
        {
            nameText = "Diffusion";
            SType = "Debuff";
            description = $"Walker removes positive effects from the enemy target.\r\nEnergy required: 2";
        }
        else
        {
            nameText = "Рассеивание";
            SType = "Проклятье";
            description = $"Валкер снимает положительные эффекты с вражеской цели.\r\nНеобходимая энергия: 2";
        }
    }
    public override IEnumerator HitEffect(Dictionary<string, int> inpData)
    {
        UnitProperties targetUnit = Turns.circlesMap[inpData["side"], inpData["place"]].newObject;
        yield return new WaitForSeconds(timeBeforeShoot);
        for (int i = 0; i < targetUnit.idDebuff.Count; i++)
        {
            if (targetUnit.idDebuff[i].GetComponent<AbstractSpell>().Type == "Buff")
                Destroy(targetUnit.idDebuff[i]);
        }
        Instantiate(Effect2, targetUnit.pathBulletTarget.position, Quaternion.identity);
        yield return new WaitForSeconds(0.4f);
        Turns.hitDone = true;
    }
}
