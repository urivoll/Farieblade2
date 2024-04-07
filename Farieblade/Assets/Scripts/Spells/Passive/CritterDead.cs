using Spine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CritterDead : AbstractSpell
{
    public float Value = 0.1f;
    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioClip swish;
    private void Start()
    {
        Value += fromUnit.grade * 0.01f;
        if (PlayerData.language == 0)
        {
            nameText = "Darkening of the flesh";
            SType = "Passive";
            description = $"After the death of someone on the battlefield, the hands and flesh begin to darken and become callous. Critter gets a boost.\r\nWhen you kill an enemy, damage increases by {Convert.ToInt32(Value * 100)}%.\r\nWhen an ally is killed, health increases by {Convert.ToInt32(Value * 100)}%";
        }
        else
        {
            nameText = "Потемнение плоти";
            SType = "Пассивная";
            description = $"После смерти кого-либо на поле стражения, руки и плоть начинает темнеть и черстветь. Зубастик получает усиление.\r\nПри сметри врага - усиление урона на {Convert.ToInt32(Value * 100)}%.\r\nПри сметри союзника - увеличение здоровья на {Convert.ToInt32(Value * 100)}%";
        }
    }
    public override IEnumerator AfterStep(Dictionary<string, int> inpData)
    {
        if(parentUnit.pathCircle.newObject != null)
        {
            parentUnit.pathAnimation.SetCaracterState("passive");
            BattleSound.sound.PlayOneShot(swish);
            yield return new WaitForSeconds(0.3f);
            if (inpData["catch"] == 0)
            {
                parentUnit.hp = inpData["value"];
                parentUnit.HpDamage("hp");
                parentUnit.transform.Find("Body").gameObject.SetActive(true);
            }
            else
            {
                parentUnit.damage = inpData["value"];
                parentUnit.HpDamage("dmg");
                parentUnit.transform.Find("Hands").gameObject.SetActive(true);
            }
            BattleSound.sound.PlayOneShot(clip);
            yield return new WaitForSeconds(0.5f);
        }
        Turns.finishEndEvent = true;
    }
}
