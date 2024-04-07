using System;
using TMPro;
using UnityEngine;
public class LightWeak : AbstractSpell
{
    public float Value = 0;
    [SerializeField] private TextMeshProUGUI textStuck;
    [SerializeField] private Animator animator;
    private int stucks = 0;
    void Start()
    {
        if (transform.parent.gameObject.name == "Debuffs")
        {
            duration = 2;
            if (PlayerData.language == 0)
            {
                nameText = "Weakness to light";
                SType = "Stuck Debuff";
                description = $"This character has a weakness to light. All light damage deals an additional {Convert.ToInt32(Value * 100)}% damage.";
            }
            else
            {
                nameText = "—лабость к свету";
                SType = "Ќакапливающеетс€ прокл€тье";
                description = $"Ётот персонаж имеет слабость к свету. ¬с€кий урон светом наносит дополнительные {Convert.ToInt32(Value * 100)}% урона.";
            }
            StuckMethod();
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
        startNumberTurn = Turns.numberTurn + duration;
        Value += 0.05f;
    }
}
