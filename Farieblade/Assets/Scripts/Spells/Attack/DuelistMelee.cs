using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuelistMelee : AbstractSpell
{
    public float withProsent;
    [SerializeField] private GameObject debuff;
    void Start()
    {
        withProsent = prosentDamage * fromUnit.damage;
        if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Duelist's Strike";
                SType = "Melee ability";
                description = $"Akara delivers a quick strike to the opponent. The target receives {Convert.ToInt32(withProsent)} damage and the curse: “Silence.”\r\nEnergy required: 3";
            }
            else
            {
                nameText = "Удар дуэлянта";
                SType = "Способность ближней дистанции";
                description = $"Акара наносит быстрый удар по противнику. Цель получает {Convert.ToInt32(withProsent)} ед. урона и проклятье: «Тишина».\r\nНеобходимая энергия: 3";
            }
        }
    }
    public override IEnumerator AfterStep(Dictionary<string, int> inpData)
    {
        int rand = UnityEngine.Random.Range(0, 3);
        if (rand != 2) BattleSound.sound.PlayOneShot(voiceAfter[rand]);
        Instantiate(debuff, Turns.circlesMap[inpData["sideEnemy"], inpData["placeEnemy"]].newObject.pathDebuffs);
        yield return new WaitForSeconds(1.5f);
        Turns.finishEndEvent = true;
    }
    public override void SwishMethod(int count)
    {
        fromUnit.Model.transform.Find("RightSwish").gameObject.SetActive(true);
    }
}
