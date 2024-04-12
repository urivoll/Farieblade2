using System;
using TMPro;
using UnityEngine;
public class SoulSeekerWeaknessRes : AbstractSpell
{
    [SerializeField] private TextMeshProUGUI textStuck;
    [SerializeField] private Animator animator;
    public int stucks = 0;
    public float value = 0.1f;
    void Start()
    {
        if (transform.parent.gameObject.name == "Debuffs")
        {
            duration = 2;
            value += fromUnit.grade * 0.01f;
            StuckMethod();
            if (PlayerData.language == 0)
            {
                nameText = "Astral Explosion";
                SType = "Stuck debuff";
                description = $"The character has a curse, elemental resistance is reduced by {Convert.ToInt32(value * 100)}%.";
            }
            else
            {
                nameText = "Астральный взрыв";
                SType = "Накапливающеется проклятье";
                description = $"Персонаж имеет проклятье, стихийное сопротивление снижено на {Convert.ToInt32(value * 100)}%.";
            }
        }
    }
    public override void StuckMethod()
    {
        stucks += 1;
        if (stucks != 1)
        {
            textStuck.text = Convert.ToString(stucks);
            animator.SetTrigger("on");
        }
    }
}
