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
            inpData["sideFrom"] == parentUnit.ParentCircle.Side &&
            inpData["placeFrom"] == parentUnit.ParentCircle.Place &&
            inpData["debuffId"] == id)
        {
            UnitProperties unit = _characterPlacement.CirclesMap[inpData["side"], inpData["place"]].ChildCharacter;
            unit.HpCharacter.resurect = true;
        }
    }
    public override IEnumerator AfterStep(Dictionary<string, int> inpData)
    {
        int rand = Random.Range(0, 3);
        if (rand != 2) BattleSound.sound.PlayOneShot(voiceAfter[rand]);
        UnitProperties unit = _characterPlacement.CirclesMap[inpData["sideTarget"], inpData["placeTarget"]].ChildCharacter;
        parentUnit.Animation.TryGetAnimation("passive");
        yield return new WaitForSeconds(0.2f);
        BattleSound.sound.PlayOneShot(swish);
        yield return new WaitForSeconds(0.1f);
        Instantiate(resurect, unit.PathBulletTarget.position, Quaternion.identity);
        resurect.transform.localScale = Vector3.one;
        yield return new WaitForSeconds(0.5f);
        //unit.HpCharacter.hp = 1;
        unit.HpCharacter.HpDamage("hp");
        unit.Animation.TryGetAnimation("idle");
        unit.transform.parent.gameObject.transform.Find("Canvas").gameObject.GetComponent<Animator>().SetTrigger("start");
        yield return new WaitForSeconds(0.5f);
        unit.HpCharacter.resurect = false;
        yield return new WaitForSeconds(0.5f);
        Turns.finishEndEvent = true;
    }
    public override void EndDebuff()
    {
        Turns.tryDeath -= CastDebuff;
    }
}
