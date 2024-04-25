using System;
using UnityEngine;
public class LightWizardAura : AbstractSpell
{
    [HideInInspector] private float Value;
    [SerializeField] private GameObject Effect2;
    void Start()
    {
        Value = 1 + (fromUnit.grade * 0.2f);
        if (PlayerData.language == 0)
        {
            nameText = "Light energy";
            SType = "Aura";
            description = $"Before the battle, the wizard bestows light energy on his allies. You start the battle with {Convert.ToInt32(Value)} energy.";
        }
        else
        {
            nameText = "Энергия света";
            SType = "Аура";
            description = $"Волшебник перед боем дарует союзникам энергию света. Вы начинаете бой с {Convert.ToInt32(Value)} ед. энергии.";
        }
    }
}
