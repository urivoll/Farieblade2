using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelProperties : MonoBehaviour
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
    [SerializeField] private TextMeshProUGUI textState;
    [SerializeField] private TextMeshProUGUI textRole;
    [SerializeField] private TextMeshProUGUI textFraction;

    [SerializeField] private TextMeshProUGUI textDescription;
    [SerializeField] private TextMeshProUGUI textDescriptionFraction;

    [SerializeField] private TextMeshProUGUI textResist;
    [SerializeField] private TextMeshProUGUI textDamageType;
    [SerializeField] private TextMeshProUGUI textVulnerability;
    [SerializeField] private TextMeshProUGUI textFractionDesc;
    [SerializeField] private Image imageFractionDesc;

    [SerializeField] private Image imagePortrait;
    [SerializeField] private Image imagePortraitTrans;

    [SerializeField] private Image imageRang;
    [SerializeField] private Image imageState;
    [SerializeField] private Image imageRole;
    [SerializeField] private Image imageFraction;
    [SerializeField] private Sprite[] state;
    [SerializeField] private Sprite[] role;
    [SerializeField] private Sprite[] Fraction;

    [SerializeField] private Image ImgDamageType;
    [SerializeField] private Image ImgResist;
    [SerializeField] private Image ImgVulnerability;

    [SerializeField] private Sprite[] BG;

    [SerializeField] private StringArray[] Fractions;
    [SerializeField] private StringArray[] ElementString;
    [SerializeField] private StringArray[] RangString;
    [SerializeField] private StringArray[] States;
    [SerializeField] private StringArray[] Roles;
    [SerializeField] private GameObject expPanel;

    [SerializeField] private Sprite[] Element;
    [SerializeField] private GameObject _exp;
    [SerializeField] private GameObject _grade;
    [SerializeField] private GameObject _level;
    [SerializeField] private GameObject _modePanel;
    [SerializeField] private Image[] modeListLocal;

    [SerializeField] private GameObject Avat;
    [HideInInspector] public GameObject _avatarObject = null;

    [SerializeField] private AudioClip _on;
    [SerializeField] private AudioClip _off;

    [SerializeField] private Image _GradeImage;
    [SerializeField] private Sprite[] spriteGrade;
    public static GameObject CurrentObj;
    [SerializeField] private Image ImgDescription;
    [SerializeField] private AudioClip[] environment;
    [SerializeField] private StringArray[] arrayDesc;
    [SerializeField] private StringArray[] arrayFract;

    [SerializeField] private GameObject[] spellListLocal;
    public void SetValue(Unit obj)
    {
        string maxLevelRus = "";
        string maxLevelEng = "";

        if (obj.presentAudio.Length > 0) Sound.voice.PlayOneShot(obj.presentAudio[UnityEngine.Random.Range(0, 2)]);

        Sound.amb.clip = environment[obj.fraction];
        Sound.amb.Play();
        Sound.amb.PlayOneShot(_on);
        gameObject.SetActive(true);

        textHPBase.text = Convert.ToString(obj.hpBase);
        textDmg.text = Convert.ToString(obj.damage);
        textName.text = obj.transform.Find("Card/shell/Name").gameObject.GetComponent<TextMeshProUGUI>().text;
        textAcc.text = Convert.ToString(obj.accuracy);
        textInit.text = Convert.ToString(obj.initiative);

        textLevel.text = Convert.ToString(obj.level);
        Turns.unitChoose = obj.GetComponent<Unit>().Model;

        textGrade.text = Convert.ToString(obj.grade);

        ImgResist.sprite = Element[obj.resist];
        ImgVulnerability.sprite = Element[obj.vulnerability];
        ImgDamageType.sprite = Element[obj.damageType];


        textDamageType.text = ElementString[obj.damageType].intArray[PlayerData.language];
        textResist.text = ElementString[obj.resist].intArray[PlayerData.language];
        textVulnerability.text = ElementString[obj.vulnerability].intArray[PlayerData.language];

        textRang.text = RangString[obj.rang].intArray[PlayerData.language];
        textState.text = States[obj.state].intArray[PlayerData.language];
        textRole.text = Roles[obj.Type].intArray[PlayerData.language];
        textFraction.text = Fractions[obj.fraction].intArray[PlayerData.language];
        imageRang.sprite = spriteGrade[obj.rang];
        if (obj.state == 0) imageState.sprite = this.state[0]; else imageState.sprite = this.state[1];
        imageRole.sprite = role[obj.Type];
        imageFraction.sprite = Fraction[obj.fraction];


        imagePortrait.sprite = BG[obj.fraction];
        imagePortraitTrans.sprite = BG[obj.fraction];

        if (PlayerData.language == 0)
        {
            textExpNeed.text = maxLevelEng;
        }
        else if (PlayerData.language == 1)
        {
            textExpNeed.text = maxLevelRus;
        }
        if (obj.level == 60)
        {
            maxLevelEng = "Max";
            maxLevelRus = "Макс";
            textExp.text = " ";
        }
        else
        {
            textExpNeed.text = Convert.ToString(obj.expNeed);
            textExp.text = Convert.ToString(obj.exp);
        }
        ShowSpells(obj);
        CurrentObj = obj.gameObject;
        gameObject.transform.Find("Avatar").gameObject.SetActive(true);
        GetComponent<PanelPropertiesInfo>().Start2();
        _avatarObject = Instantiate(obj.modelPanel, gameObject.transform.Find("Avatar"));
        if (obj.Type == 3) _avatarObject.transform.localScale = new(80, 80, 1);
        ImgDescription.sprite = obj.ImgDescription;
        textDescription.text = arrayDesc[obj.ID].intArray[PlayerData.language];
        textDescriptionFraction.text = arrayFract[obj.fraction].intArray[PlayerData.language];
        textFractionDesc.text = Fractions[obj.fraction].intArray[PlayerData.language];
        imageFractionDesc.sprite = Fraction[obj.fraction];

        //Скины
        transform.Find("Skins").GetComponent<SkinsManager>().SetButtons(obj.modelPanel.GetComponent<ModelPanel>());
        transform.Find("Skins").GetComponent<SkinsManager>().obj = CurrentObj;


        _avatarObject.SetActive(true);

        if (obj.level == 0) expPanel.SetActive(false);
        else expPanel.SetActive(true);
        _GradeImage.sprite = spriteGrade[obj.rang];
    }
    public void Return()
    {
        transform.Find("Skins").GetComponent<SkinsManager>().Exit();
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
