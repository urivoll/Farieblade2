public class EdwardVulHit : AbstractSpell
{
    void Start()
    {
        if (transform.parent.gameObject.name == "Debuffs")
        {
            parentUnit.transform.Find("ModeParticle1").gameObject.SetActive(true);
        }
        if (PlayerData.language == 0)
        {
            nameText = "Weak spot";
            SType = "Mode";
            description = $"Edward endows his weapon with special magic that itself adapts to the enemy�s weak spot. (The ability does not apply to opponents who do not have a weak point).";
        }
        else
        {
            nameText = "�������� �����";
            SType = "�����";
            description = $"������ �������� ���� ������ ������ ������ ������� ���� �������������� ��� �������� ����� ����������. (����������� �� ���������������� �� ����������� � ������� ��� ��������� �����).";
        }
    }
    public override void EndDebuff()
    {
        parentUnit.transform.Find("ModeParticle1").gameObject.SetActive(false);
    }
}
