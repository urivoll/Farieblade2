public class FermorCanDebuff : AbstractSpell
{
    void Start()
    {
        if (transform.parent.gameObject.name == "Debuffs")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Incurable ulcers";
                SType = "Debuff";
                description = $"This character cannot receive healing.";
            }
            else
            {
                nameText = "����������� ����";
                SType = "���������";
                description = $"���� �������� �� ����� �������� �������.";
            }
        }
    }
}
