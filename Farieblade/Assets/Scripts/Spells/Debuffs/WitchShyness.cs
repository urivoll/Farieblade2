public class WitchShyness : AbstractSpell
{
    public int Value = 20;
    void Start()
    {
        Value += fromUnit.grade;
        if (transform.parent.gameObject.name == "Debuffs")
        {
            duration = 3;
            startNumberTurn = Turns.numberTurn + duration;
            parentUnit.initiative -= Value;
        }
        if (PlayerData.language == 0)
        {
            nameText = "Shyness";
            SType = "Debuff";
            description = $"With just one piercing gaze, the witch of the crimson fields can make any opponent shy.\r\nEnergy required: 1\r\nDuration: 3\r\nInitiative: -{Value}";
        }
        else
        {
            nameText = "�������������";
            SType = "���������";
            description = $"���� ����� ������������� �������� ������ �������� ����� ����� ������� �������������� � ������ ����������.\r\n����������� �������: 1\r\n������������: 3\r\n����������: -{Value}";
        }
    }
    public override void EndDebuff()
    {
        parentUnit.initiative += Value;
    }
}
