using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MinionNeedleSwell : AbstractSpell
{
    private float withProsent;
    void Start()
    {
        withProsent = fromUnit.damage * prosentDamage;
        if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Needle explosion";
                SType = "Ranged ability";
                description = $"The minion inflates, accumulating needles in its body, and then emits them in different directions. Deals {Convert.ToInt32(withProsent)} damage to 3 targets.\r\nEnergy required: 3";
            }
            else
            {
                nameText = "Взрыв игл";
                SType = "Способность дальней дистанции";
                description = $"Миньон надувается копя иглы в своем теле, после чего испускает их в разные стороны. Наносит {Convert.ToInt32(withProsent)} ед. урона по 3 целям.\r\nНеобходимая энергия: 3";
            }
        }
    }
    public override IEnumerator HitEffect(Dictionary<string, int> inpData)
    {
        yield return new WaitForSeconds(timeBeforeShoot);
        BattleSound.sound.PlayOneShot(soundAfter);
        for (int i = 0; i < inpData["count"]; i++)
        {
            UnitProperties victim = Turns.circlesMap[inpData[$"sideOnMap{i}"], inpData[$"placeOnMap{i}"]].newObject;
            int damage = inpData[$"damage{i}"];
            CreateBullet(victim, damage);
        }
        yield return new WaitForSeconds(0.3f);
        Turns.hitDone = true;
    }
    private void CreateBullet(UnitProperties victim, int damage)
    {
        GameObject bulletTarget = victim.pathBulletTarget.gameObject;
        UnitProperties UnitHit = victim;
        GameObject newBullet = Instantiate(Effect, fromUnit.Model.transform.Find("bullet").position, Quaternion.identity);
        var direction = bulletTarget.transform.position - newBullet.transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        newBullet.transform.rotation = Quaternion.Euler(0, 0, angle);
        Bullet bullet = newBullet.GetComponent<Bullet>();
        bullet.unitTarget = UnitHit;
        bullet.unitFrom = fromUnit.Model;
        bullet.damage = damage;
        bullet.element = 4;
    }
}
