using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class NecrBless : AbstractSpell
{
    [SerializeField] private GameObject Effect2;
    void Start()
    {
        if (transform.parent.gameObject.name == "Debuffs")
        {
            Turns.tryDeath += Cast;
        }
        else if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Will";
                SType = "Buff";
                description = $"After a character's death, a random ally receives one of the following buffs: Damage: +20% or Health +20% or Accuracy +10 or Initiative +10.\r\nEnergy required: 3";
            }
            else
            {
                nameText = "Завещание";
                SType = "Усиливающее заклинание";
                description = $"После смерти персонажа, случайный союзник получает один из усилений: Урон: + 20% или Здоровье +20% или Точность +10 или Инициатива +10.\r\nНеобходимая энергия: 3";
            }
        }
    }
    private void Cast(UnitProperties victim, Dictionary<string, int> inpData)
    {
        if (inpData["sideFrom"] == parentUnit.sideOnMap &&
            inpData["placeFrom"] == parentUnit.placeOnMap &&
            inpData["debuffId"] == id)
        {
            UnitProperties unit = Turns.circlesMap[inpData["sideTarget"], inpData["placeTarget"]].newObject;
            GameObject newObj = Instantiate(Effect2, unit.pathBulletTarget.position, Quaternion.identity);
            if (inpData["effect"] == 0)
            {
                unit.damage += Convert.ToInt32(unit.damage * 0.2f);
                unit.HpDamage("dmg");
                if (PlayerData.language == 0) newObj.transform.Find("TextDamage/Text").GetComponent<TextMeshProUGUI>().text = $"+{Convert.ToInt32(unit.damage * 0.2f)} Урона";
                else newObj.transform.Find("TextDamage/Text").GetComponent<TextMeshProUGUI>().text = $"+{Convert.ToInt32(unit.damage * 0.2f)} Damage";
            }
            else if (inpData["effect"] == 1)
            {
                unit.initiative += 10;
                if (PlayerData.language == 0) newObj.transform.Find("TextDamage/Text").GetComponent<TextMeshProUGUI>().text = "+10 Инициативы";
                else newObj.transform.Find("TextDamage/Text").GetComponent<TextMeshProUGUI>().text = "+10 Initiative";
            }
            else if (inpData["effect"] == 2)
            {
                unit.accuracy += 10;
                if (unit.accuracy > 100) unit.accuracy = 100;
                if (PlayerData.language == 0) newObj.transform.Find("TextDamage/Text").GetComponent<TextMeshProUGUI>().text = "+10 Точности";
                else newObj.transform.Find("TextDamage/Text").GetComponent<TextMeshProUGUI>().text = "+10 Accuracy";
            }
            else if (inpData["effect"] == 3)
            {
                unit.hp += Convert.ToInt32(unit.hp * 0.2f);
                unit.HpDamage("hp");
                if (PlayerData.language == 0) newObj.transform.Find("TextDamage/Text").GetComponent<TextMeshProUGUI>().text = $"+{Convert.ToInt32(unit.hp * 0.2f)} Здоровья";
                else newObj.transform.Find("TextDamage/Text").GetComponent<TextMeshProUGUI>().text = $"+{Convert.ToInt32(unit.hp * 0.2f)} HP";
            }
            newObj.transform.Find("TextDamage/Text").GetComponent<Animator>().SetTrigger("Alarm");
            Destroy(gameObject);
        }
    }
    public override void EndDebuff()
    {
        Turns.tryDeath -= Cast;
    }
}
