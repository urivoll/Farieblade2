using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AuraCocaroach : Aura
{
    [SerializeField] private AudioClip clip;
    public override IEnumerator GetAura(Dictionary<string, int> inpData)
    {
        parentUnit.Animation.TryGetAnimation("passive");
        yield return new WaitForSeconds(0.2f);
        BattleSound.sound.PlayOneShot(clip);
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < inpData["count"]; i++)
        {
            GameObject debuff = Instantiate(parentUnit.Spells.SpellList[1], _characterPlacement.CirclesMap[inpData["side"], inpData[$"place{i}"]].ChildCharacter.PathDebuffs);
            debuff.GetComponent<AbstractSpell>().fromUnit = parentUnit.pathParent;
        }
        yield return new WaitForSeconds(0.3f);
        Turns.finishEndEvent = true;
    }
}