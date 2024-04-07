using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadKingDamn : AbstractSpell
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
                nameText = "Strike with the butt";
                SType = "Melee ability";
                description = $"The Dead King deals {Convert.ToInt32(withProsent)} damage and inflicts 'Weakness' on the victim.\r\nEnergy required: 3";
            }
            else
            {
                nameText = "Удар обухом меча";
                SType = "Способность ближней дистанции";
                description = $"Мертвый король наносит {Convert.ToInt32(withProsent)} ед урона и накладывает на жертву 'Слабость'\r\nНеобходимая энергия: 3";
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
}
