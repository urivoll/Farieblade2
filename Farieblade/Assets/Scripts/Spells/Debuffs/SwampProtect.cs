using System;
public class SwampProtect : AbstractSpell
{
    public float Value = 0.15f;
    void Start()
    {
        Value += (fromUnit.grade * 0.01f);
        if (transform.parent.gameObject.name == "Debuffs")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Cloak of the Elements";
                SType = "Buff";
                description = $"The Flentz's cloak protects the character from elemental attacks. Elemental damage reduced by {Convert.ToInt32(Value * 100)}%";
            }
            else
            {
                nameText = "���� ������";
                SType = "����������� ����������";
                description = $"���� ������ ��������� ��������� �� ��������� ����. ���� ������ ������ �� {Convert.ToInt32(Value * 100)}%";
            }
        }
    }
}
