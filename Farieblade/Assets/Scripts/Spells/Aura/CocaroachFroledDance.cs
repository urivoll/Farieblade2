using System;
using System.Collections.Generic;
using UnityEngine;
public class CocaroachFroledDance : AbstractSpell
{
    public float Value = 0.3f;
    void Start()
    {
        Value += (fromUnit.grade * 0.01f);
        if (transform.parent.gameObject.name == "Debuffs")
        {
            Turns.tryDeath += Buff;
        }
        if (PlayerData.language == 0)
        {
            nameText = "Beetle Dance";
            SType = "Aura";
            description = $"The armored beetle performs its dance, all allies from the Froled World go into a trance. If an ally with this effect dies, those remaining receive additional health equal to {Convert.ToInt32(Value * 100)}% of the deceased's health, distributed equally.";
        }
        else
        {
            nameText = "Танец жука";
            SType = "Аура";
            description = $"Жук броневик исполняет свой танец, все союзники из Мира Фролед входят в транс. В случае смерти союзника с этим эффектом, оставшиеся получают дополнительное здоровье в размере {Convert.ToInt32(Value * 100)}% от здоровья погибшего, распределенного поровну.";
        }
    }
    private void Buff(UnitProperties victim, Dictionary<string, int> inpData)
    {
        if (inpData["side"] == parentUnit.sideOnMap &&
            inpData["place"] == parentUnit.placeOnMap &&
            inpData["debuffIndex"] == parentUnit.idDebuff.IndexOf(gameObject))
        {
            for (int i = 0; i < inpData["count"]; i++)
            {
                UnitProperties unit = Turns.circlesMap[inpData["side"], inpData[$"placeTarget{i}"]].newObject;
                unit.hp = inpData[$"hp{i}"];
                unit.HpDamage("hp");
                Instantiate(Effect, unit.transform.Find("BulletTarget").position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
    public override void EndDebuff()
    {
        Turns.tryDeath -= Buff;
    }
}
