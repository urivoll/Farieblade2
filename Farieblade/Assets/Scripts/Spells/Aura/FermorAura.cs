public class FermorAura : AbstractSpell
{
    void Start()
    {
        if (transform.parent.gameObject.name == "Debuffs")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Fire connection";
                SType = "Aura";
                description = $"The characteristics of the snow wolf are associated with Fermor.";
            }
            else
            {
                nameText = "�������� �����";
                SType = "����";
                description = $"�������������� �������� ����� ������� � ��������.";
            }
        }
    }
}
