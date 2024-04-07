using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FlanceShamanSwampBuff : AbstractSpell
{
    private float Value;
    [SerializeField] private GameObject Effect2;
    void Start()
    {
        Value = fromUnit.damage * (0.4f + fromUnit.grade * 0.02f);
        if (transform.parent.gameObject.name == "Debuffs")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Swamp Defense";
                SType = "Buff";
                description = $"This character is under swamp protection, after an enemy attack, {Convert.ToInt32(Value)} of the damage will be returned to the attacker.";
            }
            else
            {
                nameText = "Болотная защита";
                SType = "Усиливающее заклинание";
                description = $"Этот персонаж под болотной защитой, после атаки противника, {Convert.ToInt32(Value)} ед. урона возвратится атакующему.";
            }
        }
        else if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Swamp Defense";
                SType = "Buff";
                description = $"Flance Shaman performs a Swamp Dance, which grants Swamp Defense to the targeted ally. After an enemy attack, {Convert.ToInt32(Value)} of the damage will be returned to the attacker.\r\nEnergy required: 2\r\nDuration: 3";
            }
            else
            {
                nameText = "Болотная защита";
                SType = "Усиливающее заклинание";
                description = $"Фленц Шаман исполняет болотный танец, который дает выбранному союзнику Болотную защиту. После атаки противника, {Convert.ToInt32(Value)} ед. урона возвратится атакующему.\r\nНеобходимая энергия: 2\r\nДлительность: 3";
            }
        }
    }
    public override IEnumerator AfterStep(Dictionary<string, int> inpData)
    {
        UnitProperties targetUnit = Turns.circlesMap[inpData["sideTarget"], inpData["placeTarget"]].newObject;
        Instantiate(Effect2, targetUnit.pathBulletTarget.position, Quaternion.identity);
        targetUnit.SpellDamage(inpData["damage"], 3);
        yield return new WaitForSeconds(0.3f);
        Turns.finishEndEvent = true;
    }
}
