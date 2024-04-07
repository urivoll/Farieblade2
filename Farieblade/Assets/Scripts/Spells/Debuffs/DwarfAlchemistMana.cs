using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DwarfAlchemistMana : AbstractSpell
{
    [HideInInspector] public float Value;
    [SerializeField] private GameObject Effect2;
    void Start()
    {
        Value = 2 + (fromUnit.grade * 0.2f);
        if (PlayerData.language == 0)
        {
            nameText = "Light energy";
            SType = "Buff";
            description = $"The alchemist gives you an elixir of energy to drink. The ally receives {Convert.ToInt32(Value)} energy.\r\nEnergy required: 1";
        }
        else
        {
            nameText = "Энергия света";
            SType = "Усиливающее заклинание";
            description = $"Алхимик дает выпить эликсир энергии. Союзник получает {Convert.ToInt32(Value)} ед. энергии.\r\nНеобходимая энергия: 1";
        }
    }
    public override IEnumerator HitEffect(Dictionary<string, int> inpData)
    {
        UnitProperties targetUnit = Turns.circlesMap[inpData["side"], inpData["place"]].newObject;
        yield return new WaitForSeconds(timeBeforeShoot);
        targetUnit.pathEnergy.SetEnergy(inpData["energy"]);
        BattleSound.sound.PlayOneShot(soundMid);
        Instantiate(Effect2, targetUnit.pathBulletTarget.position, Quaternion.identity);
        yield return new WaitForSeconds(0.4f);
        Turns.hitDone = true;
    }
}
