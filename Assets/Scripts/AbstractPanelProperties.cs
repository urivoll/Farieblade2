using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbstractPanelProperties : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI textHPBase;
    [SerializeField] protected TextMeshProUGUI textDmg;
    [SerializeField] protected TextMeshProUGUI textName;
    [SerializeField] protected TextMeshProUGUI textAcc;
    [SerializeField] protected TextMeshProUGUI textInit;
    [SerializeField] protected TextMeshProUGUI textLevel;
    [SerializeField] protected TextMeshProUGUI textGrade;
    [SerializeField] protected TextMeshProUGUI textRang;
    [SerializeField] protected TextMeshProUGUI textFraction;
    [SerializeField] protected TextMeshProUGUI textResist;
    [SerializeField] protected TextMeshProUGUI textDamageType;
    [SerializeField] protected TextMeshProUGUI textVulnerability;
    [SerializeField] protected TextMeshProUGUI textType;

    [SerializeField] protected Image imageBG;
    [SerializeField] protected Image imageRang;
    [SerializeField] protected Image imageType;
    [SerializeField] protected Image imageFraction;
    [SerializeField] protected Image ImgDamageType;
    [SerializeField] protected Image ImgResist;
    [SerializeField] protected Image ImgVulnerability;

    [SerializeField] protected AudioClip _on;
    [SerializeField] protected AudioClip _off;

    [SerializeField] protected GameObject Avat;

    protected GameObject _avatarObject;

    protected CharacterData _character;

    [SerializeField] private IDCaracter _IDcharacter;

    public virtual void SetValue(Dictionary<string, int> data) 
    {
        CharacterData character = _IDcharacter.CharacterData[data["id"]];
        textName.text = character.Name[PlayerData.language];

        textDmg.text = Convert.ToString(character.Attributes.GetDamage(data["level"], data["grade"]));
        textHPBase.text = Convert.ToString(character.Attributes.GetHp(data["level"], data["grade"]));
        textInit.text = Convert.ToString(character.Attributes.Initiative);
        textAcc.text = Convert.ToString(character.Attributes.Accuracy);

        textLevel.text = Convert.ToString(data["level"]);
        textGrade.text = Convert.ToString(data["grade"]);

        textType.text = character.Attributes.Type.Name[PlayerData.language];
        imageType.sprite = character.Attributes.Type.Icon;

        ImgDamageType.sprite = character.Attributes.DamageType.Icon;
        textDamageType.text = character.Attributes.DamageType.Name[PlayerData.language];

        ImgResist.sprite = character.Attributes.Resist.Icon;
        textResist.text = character.Attributes.Resist.Name[PlayerData.language];

        ImgVulnerability.sprite = character.Attributes.Vulnerability.Icon;
        textVulnerability.text = character.Attributes.Vulnerability.Name[PlayerData.language];

        textRang.text = character.Attributes.Rang.Name[PlayerData.language];
        imageRang.sprite = character.Attributes.Rang.Icon;

        imageFraction.sprite = character.Attributes.Fraction.Icon;
        textFraction.text = character.Attributes.Fraction.Name[PlayerData.language];

        _avatarObject = Instantiate(character.Prefub, Avat.transform);
    }
}