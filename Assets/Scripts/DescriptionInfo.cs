using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DescriptionInfo : MonoBehaviour
{
    [SerializeField] private string[] nameDamageType;
    [SerializeField] private string[] descDamageType;
    [SerializeField] private string[] nameResist;
    [SerializeField] private string[] descResist;
    [SerializeField] private string[] nameVul;
    [SerializeField] private string[] descVul;

    [SerializeField] private string[] nameRang;
    [SerializeField] private string[] descRang;
    [SerializeField] private string[] nameKind;
    [SerializeField] private string[] descKind;
    [SerializeField] private string[] nameFraction;
    [SerializeField] private string[] descFraction;

    [SerializeField] private string[] nameExp;
    [SerializeField] private string[] descExp;
    [SerializeField] private string[] nameLevel;
    [SerializeField] private string[] descLevel;
    [SerializeField] private string[] nameGrade;
    [SerializeField] private string[] descGrade;

    [SerializeField] private string[] nameHP;
    [SerializeField] private string[] descHP;
    [SerializeField] private string[] nameDamage;
    [SerializeField] private string[] descDamage;
    [SerializeField] private string[] nameInitiative;
    [SerializeField] private string[] descInitiative;

    [SerializeField] private TextMeshProUGUI textName1;
    [SerializeField] private TextMeshProUGUI textName2;
    [SerializeField] private TextMeshProUGUI textName3;
    [SerializeField] private TextMeshProUGUI textDesc1;
    [SerializeField] private TextMeshProUGUI textDesc2;
    [SerializeField] private TextMeshProUGUI textDesc3;
    public void SetDesc(int num)
    {
        if (num == 1)
        {
            textName1.text = nameHP[PlayerData.language];
            textName2.text = nameDamage[PlayerData.language];
            textName3.text = nameInitiative[PlayerData.language];
            textDesc1.text = descHP[PlayerData.language];
            textDesc2.text = descDamage[PlayerData.language];
            textDesc3.text = descInitiative[PlayerData.language];
        }
        else if (num == 2)
        {
            textName1.text = nameDamageType[PlayerData.language];
            textName2.text = nameResist[PlayerData.language];
            textName3.text = nameVul[PlayerData.language];
            textDesc1.text = descDamageType[PlayerData.language];
            textDesc2.text = descResist[PlayerData.language];
            textDesc3.text = descVul[PlayerData.language];
        }
        else if (num == 3)
        {
            textName1.text = nameRang[PlayerData.language];
            textName2.text = nameKind[PlayerData.language];
            textName3.text = nameFraction[PlayerData.language];
            textDesc1.text = descRang[PlayerData.language];
            textDesc2.text = descKind[PlayerData.language];
            textDesc3.text = descFraction[PlayerData.language];
        }
        else if (num == 4)
        {
            textName1.text = nameExp[PlayerData.language];
            textName2.text = nameLevel[PlayerData.language];
            textName3.text = nameGrade[PlayerData.language];
            textDesc1.text = descExp[PlayerData.language];
            textDesc2.text = descLevel[PlayerData.language];
            textDesc3.text = descGrade[PlayerData.language];
        }
    }
}
