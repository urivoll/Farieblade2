using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class VenomFlancePassive : AbstractSpell
{
    public float Value;
    public GameObject debuff;
    void Start()
    {
        Value = (fromUnit.damage / 2) * (1 + fromUnit.grade * 0.1f);
        if (PlayerData.language == 0)
        {
            nameText = "Poisoned Weapon";
            SType = "Passive";
            description = $"After the attack, Flance inflicts poison on the target. The effect may be cumulative.\r\nPoison Damage: {Convert.ToInt32(Value)}";
        }
        else
        {
            nameText = "Отравленное оружие";
            SType = "Пассивная";
            description = $"После атаки Фленц поражает цель ядом. Эффект может накапливаться.\r\nУрон от яда: {Convert.ToInt32(Value)} ед.";
        }
    }
    public override IEnumerator AfterStep(Dictionary<string, int> inpData)
    {
        GameObject newObject = Instantiate(debuff, Turns.circlesMap[inpData["sideTarget"], inpData["placeTarget"]].newObject.pathDebuffs);
        newObject.GetComponent<AbstractSpell>().fromUnit = parentUnit.pathParent;
        yield return new WaitForSeconds(0.8f);
        Turns.finishEndEvent = true;
    }
}