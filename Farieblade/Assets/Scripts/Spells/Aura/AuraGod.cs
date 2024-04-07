using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AuraGod : Aura
{
    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioClip auraLanch;
    [SerializeField] private AudioClip fall;
    [SerializeField] private GameObject wolf;
    [SerializeField] private GameObject Effect;
    [SerializeField] private GameObject soul;
    public override IEnumerator GetAura(Dictionary<string, int> inpData)
    {
        if (inpData.ContainsKey("placeTarget"))
        {
            parentUnit.pathAnimation.SetCaracterState("aura");
            yield return new WaitForSeconds(0.2f);
            BattleSound.sound.PlayOneShot(clip);
            yield return new WaitForSeconds(0.1f);
            BattleSound.sound.PlayOneShot(auraLanch);
            yield return new WaitForSeconds(0.5f);
            UnitProperties newObject = Turns.circlesMap[inpData["side"], inpData["placeTarget"]].CreateUnit(wolf);
            newObject.pathParent.level = inpData["level"];
            newObject.pathParent.grade = inpData["grade"];
            newObject.pathParent.SetValues();
            newObject.Instantiate();
            yield return new WaitForSeconds(0.01f);
            newObject.pathAnimation.SetCaracterState("aura");
            yield return new WaitForSeconds(0.2f);
            Instantiate(Effect, newObject.transform.Find("BulletTarget").position, Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
            BattleSound.sound.PlayOneShot(fall);
            yield return new WaitForSeconds(0.3f);
        }
        Turns.finishEndEvent = true;
    }
}
