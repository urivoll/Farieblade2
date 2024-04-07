using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LightWizardTriple : AbstractSpell
{
    private float withProsent;
    [SerializeField] private GameObject debuff;
    void Start()
    {
        withProsent = prosentDamage * fromUnit.damage;
        if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Queue of light rays";
                SType = "Ranged ability";
                description = $"The light wizard summons a flurry of small rays of light that strike the enemy. After hitting the enemy, there is a 50% chance that the cumulative curse “Weakness to Light” will be applied to the enemy, which reduces resistance to light by 5%.\r\nEnergy required: 3\r\nShots: 3\r\nDamage per shot: {Convert.ToInt32(withProsent)}";
            }
            else
            {
                nameText = "Очередь лучей света";
                SType = "Способность дальней дистанции";
                description = $"Светлый волшебник вызывает шквал маленьких лучей света поражающих противника. После попадания противнику с шансом в 50% накладывается накапливающее проклятье 'Слабость к свету', которое снижает сопротивление к свету на 5%.\r\nНеобходимая энергия: 3\r\nВыстрелов: 3\r\nDamage per shot: {Convert.ToInt32(withProsent)} ед.";
            }
        }
    }
    public override IEnumerator HitEffect(Dictionary<string, int> inpData)
    {
        yield return null;
        if (inpData["catch"] == 1)
        {
            GameObject newObject = Instantiate(debuff, Turns.circlesMap[inpData["side"], inpData["place"]].newObject.pathDebuffs);
            newObject.GetComponent<AbstractSpell>().fromUnit = fromUnit;
        }
    }
}