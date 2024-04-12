using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TerrableBleeding : AbstractSpell
{
    [SerializeField] private TextMeshProUGUI textStuck;
    [SerializeField] private Animator animator;
    public int stucks = 0;
    private float Value;
    private float Value2;
    void Start()
    {
        Value = (fromUnit.damage / 3) * (1 + fromUnit.grade * 0.1f);
        Value2 = 0.2f + fromUnit.grade * 0.01f;
        StuckMethod();
        if (PlayerData.language == 0)
        {
            nameText = "Bleeding";
            SType = "Periodic damage";
            description = $"This creature will lose health after each turn.";
        }
        else
        {
            nameText = "Кровотечение";
            SType = "Переодический урон";
            description = $"Это существо будет терять здоровье после каждого хода.";
        }
    }
    public override IEnumerator AfterStep(Dictionary<string, int> inpData)
    {
        if(parentUnit != null && parentUnit.pathCircle.newObject != null)
        {
            Instantiate(Effect, parentUnit.pathBulletTarget.position, Quaternion.identity, Turns.circlesTransform.transform);
            parentUnit.SpellDamage(inpData["damage"], 2);
        }
        yield return new WaitForSeconds(0.3f);
        Turns.finishEndEvent = true;
    }

    public override void StuckMethod()
    {
        stucks++;
        if (stucks != 1)
        {
            textStuck.text = Convert.ToString(stucks);
            animator.SetTrigger("on");
        }
        Value += (Value * Value2);
    }
}
