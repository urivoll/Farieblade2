using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerberusMelee : AbstractSpell
{
    private float withProsent;
    private float value;
    [SerializeField] private GameObject useSpell;
    void Start()
    {
        value = 0.3f + fromUnit.grade * 0.01f;
        withProsent = prosentDamage * fromUnit.damage;
        if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Fire Bite";
                SType = "Melee ability";
                description = $"Tsumil deals damage to the enemy equal to his attack, but the damage increases for each charge of 'Hell's Howl' by {Convert.ToInt32(value * 100)}%\r\nEnergy required: 3";
            }
            else
            {
                nameText = "Огненный укус";
                SType = "Способность ближней дистанции";
                description = $"Цумил наносит урон противнику равный своей атаке, однако урон повышается за каждый заряд 'Адского воя' на {Convert.ToInt32(value * 100)}%.\r\nНеобходимая энергия: 3";
            }
        }
    }
    public override IEnumerator HitEffect(Dictionary<string, int> inpData)
    {
        yield return new WaitForSeconds(timeBeforeShoot);
        BattleSound.sound.PlayOneShot(soundMid);
        fromUnit.Model.transform.Find("AttackSwish").gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        BattleSound.sound.PlayOneShot(soundAfter);
        Instantiate(useSpell, Turns.circlesMap[inpData["side"], inpData["place"]].newObject.pathBulletTarget.position, Quaternion.identity);
        Turns.circlesMap[inpData["side"], inpData["place"]].newObject.SpellDamage(inpData["damage"], 5);
        yield return new WaitForSeconds(0.3f);
        Turns.hitDone = true;
    }
}
