using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GreoThird : AbstractSpell
{
    private float withProsent;
    void Start()
    {
        withProsent = fromUnit.damage * prosentDamage;
        if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Triplet";
                SType = "Ranged ability";
                description = $"Greo simultaneously pulls 3 arrows, 2 of which will fly to random targets, thereby dealing {Convert.ToInt32(withProsent)} damage.\r\nEnergy required: 3";
            }
            else
            {
                nameText = "Триплет";
                SType = "Способность дальней дистанции";
                description = $"Грео одновременно натягивает 3 стрелы, 2 из которых полетят в случайные цели, тем самым наносит {Convert.ToInt32(withProsent)} ед. урона.\r\nНеобходимая энергия: 3";
            }
        }
    }
    public override IEnumerator HitEffect(Dictionary<string, int> inpData)
    {
        yield return new WaitForSeconds(0.3f);
        BattleSound.sound.PlayOneShot(soundMid);
        yield return new WaitForSeconds(0.8f);
        BattleSound.sound.PlayOneShot(soundAfter);
        for (int i = 0; i < inpData["count"]; i++)
        {
            UnitProperties victim = Turns.circlesMap[inpData[$"sideOnMap{i}"], inpData[$"placeOnMap{i}"]].newObject;
            GameObject bulletTarget = victim.pathBulletTarget.gameObject;
            GameObject newBullet = Instantiate(Effect, fromUnit.Model.transform.Find("bullet").position, Quaternion.identity);
            Bullet bullet = newBullet.GetComponent<Bullet>();
            var direction = bulletTarget.transform.position - newBullet.transform.position;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            newBullet.transform.rotation = Quaternion.Euler(0, 0, angle);
            bullet.unitTarget = victim;
            bullet.unitFrom = fromUnit.Model;
            bullet.damage = inpData[$"damage{i}"];
            bullet.element = 4;
            if (inpData.ContainsKey($"crit{i}")) bullet.end = bullet.transform.Find("Exp").gameObject;
        }
        yield return new WaitForSeconds(0.3f);
        Turns.hitDone = true;
    }
}
