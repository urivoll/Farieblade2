using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoulSeekerRes : AbstractSpell
{
    public float value = 0.1f;
    [SerializeField] private GameObject debuff;
    void Start()
    {
        if (transform.parent.gameObject.name == "Debuffs")
        {
            value += parentUnit.pathParent.grade * 0.01f;
            parentUnit.transform.Find("ModeParticle2").gameObject.SetActive(true);
        }
        else if (transform.parent.gameObject.name == "Spells")
        {
            value += fromUnit.grade * 0.01f;
        }
        if (PlayerData.language == 0)
        {
            nameText = "Astral Explosion";
            SType = "Stuck debuff";
            description = $"The Soul Seeker places a curse on the enemy that weakens the enemy's resistance.\nDuration: 2\nResistance: -{Convert.ToInt32(value * 100)}%";
        }
        else
        {
            nameText = "Астральный взрыв";
            SType = "Накапливающеется проклятье";
            description = $"Искатель душ накладывает на врага проклятье которое ослабляет сопротивление врага.\nДлительность: 2\nСопротивление: -{Convert.ToInt32(value * 100)}%";
        }
    }
    public override void EndDebuff()
    {
        parentUnit.transform.Find("ModeParticle2").gameObject.SetActive(false);
    }
    public override IEnumerator AfterStep(Dictionary<string, int> inpData)
    {
        UnitProperties unit = Turns.circlesMap[inpData["sideTarget"], inpData["placeTarget"]].newObject;
        GameObject newObject = Instantiate(debuff, unit.pathDebuffs);
        newObject.GetComponent<AbstractSpell>().fromUnit = parentUnit.pathParent;
        yield return new WaitForSeconds(0.8f);
        Turns.finishEndEvent = true;
    }
}
