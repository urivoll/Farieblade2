using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HealerHeal : AbstractSpell
{
    public float Value;
    [SerializeField] private GameObject heal;
    void Start()
    {
        Value = 20 + 20 * (0.1f * fromUnit.level);
        Value = Value + Value * (0.4f * fromUnit.grade);
        if (PlayerData.language == 0)
        {
            nameText = "Heal";
            SType = "Buff";
            description = $"The healer grants life force to an ally. The target is healed.\r\nEnergy required: 1\r\nHeal: {Convert.ToInt32(Value)}";
        }
        else
        {
            nameText = "Лечение";
            SType = "Усиливающее заклинание";
            description = $"Целитель дарует жизненную силу союзнику. Цель исцеляется.\r\nНеобходимая энергия: 1\r\nЛечение: {Convert.ToInt32(Value)}";
        }
    }
    public override IEnumerator HitEffect(Dictionary<string, int> inpData)
    {
        yield return new WaitForSeconds(timeBeforeShoot);
        if (inpData.ContainsKey("heal"))
        {
            UnitProperties targetUnit = Turns.circlesMap[inpData["side"], inpData["place"]].newObject;
            targetUnit.hp = inpData["heal"];
            targetUnit.HpDamage("hp");
            Instantiate(Effect, targetUnit.pathBulletTarget.position, Quaternion.identity);
            Instantiate(heal, targetUnit.pathBulletTarget.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(0.4f);
        Turns.hitDone = true;
    }
}
