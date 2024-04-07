using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FatManArrow : AbstractSpell
{
    private float withProsent;
    [SerializeField] private GameObject debuff;
    void Start()
    {
        withProsent = prosentDamage * fromUnit.damage;

        if (transform.parent.gameObject.name == "Spells")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Titan Arrow";
                SType = "Ranged ability";
                description = $"The fat man assembles a titan arrow from old parts and throws it at the enemy, causing {Convert.ToInt32(withProsent)} damage. After a hit, the target has a 60% chance of being stunned.\r\nEnergy required: 2";
            }
            else
            {
                nameText = "������ �������";
                SType = "����������� ������� ���������";
                description = $"������� �������� �� ������ ������ ������ ������� � ������� �� � ����������, ������ {Convert.ToInt32(withProsent)} ��. �����. ����� ��������� ���� � ������ � 60% ������� ���������. \r\n����������� �������: 2";
            }
        }
    }
    public override IEnumerator AfterStep(Dictionary<string, int> inpData)
    {
        GameObject newObject = Instantiate(debuff, Turns.circlesMap[inpData["sideEnemy"], inpData["placeEnemy"]].newObject.pathDebuffs);
        newObject.GetComponent<AbstractSpell>().fromUnit = fromUnit;
        yield return new WaitForSeconds(0.5f);
        Turns.finishEndEvent = true;
    }
}
