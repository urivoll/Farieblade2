using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GreoMark : AbstractSpell
{
    private int Value;
    [SerializeField] private GameObject Effect2;
    void Start()
    {
        Value = 5 + fromUnit.grade / 2;
        if (transform.parent.gameObject.name == "Debuffs")
        {
            Turns.punch += Cast;
            if (PlayerData.language == 0)
            {
                nameText = "Sniper's Mark";
                SType = "Buff";
                description = $"This creature is marked Greo. Anyone who hits him will receive additional accuracy.";
            }
            else
            {
                nameText = "Метка снайпера";
                SType = "Усиливающее заклинание";
                description = $"Это существо помечено Грео. Любой кто его ударит получит дополнительную точность.";
            }
        }
        else if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Sniper's Mark";
                SType = "Buff";
                description = $"Greo tags the opponent. Anyone who hits him will receive additional accuracy.\r\nEnergy required: 3\nDuration: 2\nAccuracy: +{Value}";
            }
            else
            {
                nameText = "Метка снайпера";
                SType = "Усиливающее заклинание";
                description = $"Грео помечает противника. Любой кто его ударит получит дополнительную точность.\r\nНеобходимая энергия: 3\nДлительность: 2\nТочность: +{Value}";
            }
        }
    }
    private void Cast(UnitProperties victim, UnitProperties from, List<Dictionary<string, int>> inpData)
    {
        for (int i = 0; i < inpData.Count; i++)
        {
            if (inpData[i]["side"] == parentUnit.sideOnMap &&
                inpData[i]["place"] == parentUnit.placeOnMap &&
                inpData[i]["debuffId"] == id)
            {
                UnitProperties unit = Turns.circlesMap[inpData[i]["sideTarget"], inpData[i][$"placeTarget"]].newObject;
                unit.accuracy += inpData[i]["acc"];
                if (unit.accuracy > 100) unit.accuracy = 100;
                GameObject newObj = Instantiate(Effect2, unit.pathBulletTarget.position, Quaternion.identity);
                if (PlayerData.language == 0) newObj.transform.Find("TextDamage/Text").GetComponent<TextMeshProUGUI>().text = $"+{inpData[i]["acc"]} Точности";
                else newObj.transform.Find("TextDamage/Text").GetComponent<TextMeshProUGUI>().text = $"+{inpData[i]["acc"]} Accuracy";
                newObj.transform.Find("TextDamage/Text").GetComponent<Animator>().SetTrigger("Alarm");
            }
        }
    }
    public override void EndDebuff()
    {
        Turns.punch -= Cast;
    }
}
