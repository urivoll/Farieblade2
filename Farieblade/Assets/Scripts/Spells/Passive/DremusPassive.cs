using Spine;
using System;
using System.Collections;
using UnityEngine;

public class DremusPassive : AbstractSpell
{
    public float Value = 0.2f;
    public int chance = 30;
    private string victim;
    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioClip swish;
/*    private void Start()
    {
        Value += fromUnit.grade * 0.01f;
        chance += fromUnit.grade;
        if (transform.parent.gameObject.name == "Debuffs")
        {
            Over += Over2;
            Turns.TryDeath += Dead;
            EndEventDoing += Do;
        }
        if (PlayerData.Language == 0)
        {
            Name = "Darkening of the flesh";
            SType = "Passive";
            Description = $"After the death of someone on the battlefield, the hands and flesh begin to darken and become callous. Critter gets a boost.\r\nWhen you kill an enemy, damage increases by {Convert.ToInt32(Value * 100)}%.\r\nWhen an ally is killed, health increases by {Convert.ToInt32(Value * 100)}%";
        }
        else
        {
            Name = "Потемнение плоти";
            SType = "Пассивная";
            Description = $"После смерти кого-либо на поле стражения, руки и плоть начинает темнеть и черстветь. Зубастик получает усиление.\r\nПри сметри врага - усиление урона на {Convert.ToInt32(Value * 100)}%.\r\nПри сметри союзника - увеличение здоровья на {Convert.ToInt32(Value * 100)}%";
        }
    }
    private void Dead(UnitProperties Victim)
    {
        if(Victim == ParentObject)
        {
            Turns.EventEndCard.Add(gameObject);
        }
    }
    private void Over2()
    {
        Over -= Over2;
        Turns.TryDeath -= Dead;
        EndEventDoing -= Do;
    }*/
/*    private void Do()
    {
        StartCoroutine(Doing());
    }*/
/*    public IEnumerator Doing()
    {
        if(ParentObject.tag != "Respawn")
        {
            ParentObject.GetComponent<UnitProperties>()._unitAnimation.SetCaracterState("passive");
            ParentObject.transform.parent.gameObject.GetComponent<AudioSource>().PlayOneShot(swish);
            yield return new WaitForSeconds(0.3f);
            if (victim == ParentObject.transform.parent.transform.parent.transform.parent.gameObject.GetComponent<CircleProperties>().whoseCircle)
            {
                ParentObject.GetComponent<UnitProperties>().hp += Convert.ToInt32(ParentObject.GetComponent<UnitProperties>().hp * Value);
                ParentObject.GetComponent<UnitProperties>().HpDamage("hp");
                ParentObject.transform.Find("Body").gameObject.SetActive(true);
            }
            else
            {
                ParentObject.GetComponent<UnitProperties>().damage += Convert.ToInt32(ParentObject.GetComponent<UnitProperties>().damage * Value);
                ParentObject.GetComponent<UnitProperties>().HpDamage("dmg");
                ParentObject.transform.Find("Hands").gameObject.SetActive(true);
            }
            ParentObject.transform.parent.gameObject.GetComponent<AudioSource>().PlayOneShot(clip);
            yield return new WaitForSeconds(0.5f);
        }
        Turns.FinishEndEvent = true;
    }*/
}
