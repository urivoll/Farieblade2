public class WitchMiss : AbstractSpell
{
    public int Value = 20;
    void Start()
    {
        Value += fromUnit.grade;
        if (transform.parent.gameObject.name == "Debuffs")
        {
            parentUnit.accuracy -= Value;
            if (PlayerData.language == 0)
            {
                nameText = "Blindness";
                SType = "Debuff";
                description = $"This character is blinded, accuracy is reduced on {Value}%";
            }
            else
            {
                nameText = "����������";
                SType = "���������";
                description = $"���� �������� ��������, �������� ������� �� {Value}%";
            }
        }
        else if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Blindness";
                SType = "Debuff";
                description = $"The Witch of the Crimson Fields casts a spell on her enemies, blinding them. The enemy has a penalty to accuracy.\r\n\b>Energy required:\b 1\r\nDuration: 2\r\nAccuracy: -{Value}%";
            }
            else
            {
                nameText = "����������";
                SType = "���������";
                description = $"������ �������� ����� ����������� ����� �� ����� ������ �������� ��. ��������� ����� ����� � ��������.\r\n\b����������� �������:\b 1\r\n������������: 2\r\n��������: -{Value}%";
            }
        }
    }
    public override void EndDebuff()
    {
        parentUnit.accuracy += Value;
    }
}
