using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoulSeekerVul : AbstractSpell
{
    public float value = 0.1f;
    [SerializeField] private GameObject debuff;
    void Start()
    {
        if (transform.parent.gameObject.name == "Debuffs")
        {
            value += parentUnit.Grade * 0.01f;
            parentUnit.transform.Find("ModeParticle1").gameObject.SetActive(true);
        }
        else if (transform.parent.gameObject.name == "Spells")
        {
            value += fromUnit.grade * 0.01f;
        }
        if (PlayerData.language == 0)
        {
            nameText = "Astral Explosion";
            SType = "Stuck debuff";
            description = $"The Soul Seeker places a curse on the enemy that increases incoming damage if they hit a vulnerable spot.\nDuration: 2\nVulnerability Damage: +{Convert.ToInt32(value * 100)}%";
        }
        else
        {
            nameText = "���������� �����";
            SType = "���������������� ���������";
            description = $"�������� ��� ����������� �� ����� ���������, ����������� �������� ���� � ������ ����� �� ��������� �����.\n������������: 2\n���� �� ��������� �����: +{Convert.ToInt32(value * 100)}%.";
        }
    }
    public override void EndDebuff()
    {
        parentUnit.transform.Find("ModeParticle1").gameObject.SetActive(false);
    }
    public override IEnumerator AfterStep(Dictionary<string, int> inpData)
    {
        UnitProperties unit = _characterPlacement.CirclesMap[inpData["sideTarget"], inpData["placeTarget"]].ChildCharacter;
        GameObject newObject = Instantiate(debuff, unit.PathDebuffs);
        ///newObject.GetComponent<AbstractSpell>().fromUnit = parentUnit.pathParent;
        yield return new WaitForSeconds(0.8f);
        Turns.finishEndEvent = true;
    }
}
