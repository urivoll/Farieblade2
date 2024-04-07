using Spine.Unity;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PanelPropertiesFight : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textHPBase;
    [SerializeField] private TextMeshProUGUI textHPFight;
    [SerializeField] private TextMeshProUGUI textDmg;
    [SerializeField] private TextMeshProUGUI textDmgFight;
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private TextMeshProUGUI textAcc;
    [SerializeField] private TextMeshProUGUI textAccFight;
    [SerializeField] private TextMeshProUGUI textInit;
    [SerializeField] private TextMeshProUGUI textInitFight;
    [SerializeField] private TextMeshProUGUI textLevel;
    [SerializeField] private TextMeshProUGUI textGrade;
    [SerializeField] private TextMeshProUGUI textRang;
    [SerializeField] private TextMeshProUGUI textFraction;
    [SerializeField] private TextMeshProUGUI textState;
    [SerializeField] private TextMeshProUGUI textResist;
    [SerializeField] private TextMeshProUGUI textDamageType;
    [SerializeField] private TextMeshProUGUI textVulnerability;

    [SerializeField] private Image imagePortrait;
    [SerializeField] private Image imageFramePortrait;
    [SerializeField] private Image imageFraction;
    [SerializeField] private Image ImgDamageType;
    [SerializeField] private Image ImgResist;
    [SerializeField] private Image ImgVulnerability;

    [SerializeField] private Sprite[] Fraction;
    [SerializeField] private Sprite[] BG;
    [SerializeField] private StringArray[] Fractions;
    [SerializeField] private StringArray[] ElementString;
    [SerializeField] private StringArray[] RangString;
    [SerializeField] private Sprite[] Element;
    [SerializeField] private Sprite[] Rang;
    [SerializeField] private StringArray[] States;

    [SerializeField] private EndFight _endFight;
    [SerializeField] private GameObject Avat;
    [HideInInspector] public GameObject _avatarObject = null;
    [SerializeField] private GameObject hpFightObj;
    [SerializeField] private GameObject dmgFightObj;
    [SerializeField] private GameObject accFightObj;
    [SerializeField] private GameObject initFightObj;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _on;
    [SerializeField] private AudioClip _off;

    [SerializeField] private Image _GradeImage;
    [SerializeField] private Sprite[] spriteGrade;
    public static GameObject CurrentObj;

    [SerializeField] private GameObject[] spellListLocal;
    public void SetValue(Unit obj)
    {
        Turns.unitChoose = obj.Model;
        _audioSource.PlayOneShot(_on);
        gameObject.SetActive(true);

        textHPBase.text = Convert.ToString(obj.hpBase);
        textDmg.text = Convert.ToString(obj.damage);
        textName.text = obj.transform.Find("Card/shell/Name").gameObject.GetComponent<LanguageText>().text[PlayerData.language];
        textAcc.text = Convert.ToString(obj.accuracy);
        textInit.text = Convert.ToString(obj.initiative);

        textLevel.text = Convert.ToString(obj.level);
        textGrade.text = Convert.ToString(obj.grade);

        ImgResist.sprite = Element[obj.resist];
        ImgVulnerability.sprite = Element[obj.vulnerability];
        ImgDamageType.sprite = Element[obj.damageType];

        textState.text = States[obj.state].intArray[PlayerData.language];
        textDamageType.text = ElementString[obj.damageType].intArray[PlayerData.language];
        textResist.text = ElementString[obj.resist].intArray[PlayerData.language];
        textVulnerability.text = ElementString[obj.vulnerability].intArray[PlayerData.language];
        textRang.text = RangString[obj.rang].intArray[PlayerData.language];
        //Фракции

        imageFraction.sprite = Fraction[obj.fraction];
        imagePortrait.sprite = BG[obj.fraction];
        textFraction.text = Fractions[obj.fraction].intArray[PlayerData.language];
        if (obj.transform.Find("Fight/Model").gameObject.GetComponent<Spells>() != null)
        {
            Spells spells = obj.transform.Find("Fight/Model").gameObject.GetComponent<Spells>();
            for (int i = 0; i < spells.SpellList.Count; i++)
            {
                spellListLocal[i].SetActive(true);
                Sprite image = spells.SpellList[i].transform.Find("Mask/Pic").gameObject.GetComponent<Image>().sprite;
                SkillSlot slot = spellListLocal[i].GetComponent<SkillSlot>();
                if (spells.SpellList[i].GetComponent<AbstractSpell>().state == "Aura")
                {
                    slot.FrameAura.SetActive(true);
                    slot.picAura.sprite = image;
                }
                else if (spells.SpellList[i].GetComponent<AbstractSpell>().state == "Effect" || spells.SpellList[i].GetComponent<AbstractSpell>().state == "Ball" ||
                    spells.SpellList[i].GetComponent<AbstractSpell>().state == "Melee" || spells.SpellList[i].GetComponent<AbstractSpell>().state == "nonTarget")
                {
                    slot.FrameActive.SetActive(true);
                    slot.picActive.sprite = image;
                }
                else if (spells.SpellList[i].GetComponent<AbstractSpell>().state == "Passive")
                {
                    slot.FramePassive.SetActive(true);
                    slot.picPassive.sprite = image;
                }
            }
        }
        if (GameObject.Find("Right0") != null)
        {
            imageFramePortrait.sprite = Rang[obj.rang];
            _avatarObject = Instantiate(obj.modelPanel, gameObject.transform.Find("Panel/Portrait/Avatar").gameObject.transform);
            _avatarObject.transform.Find("Shade3").GetComponent<SpriteRenderer>().sortingLayerName = "TopUI";
            _avatarObject.transform.Find("Shade3").GetComponent<SpriteRenderer>().sortingOrder = 3;
            _avatarObject.GetComponent<ModelPanel>().GetComponent<SkeletonPartsRenderer>().MeshRenderer.sortingLayerName = "TopUI";
            _avatarObject.GetComponent<ModelPanel>().GetComponent<SkeletonPartsRenderer>().MeshRenderer.sortingOrder = 3;
        }
        _avatarObject.SetActive(true);

        textHPFight.text = Convert.ToString(obj.Model.hp);
        textDmgFight.text = Convert.ToString(obj.Model.damage);
        textAccFight.text = Convert.ToString(obj.Model.accuracy);
        textInitFight.text = Convert.ToString(obj.Model.initiative);

        _GradeImage.sprite = spriteGrade[obj.rang];
    }
    public void ReturnAfterAnimation()
    {
        _audioSource.PlayOneShot(_off);
        Invoke("Return", 0.3f);
    }

    public void Return()
    {
        if (_avatarObject != null) Destroy(_avatarObject);

        for (int i = 0; i < spellListLocal.Length; i++) spellListLocal[i].GetComponent<SkillSlot>().Off();

        gameObject.SetActive(false);
    }
}
