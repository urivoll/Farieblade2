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
            nameText = "���������";
            SType = "���������";
            description = $"����� ����� {chance}% ���� ������������� ����� ���������� �������� ��� � �������� ���������� ������ {Convert.ToInt32(value * 100)}% �� �����. ������ 4 ��������������� ����� ����������� ���� ����� �� {Convert.ToInt32(value2 * 100)}%";
        }
    }

    private void CastDebuff(UnitProperties targetUnit, UnitProperties fromUnit, List<Dictionary<string, int>> inpData)
    {
        for(int i = 0; i < inpData.Count; i++)
        {
            if (inpData[i]["side"] == parentUnit.ParentCircle.Side &&
                inpData[i]["place"] == parentUnit.ParentCircle.Place &&
                inpData[i]["debuffId"] == id)
            {
                BattleSound.sound.Stop();
                //parentUnit.HpCharacter.inpDamage = 0;
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
                    //parentUnit.HpCharacter.damage = inpData[i]["damageParent"];
                    parentUnit.HpCharacter.HpDamage("dmg");
                    BattleSound.sound.PlayOneShot(Up);
                }
                else textStuck.text = Convert.ToString(stuck);
            }
        }
    }
    public override IEnumerator AfterStep(Dictionary<string, int> inpData)
    {

        parentUnit.Animation.TryGetAnimation("passive");
        BattleSound.sound.PlayOneShot(SwishBefore);
        parentUnit.transform.Find("AttackSwish").gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        BattleSound.sound.PlayOneShot(Swish);
        parentUnit.transform.Find("AttackSwish").gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        BattleSound.sound.PlayOneShot(Strike);
        //_characterPlacement.CirclesMap[inpData["sideTarget"], inpData["placeTarget"]].newObject.SpellDamage(inpData["damage"], 4);
        yield return new WaitForSeconds(0.3f);
        Turns.finishEndEvent = true;
    }
    private void Block()
    {
        if (parentUnit.HpCharacter.Hp > 0)
            parentUnit.Animation.TryGetAnimation("mode");
    }
    public override void EndDebuff()
    {
        Turns.punch -= CastDebuff;
    }
}
