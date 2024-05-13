using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelProperties : AbstractPanelProperties
{
    [SerializeField] private TextMeshProUGUI textFractionDesc;
    [SerializeField] private TextMeshProUGUI textExp;
    [SerializeField] private TextMeshProUGUI textExpNeed;
    [SerializeField] private TextMeshProUGUI textDescription;
    [SerializeField] private TextMeshProUGUI textDescriptionFraction;
    [SerializeField] private TextMeshProUGUI textRole;

    [SerializeField] private Image _GradeImage;
    [SerializeField] private Image ImgDescription;
    [SerializeField] private Image[] modeListLocal;
    [SerializeField] private Image imageRole;
    [SerializeField] private Image imageFractionDesc;
    [SerializeField] private Image imageBGTrans;

    [SerializeField] private GameObject expPanel;
    [SerializeField] private GameObject _modePanel;
    [SerializeField] private GameObject[] spellListLocal;

    public static GameObject CurrentObj;

    [SerializeField] private StringArray[] arrayDesc;

    [SerializeField] private ExpNeeder _expNeeder;

    public override void SetValue(Dictionary<string, int> data)
    {
        base.SetValue(data);
        string maxLevelRus = "";
        string maxLevelEng = "";
        if (_character.Presenter.Length > 0) 
            Sound.voice.PlayOneShot(_character.Presenter[UnityEngine.Random.Range(0, 2)]);
        Sound.amb.clip = _character.Attributes.Fraction.Environment;
        Sound.amb.Play();
        Sound.amb.PlayOneShot(_on);
        textRole.text = _character.Attributes.Role.Name[PlayerData.language];
        imageRole.sprite = _character.Attributes.Role.Icon;

        imageBG.sprite = _character.Attributes.Fraction.MenuBg;
        imageBGTrans.sprite = imageBG.sprite;

        if (PlayerData.language == 0) textExpNeed.text = maxLevelEng;
        else if (PlayerData.language == 1) textExpNeed.text = maxLevelRus;
        expPanel.SetActive(true);

        if (data["level"] == 60)
        {
            maxLevelEng = "Max";
            maxLevelRus = "Макс";
            textExp.text = " ";
        }
        else if(data["level"] > 0)
        {
            textExpNeed.text = Convert.ToString(_expNeeder.Exp[data["level"] - 1]);
            textExp.text = Convert.ToString(data["exp"]);
            
        }
        else
        {
            expPanel.SetActive(false);
        }
        //ShowSpells(obj);
        //CurrentObj = obj.gameObject;
        transform.Find("Avatar").gameObject.SetActive(true);
        GetComponent<PanelPropertiesInfo>().Start2();

        ImgDescription.sprite = _character.Portrait;
        textDescription.text = _character.Description[PlayerData.language];
        textDescriptionFraction.text = _character.Attributes.Fraction.Description[PlayerData.language];
        textFractionDesc.text = _character.Attributes.Fraction.Name[PlayerData.language];
        imageFractionDesc.sprite = _character.Attributes.Fraction.Icon;
        _GradeImage.sprite = _character.Attributes.Rang.Icon;
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
