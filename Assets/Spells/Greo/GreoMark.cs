using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GreoMark : AbstractSpell
{
    private int Value;
    [SerializeField] private GameObject Effect2;
    void Start()
    {
        Value = 5 + fromUnit.grade / 2;
        if (transform.parent.gameObject.name == "Debuffs")
        {
            Turns.punch += Cast;
            if (PlayerData.language == 0)
            {
                nameText = "Sniper's Mark";
                SType = "Buff";
                description = $"This creature is marked Greo. Anyone who hits him will receive additional accuracy.";
            }
            else
            {
                nameText = "����� ��������";
                SType = "����������� ����������";
                description = $"��� �������� �������� ����. ����� ��� ��� ������ ������� �������������� ��������.";
            }
        }
        else if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Sniper's Mark";
                SType = "Buff";
                description = $"Greo tags the opponent. Anyone who hits him will receive additional accuracy.\r\nEnergy required: 3\nDuration: 2\nAccuracy: +{Value}";
            }
            else
            {
                nameText = "����� ��������";
                SType = "����������� ����������";
                description = $"���� �������� ����������. ����� ��� ��� ������ ������� �������������� ��������.\r\n����������� �������: 3\n������������: 2\n��������: +{Value}";
            }
        }
    }
    private void Cast(UnitProperties victim, UnitProperties from, List<Dictionary<string, int>> inpData)
    {
        for (int i = 0; i < inpData.Count; i++)
        {
            if (inpData[i]["side"] == parentUnit.ParentCircle.Side &&
                inpData[i]["place"] == parentUnit.ParentCircle.Place &&
                inpData[i]["debuffId"] == id)
            {
                UnitProperties unit = _characterPlacement.CirclesMap[inpData[i]["sideTarget"], inpData[i][$"placeTarget"]].ChildCharacter;
                //unit.Weapon.accuracy += inpData[i]["acc"];
                //if (unit.Weapon.accuracy > 100) unit.Weapon.accuracy = 100;
                GameObject newObj = Instantiate(Effect2, unit.PathBulletTarget.position, Quaternion.identity);
                if (PlayerData.language == 0) newObj.transform.Find("TextDamage/Text").GetComponent<TextMeshProUGUI>().text = $"+{inpData[i]["acc"]} ��������";
                else newObj.transform.Find("TextDamage/Text").GetComponent<TextMeshProUGUI>().text = $"+{inpData[i]["acc"]} Accuracy";
                newObj.transform.Find("TextDamage/Text").GetComponent<Animator>().SetTrigger("Alarm");
            }
        }
    }
    public override void EndDebuff()
    {
        Turns.punch -= Cast;
    }
}
