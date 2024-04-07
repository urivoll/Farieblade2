using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SnowWarRock : AbstractSpell
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
                nameText = "Headshot";
                SType = "Ranged ability";
                description = $"The snow warrior throws a stone, aiming for the head to stun the enemy.\r\nEnergy required: 2\r\nDamage: {Convert.ToInt32(withProsent)}\r\nStun Chance: 50%";
            }
            else
            {
                nameText = "Попадание в голову";
                SType = "Способность дальней дистанции";
                description = $"Снежный воин кидает камень, при этом целясь в голову для того чтобы оглушить противника.\r\nНеобходимая энергия: 2\r\nУрон: {Convert.ToInt32(withProsent)}\r\nШанс оглушения: 50%";
            }
        }
    }
    public override IEnumerator AfterStep(Dictionary<string, int> inpData)
    {
        GameObject newObject = Instantiate(debuff, Turns.circlesMap[inpData["sideEnemy"], inpData["placeEnemy"]].newObject.pathDebuffs);
        newObject.GetComponent<AbstractSpell>().fromUnit = fromUnit;
        yield return new WaitForSeconds(0.5f);
        Turns.finishEndEvent = true;
    }
}
