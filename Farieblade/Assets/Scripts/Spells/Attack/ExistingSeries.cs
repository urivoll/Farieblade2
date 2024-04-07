using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ExistingSeries : AbstractSpell
{
    private float withProsent;
    [SerializeField] private AudioClip clipLanch;
    [SerializeField] private AudioClip clipSwish;
    [SerializeField] private AudioClip Strike1;
    [SerializeField] private AudioClip Strike2;
    [SerializeField] private AudioClip Strike3;
    private List<UnitProperties> allow;
    void Start()
    {
        withProsent = fromUnit.damage * prosentDamage;
        if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Series of fiery flashes";
                SType = "Ranged ability";
                description = $"The Entity begins to randomly cause bursts of fire, damaging random enemies.\r\nEnergy required: 3\r\nDamage per flash: {Convert.ToInt32(withProsent)}";
            }
            else
            {
                nameText = "Серия огненных вспышек";
                SType = "Способность дальней дистанции";
                description = $"Сущий начинает беспорядочно вызывать огненные вспышки, нанося урон случайным противникам.\r\nНеобходимая энергия: 3\r\nУрон за вспышку: {Convert.ToInt32(withProsent)} ед.";
            }
        }
    }
    public override IEnumerator HitEffect(Dictionary<string, int> inpData)
    {
        allow = new();
        allow.AddRange(Turns.listUnitEnemy);
        fromUnit.Model.transform.Find("Hands").gameObject.SetActive(true);
        for (int i = 0; i < inpData["count"]; i++)
        {
            yield return new WaitForSeconds(0.7f);
            StartIni.soundVoice.StrikeVoices(fromUnit.Model.indexVoice);
            BattleSound.sound.PlayOneShot(clipSwish);
            BattleSound.sound.PlayOneShot(clipLanch);
            yield return new WaitForSeconds(0.05f);
            UnitProperties unit = Turns.circlesMap[inpData["sideOnMap"], inpData[$"placeOnMap{i}"]].newObject;
            Transform bulletTarget = unit.pathBulletTarget;
            GameObject newBullet = Instantiate(Effect, fromUnit.Model.transform.Find("bullet").position, Quaternion.identity);
            var direction = bulletTarget.transform.position - newBullet.transform.position;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Bullet bullet = newBullet.GetComponent<Bullet>();
            newBullet.transform.rotation = Quaternion.Euler(0, 0, angle);
            bullet.unitTarget = unit;
            bullet.damage = inpData[$"damage{i}"];
            bullet.element = 5;
            bullet.unitFrom = fromUnit.Model;
        }
        yield return new WaitForSeconds(1f);
        Turns.hitDone = true;
    }
}
