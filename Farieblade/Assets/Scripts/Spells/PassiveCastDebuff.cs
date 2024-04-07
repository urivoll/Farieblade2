using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class PassiveCastDebuff : AbstractSpell
{
    [SerializeField] private TextMeshProUGUI textStuck;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject debuff;
    [SerializeField] private int chance;
    [SerializeField] private int particle = 0;
    private UnitProperties victim;
    void Start()
    {
        Start2();
    }
    private void CastDebuff(UnitProperties Victim)
    {
        if (Turns.turnUnit == parentUnit)
        {
            int rand = Random.Range(0, 100);
            if (Victim.hp > 0 && rand <= chance)
            {
                victim = Victim;
                Turns.eventEndCard.Add(gameObject);
            }
        }
    }
    public override IEnumerator AfterStep(Dictionary<string, int> inpData)
    {
        if (victim != null && victim.pathCircle.newObject != null)
        {
            GameObject newObject = Instantiate(debuff, victim.pathDebuffs);
            newObject.GetComponent<AbstractSpell>().fromUnit = parentUnit.pathParent;
        }
        yield return new WaitForSeconds(0.8f);
        Turns.finishEndEvent = true;
    }
    public override void EndDebuff()
    {
        if (particle == 1) parentUnit.transform.Find("ModeParticle1").gameObject.SetActive(false);
        else if (particle == 2) parentUnit.transform.Find("ModeParticle2").gameObject.SetActive(false);
        Turns.takeDamage -= CastDebuff;
    }
    public void Start2()
    {
        if (transform.parent.gameObject.name == "Debuffs")
        {
            if (particle == 1) parentUnit.transform.Find("ModeParticle1").gameObject.SetActive(true);
            else if (particle == 2) parentUnit.transform.Find("ModeParticle2").gameObject.SetActive(true);
            Turns.takeDamage += CastDebuff;
        }
    }
}
