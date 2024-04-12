using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraFatMan : Aura
{
    [SerializeField] private AudioClip clip;
    private float Value;
    [SerializeField] private GameObject EffectAura;
    public override IEnumerator GetAura(Dictionary<string, int> inpData)
    {
        Value = 0.2f + (transform.parent.transform.parent.GetComponent<Unit>().grade * 0.01f);
        gameObject.GetComponent<UnitProperties>().pathAnimation.TryGetAnimation("passive");
        yield return new WaitForSeconds(0.2f);
        BattleSound.sound.PlayOneShot(clip);
        yield return new WaitForSeconds(0.1f);
        bool have = false;
        for (int i = 0; i < _characterPlacement.UnitAll.Count; i++)
        {
            if (_characterPlacement.UnitAll[i].Side == GetComponent<UnitProperties>().Side &&
                _characterPlacement.UnitAll[i].pathParent.fraction == 8 && _characterPlacement.UnitAll[i] != GetComponent<UnitProperties>())
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
