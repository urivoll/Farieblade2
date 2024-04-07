using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RockDefend : AbstractSpell
{
    public float Value = 1.5f;
    public int chance = 35;
    void Start()
    {
        chance += fromUnit.grade;
        if (transform.parent.gameObject.name == "Debuffs")
        {
            Turns.punch += CastDebuff;
        }
        if (PlayerData.language == 0)
        {
            nameText = "Block";
            SType = "Passive";
            description = $"The revived stone has a chance to block an enemy's blow, reducing damage by 1.5 times.\r\nChance: {chance}%";
        }
        else
        {
            nameText = "Блок";
            SType = "Пассивная";
            description = $"Оживший камень имеет шанс заблокировать удар противника, снижая урон в 1.5 раза.\r\nШанс: {chance}%";
        }
    }

    private void CastDebuff(UnitProperties victim, UnitProperties from, List<Dictionary<string, int>> inpData)
    {
        for (int i = 0; i < inpData.Count; i++)
        {
            if (inpData[i]["side"] == parentUnit.sideOnMap &&
                inpData[i]["place"] == parentUnit.placeOnMap &&
                inpData[i]["debuffId"] == id)
            {
                BattleSound.sound.PlayOneShot(soundMid);
                StartCoroutine(Block());
            }
        }
    }
    private IEnumerator Block()
    {
        yield return new WaitForSeconds(0.01f);
        if(parentUnit.hp > 0)
            parentUnit.pathAnimation.SetCaracterState("passive");
    }
    public override void EndDebuff()
    {
        Turns.punch -= CastDebuff;
    }
}
