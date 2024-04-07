using System;
using System.Collections.Generic;
public class WalkerCatch : AbstractSpell
{
    public int Value = 35;
    void Start()
    {
        Value += Convert.ToInt32(fromUnit.grade * 0.2f);
        if (transform.parent.gameObject.name == "Debuffs")
        {
            Turns.shooterPunch += CastDebuff;
        }
        if (PlayerData.language == 0)
        {
            nameText = "Capturing the Threat";
            SType = "Passive";
            description = $"The Walker has large antennas that are capable of intercepting and sending a flying projectile back to the enemy with a {Value}% chance, while the damage of the flying projectile is divided by 2. (The ability only works on card type: Shooter).";
        }
        else
        {
            nameText = "����������� ������";
            SType = "���������";
            description = $"������ ����� ������� ������� ������� �������� � ������ � {Value}% ����������� � ��������� ������� ������ ������� � ����������, ��� ���� ���� �������� ������� ������� �� 2. (����������� ����������� ������ �� ��� �����: �������).";
        }
    }
    private void CastDebuff(UnitProperties victim, UnitProperties from, List<Dictionary<string, int>> inpData)
    {
        for (int i = 0; i < inpData.Count; i++)
        {
            if (inpData[i]["side"] == parentUnit.sideOnMap &&
                inpData[i]["place"] == parentUnit.placeOnMap &&
                inpData[i]["debuffId"] == id)
            {
                parentUnit.pathAnimation.SetCaracterState("spell");
                from.gameObject.GetComponent<Shooter>().newBullet.gameObject.AddComponent<WalkerDebuff>();
                from.gameObject.GetComponent<Shooter>().newBullet.damage = inpData[i]["damage"];
            }
        }
    }
    public override void EndDebuff()
    {
        Turns.shooterPunch -= CastDebuff;
    }
}
