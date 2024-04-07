using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class GreoCriticalDamage : AbstractSpell
{
    public int chance = 30;
    public float value = 0;
    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioClip clip2;
    [SerializeField] private TextMeshProUGUI textStuck;
    [SerializeField] private Animator animator;
    private int stucks = 0;
    void Start()
    {
        chance += fromUnit.grade;
        if (transform.parent.gameObject.name == "Debuffs")
        {
            Turns.shooterPunch += CastDebuff;
        }
        if (PlayerData.language == 0)
        {
            nameText = "Critical hit";
            SType = "Passive";
            description = $"Greo's arrow is endowed with sacred power that can cause critical damage to an enemy with a 30 percent chance. Each critical hit increases its chance to trigger by 5% and its damage by 20%.";
        }
        else
        {
            nameText = " ритический удар";
            SType = "ѕассивна€";
            description = $"—трела √рео надел€етс€ св€щенной силой, котора€ способна нанести критический урон по врагу с шансом в 30%.  аждый критический удар повышает шанс его срабатывани€ на 5% и его урон на 20%.";
        }
    }
    private void CastDebuff(UnitProperties victim, UnitProperties from, List<Dictionary<string, int>> inpData)
    {
        for (int i = 0; i < inpData.Count; i++)
        {
            if (inpData[i]["side"] == parentUnit.sideOnMap &&
                inpData[i]["place"] == parentUnit.placeOnMap &&
                inpData[i]["debuffId"] == id)
            {
                Bullet bullet = from.GetComponent<Shooter>().newBullet;
                bullet.end = bullet.transform.Find("Exp").gameObject;
            }
        }
    }
    public override IEnumerator AfterStep(Dictionary<string, int> inpData)
    {
        UnitProperties unit = Turns.circlesMap[inpData["side"], inpData["place"]].newObject;
        unit.pathAnimation.SetCaracterState("passive");
        yield return new WaitForSeconds(0.3f);
        BattleSound.sound.PlayOneShot(clip);
        yield return new WaitForSeconds(0.1f);
        unit.transform.Find("up").gameObject.SetActive(true);
        BattleSound.sound.PlayOneShot(clip2);
        stucks += 1;
        textStuck.text = Convert.ToString(stucks);
        animator.SetTrigger("on");
        yield return new WaitForSeconds(0.5f);
        Turns.finishEndEvent = true;
    }
    public override void EndDebuff()
    {
        Turns.shooterPunch -= CastDebuff;
    }
}
