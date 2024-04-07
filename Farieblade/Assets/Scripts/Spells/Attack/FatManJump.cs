using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FatManJump : AbstractSpell
{
    private float withProsent;
    [SerializeField] private AudioClip fall;
    [SerializeField] private AudioClip swish;
    [SerializeField] private GameObject effectDirt;
    [SerializeField] private GameObject effectSnow;
    [SerializeField] private GameObject effectHell;
    [SerializeField] private GameObject effectUndead;
    [SerializeField] private GameObject debuff;
    void Start()
    {
        withProsent = fromUnit.damage * prosentDamage;
        if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Dust Jump";
                SType = "Ranged ability";
                description = $"After jumping, Fat Man raises a curtain of dust, temporarily blinding enemies and dealing {Convert.ToInt32(withProsent)} damage.\r\nEnergy required: 3";
            }
            else
            {
                nameText = "Пыльный прыжок";
                SType = "Способность дальней дистанции";
                description = $"После прыжка Толстяк поднимает пыльную завесу, временно ослепляя противников и нанося {Convert.ToInt32(withProsent)} ед. урона.\r\nНеобходимая энергия: 3";
            }
        }
    }
    public override IEnumerator HitEffect(Dictionary<string, int> inpData)
    {
        yield return new WaitForSeconds(0.3f);
        BattleSound.sound.PlayOneShot(swish);
        yield return new WaitForSeconds(timeBeforeShoot);
        BattleSound.sound.PlayOneShot(fall);
        StartIni.animatorShakeStatic.SetTrigger("shake");
        for (int i = 0; i < inpData["count"]; i++)
        {
            UnitProperties tempUnit = Turns.circlesMap[inpData["sideOnMap"], inpData[$"placeOnMap{i}"]].newObject;
            if (inpData[$"catch{i}"] == 1)
            {
                GameObject newObject = Instantiate(debuff, tempUnit.pathDebuffs);
                newObject.GetComponent<AbstractSpell>().fromUnit = fromUnit;
                tempUnit.SpellDamage(inpData[$"damage{i}"], 3);
            }
            else tempUnit.Miss();
        }
        if (Campany.battleField == 4)      Instantiate(effectSnow, fromUnit.transform.Find("Fight/Model").position, Quaternion.identity, Turns.circlesTransform.transform);
        else if (Campany.battleField == 2) Instantiate(effectUndead, fromUnit.transform.Find("Fight/Model").position, Quaternion.identity, Turns.circlesTransform.transform);
        else if (Campany.battleField == 3) Instantiate(effectHell, fromUnit.transform.Find("Fight/Model").position, Quaternion.identity, Turns.circlesTransform.transform);
        else                               Instantiate(effectDirt, fromUnit.transform.Find("Fight/Model").position, Quaternion.identity, Turns.circlesTransform.transform);
        yield return new WaitForSeconds(0.3f);
        Turns.hitDone = true;
    }
}
