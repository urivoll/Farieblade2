using System;
using System.Collections.Generic;
public class DemonSplash : AbstractSpell
{
    private float Value;
    void Start()
    {
        Value = 0.3f + (fromUnit.grade * 0.02f);
        if (transform.parent.gameObject.name == "Debuffs")
        {
            Turns.punch += Hit;
        }
        if (PlayerData.language == 0)
        {
            nameText = "Splash Damage";
            SType = "Passive";
            description = $"During the attack, the Demon also deals some damage to enemies close to the target.\r\nDamage: {Convert.ToInt32(Value * 100)}%";
        }
        else
        {
            nameText = "Разбрызгивание урона";
            SType = "Пассивная";
            description = $"Во время атаки Демон наносит также часть урона ближним от цели врагам.\r\nУрон: {Convert.ToInt32(Value * 100)}%";
        }
    }
    public void Hit(UnitProperties victim, UnitProperties from, List<Dictionary<string, int>> inpData)
    {
        for (int i = 0; i < inpData.Count; i++)
        {
            if (inpData[i]["side"] == parentUnit.sideOnMap &&
                inpData[i]["place"] == parentUnit.placeOnMap &&
                inpData[i]["debuffId"] == id)
            {
                for (int i2 = 0; i2 < inpData[i]["count"]; i2++)
                {
                    Turns.circlesMap[inpData[i]["sideEnemy"], inpData[i][$"placeExtra{i2}"]].newObject.SpellDamage(inpData[i][$"damage{i2}"], 4);
                }
            }
        }
    }
    public override void EndDebuff()
    {
        Turns.punch -= Hit;
    }
}
