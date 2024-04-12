using System;
public class SquireMelee : AbstractSpell
{
    public float withProsent;
    void Start()
    {
        withProsent = prosentDamage * fromUnit.damage;
        if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Double punch";
                SType = "Melee ability";
                description = $"The Squire makes a double attack.\r\nEnergy required: 2\r\nDamage per hit: {Convert.ToInt32(withProsent)} damage.";
            }
            else
            {
                nameText = "������� ����";
                SType = "����������� ������� ���������";
                description = $"������ �������� ������� �����.\r\n����������� �������: 2\r\n���� �� ����: {Convert.ToInt32(withProsent)} ��.";
            }
        }
    }
    public override void SwishMethod(int count)
    {
        fromUnit.Model.transform.Find("AttackSwish").gameObject.SetActive(true);
    }
}
