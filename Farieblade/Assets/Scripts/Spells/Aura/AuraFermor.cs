using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AuraFermor : Aura
{
    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioClip auraLanch;
    [SerializeField] private AudioClip fall;
    [SerializeField] private GameObject demon;
    [SerializeField] private GameObject Effect;
    [SerializeField] private GameObject soul;
    public override IEnumerator GetAura(Dictionary<string, int> inpData)
    {
        if (inpData.ContainsKey("placeTarget"))
        {
            parentUnit.pathAnimation.SetCaracterState("aura");
            BattleSound.sound.PlayOneShot(auraLanch);
            yield return new WaitForSeconds(0.5f);
            BattleSound.sound.PlayOneShot(clip);
            yield return new WaitForSeconds(0.3f);
            UnitProperties newObject = Turns.circlesMap[inpData["side"], inpData["placeTarget"]].CreateUnit(demon);
            newObject.pathParent.level = inpData["level"];
            newObject.pathParent.grade = inpData["grade"];
            newObject.pathParent.SetValues();
            Instantiate(Effect, newObject.pathBulletTarget.position, Quaternion.identity);
            newObject.Instantiate();
            yield return new WaitForSeconds(0.02f);
            newObject.pathAnimation.SetCaracterState("aura");
            yield return new WaitForSeconds(0.2f);
            BattleSound.sound.PlayOneShot(fall);
            yield return new WaitForSeconds(0.3f);
        }
        Turns.finishEndEvent = true;
    }
}
