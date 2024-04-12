using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritterBite : AbstractSpell
{
    private float withProsent;
    private float healLevel;
    [SerializeField]private GameObject heal;
    void Start()
    {
        withProsent = prosentDamage * fromUnit.damage;
        healLevel = withProsent * (0.2f + (fromUnit.grade * 0.01f));
        if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Eat in battle";
                SType = "Melee ability";
                description = $"The Critter bites the enemy, thereby dealing {Convert.ToInt32(withProsent)} damage and replenishing some of its health.\r\nEnergy required: 3\r\nHeal: {Convert.ToInt32(healLevel)}";
            }
            else
            {
                nameText = "Перекусить в бою";
                SType = "Способность ближней дистанции";
                description = $"Зубастик кусает противника, тем самым наносит {Convert.ToInt32(withProsent)} ед. урона и восполняет себе часть здоровья.\r\nНеобходимая энергия: 3\r\nЛечение: {Convert.ToInt32(healLevel)}";
            }
        }
    }
    public override IEnumerator HitEffect(Dictionary<string, int> inpData)
    {
        yield return null;
        if (inpData["catch"] == 1) 
        {
            Instantiate(heal, fromUnit.Model.transform.Find("BulletTarget").position, Quaternion.identity);
            fromUnit.Model.hp = inpData["hp"];
            fromUnit.Model.HpDamage("hp");
        }
    }
}
