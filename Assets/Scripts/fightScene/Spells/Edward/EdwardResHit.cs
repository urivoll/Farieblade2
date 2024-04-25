public class EdwardResHit : AbstractSpell
{
    void Start()
    {
        if (transform.parent.gameObject.name == "Debuffs")
        {
            parentUnit.transform.Find("ModeParticle2").gameObject.SetActive(true);
        }
        if (PlayerData.language == 0)
        {
            nameText = "Strike the resistance";
            SType = "Mode";
            description = $"Edward endows his weapon with special magic that itself adapts to the enemy�s resistance. (The ability does not apply to opponents who have no resistance).";
        }
        else
        {
            nameText = "���� �� �������������";
            SType = "�����";
            description = $"������ �������� ���� ������ ������ ������ ������� ���� �������������� ��� ������������� ����������. (����������� �� ���������������� �� ����������� � ������� ��� �������������).";
        }
    }
    public override void EndDebuff()
    {
        parentUnit.transform.Find("ModeParticle2").gameObject.SetActive(false);
    }
}
