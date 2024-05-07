using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelProperties : AbstractPanelProperties
{
    [SerializeField] private TextMeshProUGUI textHPBase;
    [SerializeField] private TextMeshProUGUI textDmg;
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private TextMeshProUGUI textAcc;
    [SerializeField] private TextMeshProUGUI textInit;
    [SerializeField] private TextMeshProUGUI textExp;
    [SerializeField] private TextMeshProUGUI textExpNeed;
    [SerializeField] private TextMeshProUGUI textLevel;
    [SerializeField] private TextMeshProUGUI textGrade;
    [SerializeField] private TextMeshProUGUI textRang;
    [SerializeField] private TextMeshProUGUI textType;
    [SerializeField] private TextMeshProUGUI textRole;
    [SerializeField] private TextMeshProUGUI textFraction;
    [SerializeField] private TextMeshProUGUI textDescription;
    [SerializeField] private TextMeshProUGUI textDescriptionFraction;
    [SerializeField] private TextMeshProUGUI textResist;
    [SerializeField] private TextMeshProUGUI textDamageType;
    [SerializeField] private TextMeshProUGUI textVulnerability;
    [SerializeField] private TextMeshProUGUI textFractionDesc;

    [SerializeField] private Image imageFractionDesc;
    [SerializeField] private Image imageBG;
    [SerializeField] private Image imageBGTrans;
    [SerializeField] private Image imageRang;
    [SerializeField] private Image imageType;
    [SerializeField] private Image imageRole;
    [SerializeField] private Image imageFraction;
    [SerializeField] private Image ImgDamageType;
    [SerializeField] private Image ImgResist;
    [SerializeField] private Image ImgVulnerability;
    [SerializeField] private Image _GradeImage;
    [SerializeField] private Image ImgDescription;
    [SerializeField] private Image[] modeListLocal;

    [SerializeField] private GameObject expPanel;
    [SerializeField] private GameObject _modePanel;
    [HideInInspector] public GameObject _avatarObject = null;
    [SerializeField] private GameObject[] spellListLocal;

    [SerializeField] private AudioClip _on;
    [SerializeField] private AudioClip _off;

    public static GameObject CurrentObj;

    [SerializeField] private StringArray[] arrayDesc;

    [SerializeField] private IDCaracter _character;

    [SerializeField] private ExpNeeder _expNeeder;

    public override void SetValue(int id, int level, int grade, int exp)
    {
        CharacterData character = _character.CharacterData[id];
        string maxLevelRus = "";
        string maxLevelEng = "";

        if (character.Presenter.Length > 0) Sound.voice.PlayOneShot(character.Presenter[UnityEngine.Random.Range(0, 2)]);

        Sound.amb.clip = character.Attributes.Fraction.Environment;
        Sound.amb.Play();
        Sound.amb.PlayOneShot(_on);
        //gameObject.SetActive(true);

        textName.text = character.Name[PlayerData.language];

        textDmg.text = Convert.ToString(character.Attributes.GetDamage(level, grade));
        textHPBase.text = Convert.ToString(character.Attributes.GetHp(level, grade));
        textInit.text = Convert.ToString(character.Attributes.Initiative);
        textAcc.text = Convert.ToString(character.Attributes.Accuracy);

        textDamageType.text = character.Attributes.DamageType.Name[PlayerData.language];
        ImgDamageType.sprite = character.Attributes.DamageType.Sprite;

        textResist.text = character.Attributes.Resist.Name[PlayerData.language];
        ImgResist.sprite = character.Attributes.Resist.Sprite;

        textVulnerability.text = character.Attributes.Vulnerability.Name[PlayerData.language];
        ImgVulnerability.sprite = character.Attributes.Vulnerability.Sprite;



        textRang.text = character.Attributes.Grade.Name[PlayerData.language];
        imageRang.sprite = character.Attributes.Grade.Icon;

        textType.text = character.Attributes.Type.Name[PlayerData.language];
        imageType.sprite = character.Attributes.Type.Icon;

        textRole.text = character.Attributes.Role.Name[PlayerData.language];
        imageRole.sprite = character.Attributes.Role.Icon;

        textFraction.text = character.Attributes.Fraction.Name[PlayerData.language];
        imageFraction.sprite = character.Attributes.Fraction.Icon;


        textLevel.text = Convert.ToString(level);
        textGrade.text = Convert.ToString(grade);


        imageBG.sprite = character.Attributes.Fraction.MenuBg;
        imageBGTrans.sprite = imageBG.sprite;

        if (PlayerData.language == 0)
        {
            textExpNeed.text = maxLevelEng;
        }
        else if (PlayerData.language == 1)
        {
            textExpNeed.text = maxLevelRus;
        }
        if (level == 60)
        {
            maxLevelEng = "Max";
            maxLevelRus = "Макс";
            textExp.text = " ";
        }
        else
        {
            textExpNeed.text = Convert.ToString(_expNeeder.Exp[level - 1]);
            textExp.text = Convert.ToString(exp);
        }
        //ShowSpells(obj);
        //CurrentObj = obj.gameObject;
        transform.Find("Avatar").gameObject.SetActive(true);
        GetComponent<PanelPropertiesInfo>().Start2();
        _avatarObject = Instantiate(character.Prefub, gameObject.transform.Find("Avatar"));

        ImgDescription.sprite = character.Portrait;
        textDescription.text = character.Description[PlayerData.language];
        textDescriptionFraction.text = character.Attributes.Fraction.Description[PlayerData.language];
        textFractionDesc.text = character.Attributes.Fraction.Name[PlayerData.language];
        imageFractionDesc.sprite = character.Attributes.Fraction.Icon;

        if (level == 0) expPanel.SetActive(false);
        else expPanel.SetActive(true);
        _GradeImage.sprite = character.Attributes.Grade.Icon;
    }
    public void Return()
    {
        if (_avatarObject != null)
            Destroy(_avatarObject);
        for (int i = 0; i < spellListLocal.Length; i++)
            spellListLocal[i].GetComponent<SkillSlot>().Off();
        Sound.amb.Stop();
        Sound.voice.Stop();
        _modePanel.SetActive(false);
    }
    private void ShowSpells(Unit obj)
    {
        if (obj.transform.Find("Fight/Model").gameObject.GetComponent<Spells>() == null) return;
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
        if (spells.modeList.Count <= 0) return;
        _modePanel.SetActive(true);
        for (int i = 0; i < spells.modeList.Count; i++)
        {
            Sprite image = spells.modeList[i].transform.Find("Mask/Pic").gameObject.GetComponent<Image>().sprite;
            modeListLocal[i].sprite = image;
        }
    }
}
