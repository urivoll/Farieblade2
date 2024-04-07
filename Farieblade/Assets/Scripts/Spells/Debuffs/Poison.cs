using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class Poison : AbstractSpell
{
    [SerializeField] private TextMeshProUGUI textStuck;
    [SerializeField] private Animator animator;
    public int stucks = 0;
    public float Value;
    void Start()
    {
        Value = (fromUnit.damage / 2) * (1 + fromUnit.grade * 0.1f);
        duration = 2;
        startNumberTurn = Turns.numberTurn + duration;
        StuckMethod();
        if (PlayerData.language == 0)
        {
            nameText = "Poison";
            SType = "Periodic damage";
            description = $"This character is poisoned and will lose health before each move.";
        }
        else
        {
            nameText = "Яд";
            SType = "Переодический урон";
            description = $"Этот персонаж отравлен, перед каждым ходом от будет терять здоровье.";
        }
    }
    public override void PeriodicMethod(Dictionary<string, int> inpData)
    {
        Instantiate(Effect, parentUnit.pathBulletTarget.position, Quaternion.identity, Turns.circlesTransform.transform);
        parentUnit.SpellDamage(inpData["damage"], 2);
    }
    public override void StuckMethod()
    {
        stucks += 1;
        if (stucks != 1)
        {
            textStuck.text = Convert.ToString(stucks);
            animator.SetTrigger("on");
        }
        startNumberTurn = Turns.numberTurn + duration;
        Value += (Value * 0.2f);
    }
}
