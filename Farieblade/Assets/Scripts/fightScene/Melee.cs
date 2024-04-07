using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Melee : MonoBehaviour
{
    [SerializeField] private float timeBeforeSwish;
    [SerializeField] private float timeBeforeHit;
    [SerializeField] private AudioClip soundBeforeHit;
    [SerializeField] private int weaponIndex;

    [SerializeField] private GameObject effect = null;
    [SerializeField] private GameObject[] swishEffect;
    public IEnumerator Punch(UnitProperties unitForHit, UnitProperties from, List<MakeMove> inpData)
    {
        int times = from.times;
        float behiendTimes = from.behiendTimes;

        if (soundBeforeHit != null) BattleSound.sound.PlayOneShot(soundBeforeHit);
        int count = 0;
        yield return new WaitForSeconds(timeBeforeSwish);

        while (count != times)
        {
            //Звук голоса при ударе
            StartIni.soundVoice.StrikeVoices(from.indexVoice);
            //Звук Махов оружия
            BattleSound.sound.PlayOneShot(BattleSound.swishClip[weaponIndex]);
            if (swishEffect.Length > 0) swishEffect[count].SetActive(true);

            yield return new WaitForSeconds(timeBeforeHit);
            if (inpData[count].attackSend["catch"] == 1)
            {
                BattleSound.sound.PlayOneShot(BattleSound.weaponClip[weaponIndex]);
                if (from.pathParent.Type == 3) StartIni.animatorShakeStatic.SetTrigger("shakeShort");
                unitForHit.TakeDamage(from, inpData[count]);
                //Если есть эффект
                if (effect != null) Instantiate(effect, unitForHit.pathBulletTarget.position, Quaternion.identity);
            }
            else if (unitForHit != null) unitForHit.Miss();
            count++;
            if (times > 1) yield return new WaitForSeconds(behiendTimes);
        }
        yield return new WaitForSeconds(0.5f);
        Turns.hitDone = true;
    }
}