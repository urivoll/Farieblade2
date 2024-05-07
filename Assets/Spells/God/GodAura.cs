public class GodAura : AbstractSpell
{
    void Start()
    {
        if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Summoning the Snow Wolf";
                SType = "Aura";
                description = $"The monk's spirit summons a snow wolf before battle. Which duplicates the current characteristics of the summoner.";
            }
            else
            {
                nameText = "����� �������� �����";
                SType = "����";
                description = $"��� ������ ����� ���� ��������� �������� �����. ������� ��������� ������� �������������� �����������.";
            }
        }
    }
}
