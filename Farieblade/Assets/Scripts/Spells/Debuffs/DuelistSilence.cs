public class DuelistSilence : AbstractSpell
{
    void Start()
    {
        if (transform.parent.gameObject.name == "Debuffs")
        {
            duration = 2;
            startNumberTurn = Turns.numberTurn + duration;
            parentUnit.silence = true;
            if (PlayerData.language == 0)
            {
                nameText = "Silence";
                SType = "Debuff";
                description = $"This character has a good fighting spirit. Damage increased.\nDamage Boost: %";
            }
            else
            {
                nameText = "��������";
                SType = "���������";
                description = $"���� �������� � ������� ������ �����. ���� ��������.\n�������� �����: %";
            }
        }
    }
    public override void EndDebuff()
    {
        parentUnit.silence = false;
    }
}
