using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class CerberusDamage : AbstractSpell
{
    [SerializeField] private GameObject Effect2;
    [SerializeField] private GameObject Effect3;
    [SerializeField] private TextMeshProUGUI textStuck;
    [SerializeField] private Animator animator;
    void Start()
    {
        if (PlayerData.language == 0)
        {
            nameText = "Hell howl";
            SType = "Passive";
            description = $"After receiving damage, Tsumil increases/decreases his attack depending on the damage received.\r\nAttack is 50% more damage received: +5% Attack.\r\nAttack is 100% greater than damage received: +10% Attack.\r\nAttack more than damage received by 200%: +15% Attack.\r\nAttack greater than damage received from 300% or more: +20% Attack.";
        }
        else
        {
            nameText = "Адский вой";
            SType = "Пассивная";
            description = $"После получение урона, Цумил увеличивает/уменьшает свою атаку в зависимости от полученого урона.\r\nАтака больше полученного урона на 50%: +5% Атаки.\r\nАтака больше полученного урона на 100%: +10% Атаки.\r\nАтака больше полученного урона на 200%: +15% Атаки.\r\nАтака больше полученного урона от 300% и более:  +20% Атаки.";
        }
    }
    public override IEnumerator AfterStep(Dictionary<string, int> inpData)
    {
        BattleSound.sound.PlayOneShot(soundMid);
        parentUnit.pathAnimation.SetCaracterState("passive");
        yield return new WaitForSeconds(0.3f);
        parentUnit.damage = inpData["damagePlus"];
        Instantiate(Effect2, parentUnit.pathBulletTarget.position, Quaternion.identity);
        textStuck.text = Convert.ToString(inpData["stuck"]);
        animator.SetTrigger("on");
        parentUnit.HpDamage("dmg");
        yield return new WaitForSeconds(0.6f);
        Turns.finishEndEvent = true;
    }
}
