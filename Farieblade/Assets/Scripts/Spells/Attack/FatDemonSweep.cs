using System;
using System.Collections;
using System.Collections.Generic;
public class FatDemonSweep : AbstractSpell
{
    private float withProsent;
    private float value;
    void Start()
    {
        withProsent = fromUnit.damage * prosentDamage;
        value = withProsent * 0.6f;
        if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Sweeping Strike";
                SType = "Melee ability";
                description = $"The fat demon swings and hits an enemy, dealing {Convert.ToInt32(withProsent)} damage and also hitting nearby enemies, who also take {Convert.ToInt32(value)} damage.\r\nEnergy required: 3";
            }
            else
            {
                nameText = "Размашистый удар";
                SType = "Способность ближней дистанции";
                description = $"Толстый демон размахивается и ударяет по врагу, нанося {Convert.ToInt32(withProsent)} ед. урона, а также задевая рядом стоящих противников, которые также получают урон в размере {Convert.ToInt32(value)} ед. \r\nНеобходимая энергия: 3";
            }
        }
    }
    public override void SwishMethod(int count)
    {
        fromUnit.Model.transform.Find("AttackSwishLeft").gameObject.SetActive(true);
        fromUnit.Model.transform.Find("AttackSwishRight").gameObject.SetActive(true);
    }
    public override IEnumerator HitEffect(Dictionary<string, int> inpData)
    {
        yield return null;
        if (inpData.ContainsKey("damage")) Turns.circlesMap[inpData["sideEnemy"], inpData["placeEnemy"]].newObject.SpellDamage(inpData["damage"], 4);
    }
}
