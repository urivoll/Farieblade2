using System;
public class FermorFireBall : AbstractSpell
{
    public float withProsent;
    void Start()
    {
        withProsent = prosentDamage * fromUnit.damage;
        if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Fire ball";
                SType = "Ranged ability";
                description = $"Fermor casts a Fire ball that deals {Convert.ToInt32(withProsent)} damage.\r\nEnergy required: 4";
            }
            else
            {
                nameText = "�������� ���";
                SType = "����������� ������� ���������";
                description = $"������ �������� �������� ���, ������� ������� {Convert.ToInt32(withProsent)} �� �����.\r\n����������� �������: 4";
            }
        }
    }
}
