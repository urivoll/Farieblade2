using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExistingPassive : AbstractSpell
{
    private float Value;
    private int count = 0;
    [SerializeField] private TextMeshProUGUI textStuck;
    [SerializeField] private Animator animator;
    void Start()
    {
        Value = 0.1f + fromUnit.grade * 0.02f;
        if (transform.parent.gameObject.name == "Debuffs")
        {
            Turns.takeDamage += CastDebuff;
        }
        if (PlayerData.language == 0)
        {
            nameText = "Hot hands";
            SType = "Passive";
            description = $"Each fire damage received against an ally accumulates Existing charges, every 4 charges give the Existent an additional {Convert.ToInt32(Value * 100)}% damage.";
        }
        else
        {
            nameText = "Горячие руки";
            SType = "Пассивная";
            description = $"Каждый полученный урон огнем по союзнику копит заряды Сущего, каждый 4 заряд дает ему дополнительные {Convert.ToInt32(Value * 100)}% урона.";
        }
    }

    private void CastDebuff(UnitProperties victim)
    {
        if (victim.sideOnMap == parentUnit.sideOnMap &&
            victim.inpDamageType == 5)
        {
            count++;
            if (count == 4)
            {
                count = 0;
                parentUnit.transform.Find("AttackSwish").gameObject.SetActive(true);
                parentUnit.GetComponent<UnitProperties>().damage += Convert.ToInt32(parentUnit.GetComponent<UnitProperties>().damage * Value);
                parentUnit.GetComponent<UnitProperties>().HpDamage("dmg");
                textStuck.text = Convert.ToString(" ");
            }
            else
            {
                textStuck.text = Convert.ToString(count);
                animator.SetTrigger("on");
            }
        }
    }
    public override void EndDebuff()
    {
        Turns.takeDamage -= CastDebuff;
    }
}
