using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ZombieResurrection : AbstractSpell
{
    public int Value = 55;
    private void Start()
    {
        Value += fromUnit.grade * 2;
        if (transform.parent.gameObject.name == "Debuffs")
        {
            Turns.tryDeath += Resurect;
        }
        if (PlayerData.language == 0)
        {
            nameText = "Resurrection";
            SType = "Passive";
            description = $"After falling, a zombie has a chance to resurrect with 1 health. All spells that were with him remain.\r\nChance: {Value}%";
        }
        else
        {
            nameText = "Воскрешение";
            SType = "Пассивная";
            description = $"После падения, зомби имеет шанс воскреснуть с 1 ед. здоровья. Все заклинания которые были при нем остаются.\r\nШанс: {Value}%";
        }
    }
    private void Resurect(UnitProperties victim, Dictionary<string, int> inpData)
    {
        if (inpData["sideFrom"] == parentUnit.ParentCircle.Side &&
            inpData["placeFrom"] == parentUnit.ParentCircle.Place &&
            inpData["debuffId"] == id)
        {
            parentUnit.HpCharacter.resurect = true;
        }
    }
    public override void EndDebuff()
    {
        Turns.tryDeath -= Resurect;
    }
    public override IEnumerator AfterStep(Dictionary<string, int> inpData)
    {
        int rand = Random.Range(0, 3);
        if (rand != 2) BattleSound.sound.PlayOneShot(voiceAfter[rand]);
        parentUnit.DebuffList.Remove(gameObject);
        parentUnit.transform.Find("ZombieResurect").gameObject.SetActive(true);
        parentUnit.tag = "Unit";
        //parentUnit.HpCharacter.hp = 1;
        parentUnit.HpCharacter.HpDamage("hp");
        parentUnit.Animation.TryGetAnimation("passive");
        parentUnit.transform.parent.gameObject.transform.Find("Canvas").gameObject.GetComponent<Animator>().SetTrigger("start");
        yield return new WaitForSeconds(1.83f);
        parentUnit.Animation.TryGetAnimation("idle");
        parentUnit.transform.Find("ZombieResurect").gameObject.SetActive(false);
        parentUnit.HpCharacter.resurect = false;
        yield return new WaitForSeconds(0.5f);
        Turns.finishEndEvent = true;
        Destroy(gameObject);
    }
}
