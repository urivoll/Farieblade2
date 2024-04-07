using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwinsDamage : AbstractSpell
{
    private float Value;
    private UnitProperties victim;
    [SerializeField] private GameObject debuff;
    void Start()
    {
        Value = 60 + (fromUnit.grade * 2);
        if (transform.parent.gameObject.name == "Debuffs")
        {
            Turns.punch += CastDebuff;
        }
        if (PlayerData.language == 0)
        {
            nameText = "Block";
            SType = "Passive";
            description = $"The first one to hit Twins will receive a curse that will reduce the attacker's base damage by {Convert.ToInt32(Value)}%. Once hit, the ability disappears until the end of the battle.";
        }
        else
        {
            nameText = "Блок";
            SType = "Пассивная";
            description = $"Первый кто ударит Близнецов получит проклятие которое уменьшит базовый урон атакующего на {Convert.ToInt32(Value)}% После удара способность исчезает до конца битвы.";
        }
    }

    private void CastDebuff(UnitProperties Victim, UnitProperties from, List<Dictionary<string, int>> inpData)
    {
        if (Victim == parentUnit)
        {
            Turns.eventEndCard.Add(gameObject);
            victim = from;
        }
    }
    public override IEnumerator AfterStep(Dictionary<string, int> inpData)
    {
        if (victim != null && victim.pathCircle.newObject != null)
        {
            GameObject newObject = Instantiate(debuff, victim.pathDebuffs);
            newObject.GetComponent<AbstractSpell>().fromUnit = parentUnit.pathParent;
        }
        yield return new WaitForSeconds(0.8f);
        Turns.finishEndEvent = true;
        Destroy(gameObject);
    }
    public override void EndDebuff()
    {
        Turns.punch -= CastDebuff;
    }
}
