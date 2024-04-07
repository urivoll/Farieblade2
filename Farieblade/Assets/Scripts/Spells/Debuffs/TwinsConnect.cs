using UnityEngine;
public class TwinsConnect : AbstractSpell
{
    private GameObject parent;
    [SerializeField] private GameObject connect2;
    [HideInInspector] public bool done = false;
    [SerializeField] private GameObject Effect2;
    [SerializeField] private GameObject Effect2Hit;
    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioClip clipHit;
    void Start()
    {
        if (transform.parent.gameObject.name == "Debuffs")
        {
            if (fromUnit.transform.Find("Fight/Canvas/Debuffs/TwinsConnect2(Clone)") != null)
            {
                parent = fromUnit.transform.Find("Fight/Canvas/Debuffs/TwinsConnect2(Clone)").gameObject;
                parent.GetComponent<TwinsConnect2>().ChangeChild();
            }
            else parent = Instantiate(connect2, fromUnit.Model.pathDebuffs);
            parent.GetComponent<TwinsConnect2>().child = gameObject;
            Instantiate(Effect2, fromUnit.Model.transform.Find("BulletTarget").position, Quaternion.identity);
            Instantiate(Effect2, parentUnit.pathBulletTarget.position, Quaternion.identity);
            BattleSound.sound.PlayOneShot(clip);
            duration = 2;
            startNumberTurn = Turns.numberTurn + duration;
            Turns.takeDamage += Cast;

            if (PlayerData.language == 0)
            {
                nameText = "Water connection";
                SType = "Buff";
                description = $"When Twins ally takes damage, it is shared equally with the Gemini. Gemini takes water damage.";
            }
            else
            {
                nameText = "Водная связь";
                SType = "Усиливающее заклинание";
                description = $"Когда союзник Близнецов получает урон, он распределяется поровну с Близнецами. Близнецы получают урон водой.";
            }
        }
        else if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Water connection";
                SType = "Buff";
                description = $"When Twins ally takes damage, it is shared equally with the Gemini. Gemini takes water damage.\r\nEnergy required: 3\nDuration: 2";
            }
            else
            {
                nameText = "Водная связь";
                SType = "Усиливающее заклинание";
                description = $"Когда союзник Близнецов получает урон, он распределяется поровну с Близнецами. Близнецы получают урон водой.\r\nНеобходимая энергия: 3\nДлительность: 2";
            }
        }
    }
    private void Cast(UnitProperties victim)
    {
        if (victim != parentUnit) return;
        if (parentUnit.hp - parentUnit.inpDamage / 2 > 0)
        {
            parentUnit.inpDamage /= 2;
            Instantiate(Effect2Hit, fromUnit.Model.transform.Find("BulletTarget").position, Quaternion.identity);
            fromUnit.Model.SpellDamage(parentUnit.inpDamage / 2, 1);
            BattleSound.sound.PlayOneShot(clipHit);
        }
        else Destroy(gameObject);
    }
    public override void EndDebuff()
    {
        if(done == false && parent != null)
        {
            parent.GetComponent<TwinsConnect2>().done = true;
            Destroy(parent);
        }
        Turns.takeDamage -= Cast;
        
    }
}
