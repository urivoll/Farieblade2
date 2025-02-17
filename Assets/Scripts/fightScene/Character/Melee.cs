using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Weapon
{
    [SerializeField] private int weaponIndex;
    public override IEnumerator Attack(UnitProperties from, List<MakeMove> inpData)
    {
        UnitProperties unitForHit = _characterPlacement.CirclesMap[inpData[0].attackSend["sideTarget"], inpData[0].attackSend["placeTarget"]].ChildCharacter;
        int times = from.Weapon.Times;

        if (_soundBeforeHit != null) BattleSound.sound.PlayOneShot(_soundBeforeHit);
        int count = 0;
        yield return new WaitForSeconds(_timeBeforeHit);

        while (count != times)
        {
            StartIni.soundVoice.StrikeVoices(from.indexVoice);
            BattleSound.sound.PlayOneShot(BattleSound.swishClip[weaponIndex]);

            yield return new WaitForSeconds(0.1f);
            if (inpData[count].attackSend["catch"] == 1)
            {
                BattleSound.sound.PlayOneShot(BattleSound.weaponClip[weaponIndex]);
                unitForHit.HpCharacter.TakeDamage(from, inpData[count]);
            }
            //else if (unitForHit != null) unitForHit.HpCharacter.Miss();
            count++;
            if (times > 1) yield return new WaitForSeconds(_behiendTimes);
        }
        yield return new WaitForSeconds(0.5f);
        Turns.hitDone = true;
    }
}