using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class DuelistPassive : AbstractSpell
{
    private float value;
    private float value2;
    private int chance = 35;
    [SerializeField] private AudioClip Strike;
    [SerializeField] private AudioClip Swish;
    [SerializeField] private AudioClip SwishBefore;
    [SerializeField] private TextMeshProUGUI textStuck;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioClip Up;
    private int stuck = 0;
    void Start()
    {
        chance += fromUnit.grade;
        value = prosentDamage + fromUnit.grade * 0.1f;
        value2 = 0.2f + fromUnit.grade * 0.1f;
        if (transform.parent.gameObject.name == "Debuffs")
        {
            Turns.punch += CastDebuff;
        }
        if (PlayerData.language == 0)
        {
            nameText = "Counterstrike";
            SType = "Passive";
            description = $"Akara has a {chance}% chance to block a melee enemy attack and counterattack, dealing {Convert.ToInt32(value * 100)}% of the damage. Every 4th attack blocked increases Akara's damage by {Convert.ToInt32(value2 * 100)}%";
        }
        else
        {
            nameText = "Контрудар";
            SType = "Пассивная";
            description = $"Акара имеет {chance}% шанс заблокировать атаку противника ближнего боя и провести контроудар нанося {Convert.ToInt32(value * 100)}% от урона. Каждая 4 заблокированная атака увеличивает урон Акары на {Convert.ToInt32(value2 * 100)}%";
        }
    }

    private void CastDebuff(UnitProperties targetUnit, UnitProperties fromUnit, List<Dictionary<string, int>> inpData)
    {
        for(int i = 0; i < inpData.Count; i++)
        {
            if (inpData[i]["side"] == parentUnit.sideOnMap &&
                inpData[i]["place"] == parentUnit.placeOnMap &&
                inpData[i]["debuffId"] == id)
            {
                BattleSound.sound.Stop();
                parentUnit.inpDamage = 0;
                BattleSound.sound.PlayOneShot(soundBefore);
                BattleSound.sound.PlayOneShot(soundMid);
                Block();
                parentUnit.transform.Find("BlockSparks").gameObject.SetActive(true);
                stuck = inpData[i]["stuck"];
                animator.SetTrigger("on");
                if (stuck == 0)
                {
                    textStuck.text = " ";
                    parentUnit.transform.Find("SwordsUp").gameObject.SetActive(true);
                    parentUnit.damage = inpData[i]["damageParent"];
                    parentUnit.HpDamage("dmg");
                    BattleSound.sound.PlayOneShot(Up);
                }
                else textStuck.text = Convert.ToString(stuck);
            }
        }
    }
    public override IEnumerator AfterStep(Dictionary<string, int> inpData)
    {

        parentUnit.pathAnimation.SetCaracterState("passive");
        BattleSound.sound.PlayOneShot(SwishBefore);
        parentUnit.transform.Find("AttackSwish").gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        BattleSound.sound.PlayOneShot(Swish);
        parentUnit.transform.Find("AttackSwish").gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        BattleSound.sound.PlayOneShot(Strike);
        Turns.circlesMap[inpData["sideTarget"], inpData["placeTarget"]].newObject.SpellDamage(inpData["damage"], 4);
        yield return new WaitForSeconds(0.3f);
        Turns.finishEndEvent = true;
    }
    private void Block()
    {
        if (parentUnit.hp > 0)
            parentUnit.pathAnimation.SetCaracterState("mode");
    }
    public override void EndDebuff()
    {
        Turns.punch -= CastDebuff;
    }
}
