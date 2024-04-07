using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DemonMelee2 : AbstractSpell
{
    [SerializeField] private AudioClip strike;
    [SerializeField] private GameObject soul;
    private float withProsent;
    private float value;
    void Start()
    {
        withProsent = fromUnit.damage * prosentDamage;
        value = 0.2f + fromUnit.grade * 0.01f;
        if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Direct hit";
                SType = "Melee ability";
                description = $"The demon takes power from its allies, dealing 1 damage to them, then makes a direct strike, hitting the rear creature.\r\nEnergy required: 3\r\nDamage: +{Convert.ToInt32(value * 100)}% damage from each ally";
            }
            else
            {
                nameText = "Пробивающий удар";
                SType = "Способность ближней дистанции";
                description = $"Демон забирает силу у своих союзников, нанося им 1 ед урона, затем делает прямой удар, задевая заднее существо.\r\nНеобходимая энергия: 3\r\nУрон: +{Convert.ToInt32(value * 100)}% урона с каждого союзника.";
            }
        }
    }
    public override IEnumerator HitEffect(Dictionary<string, int> inpData)
    {
        fromUnit.Model.transform.Find("UseSpell").gameObject.SetActive(true);
        BattleSound.sound.PlayOneShot(strike);
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < Turns.listUnitOur.Count; i++)
        {
            withProsent += withProsent * value;
            if (Turns.listUnitOur[i] != fromUnit.Model)
            {
                GameObject newObject = Instantiate(soul, Turns.listUnitOur[i].pathBulletTarget.position, Quaternion.identity);
                Turns.listUnitOur[i].SpellDamage(1, 5);
                newObject.transform.Find("DeadKingSoulTarget").gameObject.transform.position = fromUnit.Model.transform.Find("BulletTarget").position;
                yield return new WaitForSeconds(0.2f);
            }
        }
        yield return new WaitForSeconds(1 - Turns.listUnitOur.Count * 0.2f);

        yield return new WaitForSeconds(0.15f);
        BattleSound.sound.PlayOneShot(soundMid);
        fromUnit.Model.transform.Find("AttackSwish").gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        BattleSound.sound.PlayOneShot(soundAfter);
        Turns.circlesMap[inpData["sideOnMap"], inpData["placeOnMap"]].newObject.SpellDamage(inpData["damage"], 4);
        if(inpData["count"] == 2)
            Turns.circlesMap[inpData["sideOnMap"], inpData["placeOnMapBehind"]].newObject.SpellDamage(inpData["damageBehind"], 4);
        yield return new WaitForSeconds(0.4f);
        Turns.hitDone = true;
    }
}
