using System;
public class CocaroachMelee : AbstractSpell
{
    public float withProsent;
    void Start()
    {
        withProsent = prosentDamage * fromUnit.damage;
        if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Triple Claw Strike";
                SType = "Melee ability";
                description = $"The armored beetle quickly deals 3 blows with its claws to the enemy.\r\nEnergy required: 3\r\nDamage per hit: {Convert.ToInt32(withProsent)} damage.";
            }
            else
            {
                nameText = "������� ���� ��������";
                SType = "����������� ������� ���������";
                description = $"��� �������� ������������ ������� 3 ����� ������ �������� �� ����������.\r\n����������� �������: 3\r\n���� �� ����: {Convert.ToInt32(withProsent)} ��.";
            }
        }
    }
    public override void SwishMethod(int count)
    {
        if      (count == 1) fromUnit.Model.transform.Find("AttackSwishLeft").gameObject.SetActive(true);
        else if (count == 2) fromUnit.Model.transform.Find("AttackSwishRight").gameObject.SetActive(true);
        else
        {
            fromUnit.Model.transform.Find("AttackSwishLeft").gameObject.SetActive(true);
            fromUnit.Model.transform.Find("AttackSwishRight").gameObject.SetActive(true);
        }
    }
}
