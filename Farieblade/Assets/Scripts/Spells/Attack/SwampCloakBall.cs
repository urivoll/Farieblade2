using System;
using UnityEngine;
public class SwampCloakBall : AbstractSpell
{
    private float value = 0.15f;
    [SerializeField] private GameObject debuff;
    void Start()
    {
        value += fromUnit.grade * 0.01f;
        if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Cloak of the Elements";
                SType = "Buff";
                description = $"Flentz will cover an ally with his traveling cloak, increasing elemental protection by {Convert.ToInt32(value * 100)}%.\r\nEnergy required: 2\r\nDuration: 3";
            }
            else
            {
                nameText = "Плащ стихий";
                SType = "Усиливающее заклинание";
                description = $"Фленц накрывет союзника своим походым плащом, защита от стихий увеличивается на {Convert.ToInt32(value * 100)}%\r\nНеобходимая энергия: 2\r\nДлительность: 3";
            }
        }
    }
}
