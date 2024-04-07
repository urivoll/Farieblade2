using System;
using System.Collections.Generic;
using UnityEngine;
public class GodIce : AbstractSpell
{
    private float Value;
    private GameObject newObj;
    [SerializeField] private GameObject Effect2;
    [SerializeField] private GameObject heal;
    [SerializeField] private GameObject mana;
    [SerializeField] private AudioClip Break;
    void Start()
    {
        Value = 0.2f + (fromUnit.grade * 0.01f);
        if (transform.parent.gameObject.name == "Debuffs")
        {
            newObj = Instantiate(Effect2, parentUnit.transform.position, Quaternion.identity, Turns.circlesTransform.transform);
            if (parentUnit.pathParent.Type == 3) newObj.transform.localScale = new Vector2(2, 2);
            Turns.takeDamage += Cast;
            if (PlayerData.language == 0)
            {
                nameText = "Ice protection";
                SType = "Buff";
                description = $"The selected creature is engulfed in ice, and during the duration all damage except fire damage is blocked.";
            }
            else
            {
                nameText = "Ледяная защита";
                SType = "Усиливающее заклинание";
                description = $"Выбранное существо охвачено льдом, за время действия любой урон кроме огненного блокируется.";
            }
        }
        else if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Ice protection";
                SType = "Buff";
                description = $"The selected creature is covered in ice, and during the duration of the effect, any damage except fire damage is blocked. At the end of the effect, the creature is healed by {Convert.ToInt32(Value * 100)}%\r\nНеобходимая энергия: 2\nДлительность: 1";
            }
            else
            {
                nameText = "Ледяная защита";
                SType = "Усиливающее заклинание";
                description = $"Выбранное существо охватывается льдом, за время действия любой урон кроме огненного блокируется. По окончании действия существо получает лечится на {Convert.ToInt32(Value * 100)}%\r\nНеобходимая энергия: 2\nДлительность: 1";
            }
        }
    }
    private void Cast(UnitProperties victim)
    {
        if(victim == parentUnit && parentUnit.inpDamageType != 5) parentUnit.inpDamage = 0;
    }
    public override void PeriodicMethod(Dictionary<string, int> inpData)
    {
        UnitProperties targetUnit = Turns.circlesMap[inpData["side"], inpData["place"]].newObject;
        targetUnit.hp = inpData["heal"];
        targetUnit.HpDamage("hp");
        Instantiate(heal, targetUnit.pathBulletTarget.position, Quaternion.identity);
    }
    public override void EndDebuff()
    {
        Destroy(newObj);
        Turns.takeDamage -= Cast;
        if (parentUnit == null || parentUnit.hp <= 0) return;
        BattleSound.sound.PlayOneShot(Break);
        Instantiate(mana, parentUnit.pathBulletTarget.position, Quaternion.identity);
    }
}
