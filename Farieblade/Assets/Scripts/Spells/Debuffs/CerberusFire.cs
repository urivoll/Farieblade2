public class CerberusFire : AbstractSpell
{
    void Start()
    {
        if (transform.parent.gameObject.name == "Debuffs")
        {
            duration = 3;
            Turns.beforePunch += BoostDamage;
            startNumberTurn = Turns.numberTurn + duration;
            if (PlayerData.language == 0)
            {
                nameText = "Fire mania";
                SType = "Debuff";
                description = $"This creature deals fire damage.";
            }
            else
            {
                nameText = "�������� �����";
                SType = "��������";
                description = $"��� �������� ������� ���� �����.";
            }
        }
    }
    public override void EndDebuff()
    {
        Turns.beforePunch -= BoostDamage;
    }
    private void BoostDamage(UnitProperties victim, UnitProperties who)
    {
        if (who == parentUnit)
        {
            victim.inpDamageType = 5;
        }    
    }
}
