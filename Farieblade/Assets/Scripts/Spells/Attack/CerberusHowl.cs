using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CerberusHowl : AbstractSpell
{
    private float withProsent;
    [SerializeField] private GameObject StrikeEffect;
    [SerializeField] private GameObject debuff;
    void Start()
    {
        withProsent = prosentDamage * fromUnit.damage;
        if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Firefall";
                SType = "Ranged ability";
                description = $"The target and 2 random enemy units receive {Convert.ToInt32(withProsent)} damage and the 'Fire Mania' curse.\r\nEnergy required: 3\r\nDuration: 3";
            }
            else
            {
                nameText = "Огнепад";
                SType = "Способность дальней дистанции";
                description = $"Выбранный и 2 случайных существ врага получают {Convert.ToInt32(withProsent)} ед. урона и проклятье 'Огненная мания'. \r\nНеобходимая энергия: 3\r\nДлительность: 3";
            }
        }
    }
    public override IEnumerator HitEffect(Dictionary<string, int> inpData)
    {
        yield return new WaitForSeconds(timeBeforeShoot);
        for (int i = 0; i < inpData["count"]; i++)
        {
            UnitProperties victim = Turns.circlesMap[inpData[$"sideOnMap{i}"], inpData[$"placeOnMap{i}"]].newObject;
            int damage = inpData[$"damage{i}"];
            Instantiate(StrikeEffect, victim.pathBulletTarget.position, Quaternion.identity, Turns.circlesTransform.transform);
            GameObject newObject = Instantiate(debuff, victim.pathDebuffs);
            victim.SpellDamage(damage, 5);
            newObject.GetComponent<AbstractSpell>().fromUnit = fromUnit;
        }
        yield return new WaitForSeconds(0.3f);
        Turns.hitDone = true;
    }
}
