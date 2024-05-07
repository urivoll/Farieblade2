using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class CerberusDamage : AbstractSpell
{
    [SerializeField] private GameObject Effect2;
    [SerializeField] private TextMeshProUGUI textStuck;
    [SerializeField] private Animator animator;

    public override IEnumerator AfterStep(Dictionary<string, int> inpData)
    {
        BattleSound.sound.PlayOneShot(soundMid);
        parentUnit.Animation.TryGetAnimation("passive");
        yield return new WaitForSeconds(0.3f);
        //parentUnit.Weapon.damage = inpData["damagePlus"];
        Instantiate(Effect2, parentUnit.PathBulletTarget.position, Quaternion.identity);
        textStuck.text = Convert.ToString(inpData["stuck"]);
        animator.SetTrigger("on");
        parentUnit.HpCharacter.HpDamage("dmg");
        yield return new WaitForSeconds(0.6f);
        Turns.finishEndEvent = true;
    }
}
