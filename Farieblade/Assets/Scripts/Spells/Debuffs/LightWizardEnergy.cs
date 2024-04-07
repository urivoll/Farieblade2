public class LightWizardEnergy : AbstractSpell
{
    void Start()
    {
        if (transform.parent.gameObject.name == "Debuffs")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Wizard's Blessing";
                SType = "Buff";
                description = $"Whenever a character accumulates energy, he will also receive an additional 1 unit. energy.";
            }
            else
            {
                nameText = "������������� ����������";
                SType = "����������� ����������";
                description = $"������ ��� ����� �������� ����������� �������, �� ����� ������� �������������� 1 ��. �������.";
            }
        }
        else if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Wizard's Blessing";
                SType = "Buff";
                description = $"Whenever a character accumulates energy, he will also receive an additional 1 unit. energy.\r\nEnergy required: 3\nDuration: 2";
            }
            else
            {
                nameText = "������������� ����������";
                SType = "����������� ����������";
                description = $"������ ��� ����� �������� ����������� �������, �� ����� ������� �������������� 1 ��. �������.\r\n����������� �������: 3\n������������: 2";
            }
        }
    }
}
