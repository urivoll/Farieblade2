using System;
using System.Collections;
using System.Collections.Generic;
public class TerrableMelee2 : AbstractSpell
{
    private float withProsent;
    private float value;
    void Start()
    {
        withProsent = prosentDamage * fromUnit.damage;
        value = 2 + fromUnit.grade * 0.2f;
        if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Series of blows";
                SType = "Melee ability";
                description = $"Dread Flentz makes a series of attacks on the target creature, stealing {Convert.ToInt32(value)} accuracy.\r\nEnergy required: 3\r\nDamage per hit: {Convert.ToInt32(withProsent)} damage.";
            }
            else
            {
                nameText = "Серия ударов";
                SType = "Способность ближней дистанции";
                description = $"Ужасный Фленц проводит серию ударов по выбранному существу, воруя {Convert.ToInt32(value)} ед. точности.\r\nНеобходимая энергия: 3\r\nУрон за удар: {Convert.ToInt32(withProsent)} ед.";
            }
        }
    }
    public override IEnumerator HitEffect(Dictionary<string, int> inpData)
    {
        yield return null;
        Turns.circlesMap[inpData["side"], inpData["place"]].newObject.accuracy = inpData["youAcc"];
        Turns.circlesMap[inpData["sideEnemy"], inpData["placeEnemy"]].newObject.accuracy = inpData["enemyAcc"];
        if (inpData.ContainsKey("heal"))
            Turns.circlesMap[inpData["side"], inpData["place"]].newObject.hp = inpData["heal"];
        if (inpData.ContainsKey("stuck"))
            Turns.circlesMap[inpData["sideEnemy"], inpData["placeEnemy"]].newObject.idDebuff[inpData["stuck"]].GetComponent<AbstractSpell>().StuckMethod();
    }
    public override void SwishMethod(int count)
    {
        fromUnit.Model.transform.Find("RightSwish").gameObject.SetActive(true);
    }
}
