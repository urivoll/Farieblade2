using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AuraFlanceShaman : Aura
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
            UnitProperties unit = _characterPlacement.CirclesMap[inpData["side"], inpData[$"place{i}"]].ChildCharacter;
            GameObject debuff = Instantiate(parentUnit.Spells.SpellList[1], unit.PathDebuffs);
            ///debuff.GetComponent<AbstractSpell>().fromUnit = parentUnit.pathParent;
            ///unit.HpCharacter.damage = inpData[$"damage{i}"];
            unit.HpCharacter.HpDamage("dmg");
        }
        yield return new WaitForSeconds(0.3f);
        Turns.finishEndEvent = true;
    }
}
