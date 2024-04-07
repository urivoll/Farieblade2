public class WitchSplittingProtection : AbstractSpell
{
    public float Value = 0.2f;
    void Start()
    {
        Value += fromUnit.grade * 0.01f;
        if (transform.parent.gameObject.name == "Debuffs")
        {
            parentUnit.resistance -= Value;
        }
        if (PlayerData.language == 0)
        {
            nameText = "Armor Splitting";
            SType = "Debuff";
            description = $"Witch of the Crimson Fields shatters the enemy's armor. Damage to target increased.\r\nEnergy required: 1\r\nDuration: 2\r\nResistance weakening: -{Value}%";
        }
        else
        {
            nameText = "������ �����";
            SType = "���������";
            description = $"������ �������� ����� ���������� ����� ����������. ���� �� ���� �������.\r\n����������� �������: 1\r\n������������: 2\r\n���������� �������������: -{Value}%";
        }
    }
    public override void EndDebuff()
    {
        parentUnit.resistance += Value;
    }
}
