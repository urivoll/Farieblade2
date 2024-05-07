using System;
public class LizardWarriorBall : AbstractSpell
{
    public float withProsent;
    void Start()
    {
        withProsent = prosentDamage * fromUnit.damage;
        if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Javelin-throwing";
                SType = "Ranged ability";
                description = $"The lizard warrior throws his spear at the enemy, thereby inflicting {Convert.ToInt32(withProsent)} damage.\r\nEnergy required: 2";
            }
            else
            {
                nameText = "������� �����";
                SType = "����������� ������� ���������";
                description = $"���� ���� ������ ���� ����� � ���������� ��� ����� ������ {Convert.ToInt32(withProsent)} ��. �����.\r\n����������� �������: 2";
            }
        }
    }
}
