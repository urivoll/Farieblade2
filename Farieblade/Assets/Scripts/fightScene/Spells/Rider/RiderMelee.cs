using System;
public class RiderMelee : AbstractSpell
{
    public float withProsent;
    public float vaule;
    void Start()
    {
        withProsent = prosentDamage * fromUnit.damage;
        vaule = 4 + (fromUnit.grade * 0.4f);
        if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Strike of light";
                SType = "Melee ability";
                description = $"The horse rears up and the knight strikes with a full swing, dealing {Convert.ToInt32(withProsent)} damage and reducing the enemy's total health by {Convert.ToInt32(vaule)}%.\r\nEnergy required: 3";
            }
            else
            {
                nameText = "���� �����";
                SType = "����������� ������� ���������";
                description = $"������ ������ �� ����, ������ ������� ���� � �������, ������ {Convert.ToInt32(withProsent)} ��. ����� � ������ ����� �������� ���������� �� {Convert.ToInt32(vaule)}%.\r\n����������� �������: 3";
            }
        }
    }
    public override void SwishMethod(int count)
    {
        fromUnit.Model.transform.Find("AttackSwish").gameObject.SetActive(true);
    }
}
