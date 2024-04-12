using System;
using TMPro;
using UnityEngine;
public class EdwardPassive : AbstractSpell
{
    private float Value;
    private int count = 0;
    [SerializeField] private GameObject heal;
    [SerializeField] private TextMeshProUGUI textStuck;
    [SerializeField] private Animator animator;
    void Start()
    {
        Value = 0.2f + fromUnit.grade * 0.01f;
        if (transform.parent.gameObject.name == "Debuffs")
        {
            Turns.takeDamage += CastDebuff;
        }
        if (PlayerData.language == 0)
        {
            nameText = "Treatment resistance";
            SType = "Passive";
            description = $"Triggering any resistance barrier fills Edward with charges of power. Every 4th charge heals Edward by {Convert.ToInt32(Value * 100)}%.";
        }
        else
        {
            nameText = "Ћечебное сопротивление";
            SType = "ѕассивна€";
            description = $"—рабатывани€ любого барьера сопротивлени€ наполн€ет зар€ды силы Ёдварда.  аждый 4ый зар€д излечивает Ёдварда на {Convert.ToInt32(Value * 100)}%.";
        }
    }

    private void CastDebuff(UnitProperties victim)
    {
        if (victim.pathParent.resist == victim.inpDamageType) Up();
    }
    public void Up()
    {
        count++;
        if (count == 4)
        {
            count = 0;
            if (parentUnit.heal == true)
            {
                Instantiate(heal, parentUnit.pathBulletTarget.position, Quaternion.identity);
                parentUnit.hp += Convert.ToInt32(parentUnit.hpBase * Value);
                if (parentUnit.hp > fromUnit.hpBase) parentUnit.hp = fromUnit.hpBase;
                parentUnit.HpDamage("hp");
            }
            textStuck.text = Convert.ToString(" ");
        }
        else
        {
            textStuck.text = Convert.ToString(count);
            animator.SetTrigger("on");
        }
    }
    public override void EndDebuff()
    {
        Turns.takeDamage -= CastDebuff;
    }
}
