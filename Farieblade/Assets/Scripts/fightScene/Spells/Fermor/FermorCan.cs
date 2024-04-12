using System;
using UnityEngine;
public class FermorCan : AbstractSpell
{
    private float withProsent;
    [SerializeField]private GameObject debuff;
    void Start()
    {
        withProsent = prosentDamage * fromUnit.damage;
        if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Ball of corruption";
                SType = "Ranged ability";
                description = $"Fermor summons a ball of corruption, the creature takes {Convert.ToInt32(withProsent)} damage and cannot receive healing.\r\nEnergy required: 2";
            }
            else
            {
                nameText = "Шар порчи";
                SType = "Способность дальней дистанции";
                description = $"Фермор вызывает шар порчи, существо получает {Convert.ToInt32(withProsent)} ед. урона, а также не может получать лечение.\r\nНеобходимая энергия: 2";
            }
        }
    }
}
