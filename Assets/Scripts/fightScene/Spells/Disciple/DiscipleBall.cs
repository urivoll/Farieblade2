using System;
public class DiscipleBall : AbstractSpell
{
    private float withProsent;
    void Start()
    {
        withProsent = prosentDamage * fromUnit.damage;
        if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Fire Flash";
                SType = "Ranged ability";
                description = $"The student causes a fiery flash that deals {Convert.ToInt32(withProsent)} damage.\r\nEnergy required: 3";
            }
            else
            {
                nameText = "�������� �������";
                SType = "����������� ������� ���������";
                description = $"������ �������� �������� �������, ������� ������� {Convert.ToInt32(withProsent)} �� �����.\r\n����������� �������: 3";
            }
        }
    }
}
