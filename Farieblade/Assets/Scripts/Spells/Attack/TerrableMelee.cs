using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrableMelee : AbstractSpell
{
    private float withProsent;
    private float value;
    private float value2;
    [SerializeField] private GameObject debuff;
    void Start()
    {
        withProsent = prosentDamage * fromUnit.damage;
        value = (fromUnit.damage / 3) * (1 + fromUnit.grade * 0.1f);
        value2 = 0.2f + fromUnit.grade * 0.01f;
        if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Bloody injection";
                SType = "Melee ability";
                description = $"The targeted creature takes {Convert.ToInt32(withProsent)} damage and “Bleeding”.\r\nEnergy required: 3";
            }
            else
            {
                nameText = "Кровавый укол";
                SType = "Способность ближней дистанции";
                description = $"Выбранное существо получает {Convert.ToInt32(withProsent)} ед. урона, и «Кровотечение».\r\nНеобходимая энергия: 3";
            }
        }
    }
    public override IEnumerator AfterStep(Dictionary<string, int> inpData)
    {
        GameObject newObject = Instantiate(debuff, Turns.circlesMap[inpData["sideEnemy"], inpData["placeEnemy"]].newObject.pathDebuffs);
        newObject.GetComponent<AbstractSpell>().fromUnit = fromUnit;
        yield return new WaitForSeconds(0.3f);
        Turns.finishEndEvent = true;
    }
    public override void SwishMethod(int count)
    {
        fromUnit.Model.transform.Find("RightSwish").gameObject.SetActive(true);
    }
}
