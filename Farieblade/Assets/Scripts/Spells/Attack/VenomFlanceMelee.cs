using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class VenomFlanceMelee : AbstractSpell
{
    public float withProsent;
    public GameObject debuff;
    void Start()
    {
        withProsent = prosentDamage * fromUnit.damage;
        if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Strong claw strike";
                SType = "Melee ability";
                description = $"Poison Flance makes 2 hits, dealing {Convert.ToInt32(withProsent)} damage per hit.\r\nEnergy required: 2";
            }
            else
            {
                nameText = "Сильный удар когтями";
                SType = "Способность ближней дистанции";
                description = $"Ядовитый Фленц делает 2 удара, нанося {Convert.ToInt32(withProsent)} ед. урона за удар.\r\nНеобходимая энергия: 2";
            }
        }
    }
    public override void SwishMethod(int count) => fromUnit.Model.transform.Find("AttackSwish").gameObject.SetActive(true);
    public override IEnumerator AfterStep(Dictionary<string, int> inpData)
    {
        GameObject newObject = Instantiate(debuff, Turns.circlesMap[inpData["sideEnemy"], inpData["placeEnemy"]].newObject.pathDebuffs);
        newObject.GetComponent<AbstractSpell>().fromUnit = fromUnit;
        yield return new WaitForSeconds(0.8f);
        Turns.finishEndEvent = true;
    }
}
