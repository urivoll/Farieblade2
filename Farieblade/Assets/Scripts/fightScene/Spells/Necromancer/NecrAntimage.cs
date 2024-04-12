using UnityEngine;
public class NecrAntimage : AbstractSpell
{
    [SerializeField] private GameObject EffectStart;
    [SerializeField] private GameObject EffectEnd;
    private GameObject newObj;
    void Start()
    {
        if (transform.parent.gameObject.name == "Debuffs")
        {
            newObj = Instantiate(EffectStart, parentUnit.pathBulletTarget.position, Quaternion.identity);
            Turns.getDebuff += Cast;

            if (PlayerData.language == 0)
            {
                nameText = "Antimagic";
                SType = "Buff";
                description = $"Antimagic blocks the first negative effect on a creature.";
            }
            else
            {
                nameText = "Антимагия";
                SType = "Усиливающее заклинание";
                description = $"Антимагия блокирует первый отрицательный эффект на существе.";
            }
        }
        else if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Antimagic";
                SType = "Buff";
                description = $"Antimagic blocks the first negative effect on a creature.\r\nEnergy required: 3\nDuration: 2";
            }
            else
            {
                nameText = "Антимагия";
                SType = "Усиливающее заклинание";
                description = $"Антимагия блокирует первый отрицательный эффект на существе.\r\nНеобходимая энергия: 3\nДлительность: 2";
            }
        }
    }
    private void Cast(GameObject debuff, UnitProperties victim)
    {
        if(victim == parentUnit && debuff.GetComponent<AbstractSpell>().Type == "Debuff")
        {
            Destroy(newObj);
            Destroy(debuff);
            Instantiate(EffectEnd, victim.pathBulletTarget.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    public override void EndDebuff()
    {
        Turns.getDebuff -= Cast;
        Destroy(newObj);
    }
}
