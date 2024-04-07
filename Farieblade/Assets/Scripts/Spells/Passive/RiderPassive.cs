using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class RiderPassive : AbstractSpell
{
    [SerializeField] private TextMeshProUGUI textStuck;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject debuff;
    [SerializeField] private AudioClip swish;
    [SerializeField] private AudioClip clip;
    public int stuck = 0;
    void Start()
    {
        if (transform.parent.gameObject.name == "Debuffs")
        {
            Turns.getEnergy += Cast;
        }
        if (PlayerData.language == 0)
        {
            nameText = "Holy energy";
            SType = "Passive";
            description = $"After each energy gain, the Knight accumulates charges. Every 6 charges the Knight gives a random ally a divine shield.";
        }
        else
        {
            nameText = "Святая энергия";
            SType = "Пассивная";
            description = $"После каждого получения энергии у Рыцаря накапливаются заряды. Каждый 6 заряд Рыцарь дает случайному союзнику божественный щит.";
        }
    }
    public void Cast(int how, GameObject unitTarget, char sign)
    {
        if (unitTarget != fromUnit.gameObject || sign == '-') return;
        stuck += how;
        if (stuck == 0) textStuck.text = " ";
        else if (stuck == 6)
        {
            stuck = 0;
            textStuck.text = " ";
        }
        else
        {
            textStuck.text = Convert.ToString(stuck);
            animator.SetTrigger("on");
        }
    }
    public override IEnumerator AfterStep(Dictionary<string, int> inpData)
    {
        parentUnit.pathAnimation.SetCaracterState("spell");
        yield return new WaitForSeconds(0.2f);
        BattleSound.sound.PlayOneShot(swish);
        yield return new WaitForSeconds(0.1f);
        GameObject newObj = Instantiate(debuff, Turns.circlesMap[inpData["sideTarget"], inpData["placeTarget"]].newObject.pathDebuffs);
        newObj.GetComponent<AbstractSpell>().fromUnit = fromUnit;
        BattleSound.sound.PlayOneShot(clip);
        yield return new WaitForSeconds(0.3f);
        Turns.finishEndEvent = true;
    }
    public override void EndDebuff()
    {
        base.EndDebuff();
        Turns.getEnergy -= Cast;
    }
}
