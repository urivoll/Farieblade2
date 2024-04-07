using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NecromancerResurect : AbstractSpell
{
    [SerializeField] private int chance;
    [SerializeField] private GameObject resurect;
    [SerializeField] private AudioClip swish;
    void Start()
    {
        chance += fromUnit.grade;
        if (transform.parent.gameObject.name == "Debuffs") Turns.tryDeath += CastDebuff;
        if (PlayerData.language == 0)
        {
            nameText = "Resurrection";
            SType = "Passive";
            description = $"After the death of an ally, the Necromancer has a chance to resurrect him with 1 unit. health, while all effects on it are preserved.\r\nChance: {chance}%";
        }
        else
        {
            nameText = "Воскрешение";
            SType = "Пассивная";
            description = $"После смерти союзника, Некромант имеет шанс воскресить его с 1 ед. здоровья, при этом все эффекты на нем сохраняются.\r\nШанс: {chance}%";
        }
    }
    private void CastDebuff(UnitProperties Victim, Dictionary<string, int> inpData)
    {
        if (inpData.ContainsKey("sideFrom") &&
            inpData["sideFrom"] == parentUnit.sideOnMap &&
            inpData["placeFrom"] == parentUnit.placeOnMap &&
            inpData["debuffId"] == id)
        {
            UnitProperties unit = Turns.circlesMap[inpData["side"], inpData["place"]].newObject;
            unit.resurect = true;
        }
    }
    public override IEnumerator AfterStep(Dictionary<string, int> inpData)
    {
        int rand = Random.Range(0, 3);
        if (rand != 2) BattleSound.sound.PlayOneShot(voiceAfter[rand]);
        UnitProperties unit = Turns.circlesMap[inpData["sideTarget"], inpData["placeTarget"]].newObject;
        parentUnit.pathAnimation.SetCaracterState("passive");
        yield return new WaitForSeconds(0.2f);
        BattleSound.sound.PlayOneShot(swish);
        yield return new WaitForSeconds(0.1f);
        Instantiate(resurect, unit.pathBulletTarget.position, Quaternion.identity);
        resurect.transform.localScale = Vector3.one;
        yield return new WaitForSeconds(0.5f);
        unit.hp = 1;
        unit.HpDamage("hp");
        unit.pathAnimation.SetCaracterState("idle");
        unit.transform.parent.gameObject.transform.Find("Canvas").gameObject.GetComponent<Animator>().SetTrigger("start");
        yield return new WaitForSeconds(0.5f);
        unit.resurect = false;
        yield return new WaitForSeconds(0.5f);
        Turns.finishEndEvent = true;
    }
    public override void EndDebuff()
    {
        Turns.tryDeath -= CastDebuff;
    }
}
