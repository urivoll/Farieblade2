using System;
using System.Collections.Generic;

public class TwinsDamageDebuff : AbstractSpell
{
    private float Value;
    private int TempValue;
    void Start()
    {
        Value = 60 + (fromUnit.grade * 2);
        if (transform.parent.gameObject.name == "Debuffs")
        {
            TempValue = Convert.ToInt32(parentUnit.transform.parent.transform.parent.gameObject.GetComponent<Unit>().damage * (Value / 100));
            parentUnit.damage -= TempValue;
            parentUnit.HpDamage("dmg");

            if (PlayerData.language == 0)
            {
                nameText = "amyotrophy";
                SType = "Debuff";
                description = $"This character has muscle atrophy and his damage is reduced by {Convert.ToInt32(Value)}%";
            }
            else
            {
                nameText = "Атрофия мышц";
                SType = "Проклятье";
                description = $"У этого персонажа атрофия мышц, его урон уменьшен на {Convert.ToInt32(Value)}%";
            }
        }
    }
    public override void EndDebuff()
    {
        parentUnit.damage += TempValue;
        parentUnit.HpDamage("dmg");
    }
}
