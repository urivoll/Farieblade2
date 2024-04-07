using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class MoonMagePass : AbstractSpell
{
    private float Value;
    private int chance = 40;
    [SerializeField] private GameObject effect2;
    void Start()
    {
        Value = prosentDamage + (fromUnit.grade * 0.02f);
        chance += fromUnit.grade * 2;
        if (PlayerData.language == 0)
        {
            nameText = "Pass";
            SType = "Passive";
            description = $"After an enemy shooter attacks an ally, the Moon Mage has a chance to throw his own Moon Orb in response.\r\nChance: {chance}%\r\nDamage: {Convert.ToInt32(Value * 100)}%";
        }
        else
        {
            nameText = "Пас";
            SType = "Пассивная";
            description = $"После того как вражеский стрелок атакует союзника, Лунный маг имеет шанс бросить свою лунную сферу в ответ.\r\nШанс: {chance}%\r\nУрон: {Convert.ToInt32(Value * 100)}%";
        }
    }
    public override IEnumerator AfterStep(Dictionary<string, int> inpData)
    {
        int rand = Random.Range(0, 3);
        if (rand != 2) BattleVoice.voiceSource.PlayOneShot(voiceAfter[rand]);
        parentUnit.pathAnimation.SetCaracterState("passive");
        yield return new WaitForSeconds(0.6f);
        StartIni.soundVoice.StrikeVoices(fromUnit.Model.indexVoice);
        BattleSound.sound.PlayOneShot(soundMid);
        BattleSound.sound.PlayOneShot(soundAfter);

        UnitProperties targetUnit = Turns.circlesMap[inpData["sideEnemy"], inpData["placeEnemy"]].newObject;
        GameObject bulletTarget = targetUnit.pathBulletTarget.gameObject;
        GameObject newBullet = Instantiate(effect2, parentUnit.transform.Find("bulletPass").position, Quaternion.identity);
        var direction = bulletTarget.transform.position - newBullet.transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Bullet bullet = newBullet.GetComponent<Bullet>();
        newBullet.transform.rotation = Quaternion.Euler(0, 0, angle);
        bullet.element = 6;
        bullet.unitTarget = targetUnit;
        bullet.unitFrom = parentUnit;
        bullet.damage = inpData["damage"];
        yield return new WaitForSeconds(0.8f);
        Turns.finishEndEvent = true;
    }
}
