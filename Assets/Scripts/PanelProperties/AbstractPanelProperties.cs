using Spine.Unity;
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
        _character = _IDcharacter.CharacterData[data["id"]];
        textName.text = _character.Name[PlayerData.language];

        textDmg.text = Convert.ToString(_character.Attributes.GetDamage(data["level"], data["grade"]));
        textHPBase.text = Convert.ToString(_character.Attributes.GetHp(data["level"], data["grade"]));
        textInit.text = Convert.ToString(_character.Attributes.Initiative);
        textAcc.text = Convert.ToString(_character.Attributes.Accuracy);

        textLevel.text = Convert.ToString(data["level"]);
        textGrade.text = Convert.ToString(data["grade"]);

        textType.text = _character.Attributes.Type.Name[PlayerData.language];
        imageType.sprite = _character.Attributes.Type.Icon;

        ImgDamageType.sprite = _character.Attributes.DamageType.Icon;
        textDamageType.text = _character.Attributes.DamageType.Name[PlayerData.language];

        ImgResist.sprite = _character.Attributes.Resist.Icon;
        textResist.text = _character.Attributes.Resist.Name[PlayerData.language];

        ImgVulnerability.sprite = _character.Attributes.Vulnerability.Icon;
        textVulnerability.text = _character.Attributes.Vulnerability.Name[PlayerData.language];

        textRang.text = _character.Attributes.Rang.Name[PlayerData.language];
        imageRang.sprite = _character.Attributes.Rang.Icon;

        imageFraction.sprite = _character.Attributes.Fraction.Icon;
        textFraction.text = _character.Attributes.Fraction.Name[PlayerData.language];

        _avatarObject = Instantiate(_character.Prefub, Avat.transform);
        _avatarObject.transform.Find("Shade").GetComponent<SpriteRenderer>().sortingLayerName = "TopUI";
        _avatarObject.transform.Find("Shade").GetComponent<SpriteRenderer>().sortingOrder = 3;
        _avatarObject.GetComponent<SkeletonPartsRenderer>().MeshRenderer.sortingLayerName = "TopUI";
        _avatarObject.GetComponent<SkeletonPartsRenderer>().MeshRenderer.sortingOrder = 3;
        _avatarObject.transform.localScale = new Vector2(35, 35);
    }
}