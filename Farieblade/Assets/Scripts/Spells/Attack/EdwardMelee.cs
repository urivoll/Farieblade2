using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EdwardMelee : AbstractSpell
{
    public float withProsent;
    void Start()
    {
        withProsent = prosentDamage * fromUnit.damage;
        if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Strong claw strike";
                SType = "Melee ability";
                description = $"Edward strikes twice. The enemy receives {Convert.ToInt32(withProsent)} damage, and Edward receives an additional 2 charges of power.\r\nEnergy required: 5";
            }
            else
            {
                nameText = "Сильный удар когтями";
                SType = "Способность ближней дистанции";
                description = $"Эдвард наносит двойной удар. Противник получает {Convert.ToInt32(withProsent)} урона, а Эдвард дополнительные 2 заряда силы.\r\nНеобходимая энергия: 5";
            }
        }
    }
    public override IEnumerator HitEffect(Dictionary<string, int> inpData)
    {
        yield return new WaitForSeconds(timeBeforeShoot);
        StartIni.soundVoice.StrikeVoices(fromUnit.Model.indexVoice);
        BattleSound.sound.PlayOneShot(soundMid);
        SwishMethod(0);
        yield return new WaitForSeconds(0.1f);
        BattleSound.sound.PlayOneShot(soundAfter);
        UnitProperties unit = Turns.circlesMap[inpData["side"], inpData["place"]].newObject;
        unit.SpellDamage(inpData["damage"], inpData["element"]);

        yield return new WaitForSeconds(0.1f);
        fromUnit.Model.pathDebuffs.Find("EdwardPassive").GetComponent<EdwardPassive>().Up();
        yield return new WaitForSeconds(0.1f);
        fromUnit.Model.pathDebuffs.Find("EdwardPassive").GetComponent<EdwardPassive>().Up();
        if (unit.pathParent.resist == inpData["element"])
        {
            yield return new WaitForSeconds(0.1f);
            fromUnit.Model.pathDebuffs.Find("EdwardPassive").GetComponent<EdwardPassive>().Up();
        }
        yield return new WaitForSeconds(0.2f);
        Turns.hitDone = true;
    }
    public override void SwishMethod(int count) => fromUnit.Model.transform.Find("AttackSwish").gameObject.SetActive(true);
}
