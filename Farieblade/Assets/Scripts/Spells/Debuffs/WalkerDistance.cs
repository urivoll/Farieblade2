using System;
using UnityEngine;
public class WalkerDistance : AbstractSpell
{
    private float Value;
    [SerializeField] private GameObject Effect2;
    private GameObject newObj;
    void Start()
    {
        Value = 0.4f + (fromUnit.grade * 0.02f);
        if (transform.parent.gameObject.name == "Debuffs")
        {
            newObj = Instantiate(Effect2, parentUnit.pathBulletTarget.position, Quaternion.identity);
            if (PlayerData.language == 0)
            {
                nameText = "Spore shield";
                SType = "Buff";
                description = $"This character is under the Walker's shield, if the attacker has more damage than the target, the shield is triggered and absorbs some of the damage.";
            }
            else
            {
                nameText = "Споровидный щит";
                SType = "Усиливающее заклинание";
                description = $"Этот персонаж под щитом Валкера, если у атакующего больше урона чем у цели, щит срабатывает и поглощает часть урона.";
            }
        }
        else if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Spore shield";
                SType = "Buff";
                description = $"Walker places a shield on an ally, which is triggered if the incoming, ranged damage is greater than the damage of the attacked one.\r\nEnergy required: 2\nDuration: 2\nBlock: {Convert.ToInt32(Value * 100)}%";
            }
            else
            {
                nameText = "Споровидный щит";
                SType = "Усиливающее заклинание";
                description = $"Валкер накладывает на союзника щит, который срабатывает если входящий, дистанционный урон больше урона атакованного.\r\nНеобходимая энергия: 2\nДлительность: 2\nБлок: {Convert.ToInt32(Value * 100)}%";
            }
        }
    }
    public override void EndDebuff()
    {
        Destroy(newObj);
    }
}
