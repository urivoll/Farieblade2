using System;
public class FatDemonMelee2 : AbstractSpell
{
    public float withProsent;
    void Start()
    {
        withProsent = prosentDamage * fromUnit.damage;
        if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Strong claw strike";
                SType = "Melee ability";
                description = $"The zombie swings and strikes the target with its claws, dealing {Convert.ToInt32(withProsent)} damage.\r\nEnergy required: 2";
            }
            else
            {
                nameText = "������� ���� �������";
                SType = "����������� ������� ���������";
                description = $"����� ������������� � ���� ���� �������, ������ {Convert.ToInt32(withProsent)} ��. �����.\r\n����������� �������: 2";
            }
        }
    }
}
