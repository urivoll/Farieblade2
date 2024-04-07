using System;
using System.Collections;
using UnityEngine;

public class AuraFatMan : Spells
{
    [SerializeField] private AudioClip clip;
    private float Value;
    [SerializeField] private GameObject EffectAura;
    public void SetAura(int index)
    {
        StartCoroutine(SetAura2(index));
    }
    public IEnumerator SetAura2(int index)
    {
        Value = 0.2f + (transform.parent.transform.parent.GetComponent<Unit>().grade * 0.01f);
        gameObject.GetComponent<UnitProperties>().pathAnimation.SetCaracterState("aura");
        yield return new WaitForSeconds(0.2f);
        BattleSound.sound.PlayOneShot(clip);
        yield return new WaitForSeconds(0.1f);
        bool have = false;
        for (int i = 0; i < Turns.listUnitAll.Count; i++)
        {
            if (Turns.listUnitAll[i].sideOnMap == GetComponent<UnitProperties>().sideOnMap &&
                Turns.listUnitAll[i].pathParent.fraction == 8 && Turns.listUnitAll[i] != GetComponent<UnitProperties>())
            {
                have = true;
                GetComponent<UnitProperties>().damage += Convert.ToInt32(GetComponent<UnitProperties>().damage * Value);
            }
        }
        if(have == true)
        {
            Instantiate(EffectAura, gameObject.transform.Find("BulletTarget").gameObject.transform.position, Quaternion.identity);
            GetComponent<UnitProperties>().HpDamage("dmg");
        }
        yield return new WaitForSeconds(0.3f);
        Turns.finishEndEvent = true;
    }
}
