using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PoisonFlanceCloud : AbstractSpell
{
    [SerializeField] private GameObject debuff;
    [SerializeField] private GameObject Effect2;
    [SerializeField] private AudioClip clipFall;
    [SerializeField] private AudioClip clipSwish;
    [SerializeField] private AudioClip strike1;
    [SerializeField] private AudioClip strike2;
    [SerializeField] private AudioClip strike3;
    void Start()
    {
        if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Poison Cloud";
                SType = "Debuff";
                description = $"While jumping, Poison Flenz accumulates all of its poison and releases it throughout the battlefield as it falls. All enemies receive poison.\r\nEnergy required: 3";
            }
            else
            {
                nameText = "Ядовитое облако";
                SType = "Проклятье";
                description = $"Во время прыжка Ядовитый Фленц накапливает весь свой яд и во время падения испускает его на все поле битвы. Все противники получают яд.\r\nНеобходимая энергия: 3";
            }
        }
    }
    public override IEnumerator HitEffect(Dictionary<string, int> inpData)
    {
        yield return new WaitForSeconds(0.2f);
        StartIni.soundVoice.StrikeVoices(fromUnit.Model.indexVoice);
        BattleSound.sound.PlayOneShot(clipSwish);
        yield return new WaitForSeconds(0.75f);
        StartIni.animatorShakeStatic.SetTrigger("shake");
        Instantiate(Effect2, fromUnit.Model.transform.Find("BulletTarget").position, Quaternion.identity, Turns.circlesTransform.transform);
        BattleSound.sound.PlayOneShot(clipFall);
        for (int i = 0; i < inpData["count"]; i++)
        {
            GameObject newObject = Instantiate(debuff, Turns.circlesMap[inpData["sideOnMap"], inpData[$"placeOnMap{i}"]].newObject.pathDebuffs);
            newObject.GetComponent<AbstractSpell>().fromUnit = fromUnit;
        }
        yield return new WaitForSeconds(0.3f);
        Turns.hitDone = true;
    }
}
