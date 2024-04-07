using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AuraLightWizard : Aura
{
    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioClip clip2;
    [SerializeField] private GameObject effect;
    public override IEnumerator GetAura(Dictionary<string, int> inpData)
    {
        parentUnit.pathAnimation.SetCaracterState("aura");
        BattleSound.sound.PlayOneShot(clip);
        yield return new WaitForSeconds(0.3f);
        BattleSound.sound.PlayOneShot(clip2);
        for (int i = 0; i < inpData["count"]; i++)
        {
            Instantiate(effect, Turns.circlesMap[inpData["side"], inpData[$"place{i}"]].newObject.pathBulletTarget.position, Quaternion.identity);
            Turns.circlesMap[inpData["side"], inpData[$"place{i}"]].newObject.pathEnergy.SetEnergy(inpData[$"energy{i}"]);
        }
        yield return new WaitForSeconds(0.3f);
        Turns.finishEndEvent = true;
    }
}
