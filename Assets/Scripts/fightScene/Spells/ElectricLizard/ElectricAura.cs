using System;
using UnityEngine;
public class ElectricAura : AbstractSpell
{
    [HideInInspector] public float Value = 0.1f;
    void Start()
    {
        Value += fromUnit.grade * 0.01f;
        if (PlayerData.language == 0)
        {
            nameText = "Protection from Air";
            SType = "Aura";
            description = $"The Electric Lizard gives allies air protection before battle. Air damage dealt is reduced by {Convert.ToInt32(Value * 100)}%.";
        }
        else
        {
            nameText = "Защита от воздуха";
            SType = "Аура";
            description = $"Электрический ящер перед боем дает союзникам защиту от воздуха. Входящий урон воздухом снижается на {Convert.ToInt32(Value * 100)}%.";
        }
    }
}
