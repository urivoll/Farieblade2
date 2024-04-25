using System;
using System.Collections.Generic;
using UnityEngine;
public class TerrablePassive : AbstractSpell
{
    public float Value;
    [SerializeField] private GameObject heal;
    void Start()
    {
        Value = 0.2f + fromUnit.grade * 0.01f;
        if (transform.parent.gameObject.name == "Debuffs")
        {
            Turns.punch += Cast;
        }
        if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Vampirism";
                SType = "Passive";
                description = $"If the attacked creature is Bleeding, Dread Flentz is healed by {Convert.ToInt32(Value * 100)}% of the hit dealt.";
            }
            else
            {
                nameText = "Вампиризм";
                SType = "Пассивная";
                description = $"Если атакованное существо под действием кровотечения, Ужасный Фленц излечивается на {Convert.ToInt32(Value * 100)}% от нанесенного удара.";
            }
        }
    }
    private void Cast(UnitProperties victim, UnitProperties from2, List<Dictionary<string, int>> inpData)
    {
        for (int i = 0; i < inpData.Count; i++)
        {
            if (inpData[i]["side"] == parentUnit.ParentCircle.Side &&
                inpData[i]["place"] == parentUnit.ParentCircle.Place &&
                inpData[i]["debuffId"] == id)
            {
/*                if (parentUnit.heal == true)
                {
                    Instantiate(heal, parentUnit.pathBulletTarget.position, Quaternion.identity);
                    parentUnit.hp = inpData[i]["heal"];
                    parentUnit.HpDamage("hp");
                }*/
                UnitProperties unit = _characterPlacement.CirclesMap[inpData[i]["sideTarget"], inpData[i]["placeTarget"]].ChildCharacter;
                foreach (GameObject index in unit.DebuffList)
                {
                    if(index.GetComponent<AbstractSpell>().id == inpData[i]["debuffIdTarget"])
                    {
                        index.GetComponent<AbstractSpell>().StuckMethod();
                    }
                }
            }
        }
    }
    public override void EndDebuff()
    {
        Turns.punch -= Cast;
    }
}
