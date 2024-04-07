using UnityEngine;
public class RiderBulb : AbstractSpell
{
    [SerializeField] private GameObject bulb;
    [SerializeField] private GameObject bulbExp;
    [SerializeField] private GameObject mana;
    private GameObject bulbTarget;
    void Start()
    {
        if (transform.parent.gameObject.name == "Debuffs")
        {
            bulbTarget = Instantiate(bulb, parentUnit.pathBulletTarget.position, Quaternion.identity);
            Turns.takeDamage += Bulb;
            if (PlayerData.language == 0)
            {
                nameText = "Divine Shield";
                SType = "Buff";
                description = $"This character is under a divine shield that blocks any incoming damage.";
            }
            else
            {
                nameText = "Божественный щит";
                SType = "Усиливающее заклинание";
                description = $"Этот персонаж под божественным щитом который блокирует любой, входящий урон.";
            }
        }
        else if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Divine Shield";
                SType = "Buff";
                description = $"The knight grants his ally a Divine Shield, which blocks any incoming damage. After receiving damage, the shield disappears and the Knight receives 1 energy.\r\nEnergy required: 3";
            }
            else
            {
                nameText = "Божественный щит";
                SType = "Усиливающее заклинание";
                description = $"Рыцарь дарует своему союзнику Божественный щит, который блокирует любой, входящий урон. После получения урона, щит пропадает, а Рыцарь получает 1 ед энергии.\r\nНеобходимая энергия: 3";
            }
        }
    }
    private void Bulb(UnitProperties Victim)
    {
        if (Victim == parentUnit)
        {
            Instantiate(bulbExp, parentUnit.pathBulletTarget.position, Quaternion.identity);
            if(fromUnit != null)
            {
                fromUnit.Model.pathEnergy.SetEnergy(fromUnit.Model.pathEnergy.energy + 1);
                Instantiate(mana, fromUnit.Model.transform.Find("BulletTarget").position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
    public override void EndDebuff()
    {
        Destroy(bulbTarget);
        Turns.takeDamage -= Bulb;
    }
}
