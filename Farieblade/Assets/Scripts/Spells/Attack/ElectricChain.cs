using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ElectricChain : AbstractSpell
{
    private float withProsent;
    [SerializeField] private GameObject line;
    [SerializeField] private GameObject StrikeEffect;
    private UnitProperties previous = null;
    [SerializeField] private GameObject debuff;
    void Start()
    {
        withProsent = prosentDamage * fromUnit.damage;
        if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Chain lightning";
                SType = "Ranged ability";
                description = $"The electric lizard sends lightning at enemies, which strikes the selected character, dealing {Convert.ToInt32(withProsent)} damage. When jumping to another character, the damage is divided by 2. Maximum targets 3.\r\nEnergy required: 4";
            }
            else
            {
                nameText = "Цепная молния";
                SType = "Способность дальней дистанции";
                description = $"Электрический ящер направляет на врагов молнию, которая бьет по выбранному персонажу нанося {Convert.ToInt32(withProsent)} ед. урона. Перескакивая на другого персонажа урон делится на 2. Максимум целей 3.\r\nНеобходимая энергия: 4";
            }
        }
    }
    public override IEnumerator HitEffect(Dictionary<string, int> inpData)
    {
        yield return new WaitForSeconds(timeBeforeShoot);
        BattleSound.sound.PlayOneShot(soundMid);
        yield return new WaitForSeconds(0.1f);
        BattleSound.sound.PlayOneShot(soundAfter);
        for (int i = 0; i < inpData["count"]; i++)
        {
            CreateBullet(Turns.circlesMap[inpData[$"sideOnMap{i}"], inpData[$"placeOnMap{i}"]].newObject, inpData[$"damage{i}"]);
        }
        previous = null;
        yield return new WaitForSeconds(0.3f);
        Turns.hitDone = true;
    }
    private void CreateBullet(UnitProperties unitTarget, int num)
    {
        GameObject newLine;
        if (previous == null)
        {
            newLine = Instantiate(line, Turns.circlesTransform.transform);
            newLine.GetComponent<LineRenderer>().SetPosition(0, fromUnit.Model.transform.Find("bullet2").gameObject.transform.position);
        }
        else
        {
            newLine = Instantiate(line, Turns.circlesTransform.transform);
            newLine.GetComponent<LineRenderer>().SetPosition(0, previous.pathBulletTarget.position);
        }
        GameObject newObject = Instantiate(debuff, unitTarget.pathDebuffs);
        Instantiate(StrikeEffect, unitTarget.pathBulletTarget.position, Quaternion.identity, Turns.circlesTransform.transform);
        unitTarget.SpellDamage(num, 0);
        newLine.GetComponent<LineRenderer>().SetPosition(1, unitTarget.pathBulletTarget.position);
        previous = unitTarget;
        newObject.GetComponent<AbstractSpell>().fromUnit = fromUnit;
    }
}
