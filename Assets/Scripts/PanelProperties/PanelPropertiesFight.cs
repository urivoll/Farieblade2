using Spine.Unity;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelPropertiesFight : AbstractPanelProperties
{
    [SerializeField] private TextMeshProUGUI textHPFight;
    [SerializeField] private TextMeshProUGUI textDmgFight;
    [SerializeField] private TextMeshProUGUI textAccFight;
    [SerializeField] private TextMeshProUGUI textInitFight;

    [SerializeField] private Image imageFramePortrait;

    [SerializeField] private StringArray[] Fractions;
    [SerializeField] private StringArray[] ElementString;
    [SerializeField] private StringArray[] RangString;

    [SerializeField] private EndFight _endFight;

    [SerializeField] private GameObject hpFightObj;
    [SerializeField] private GameObject dmgFightObj;
    [SerializeField] private GameObject accFightObj;
    [SerializeField] private GameObject initFightObj;
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private Sprite[] spriteGrade;
    public static GameObject CurrentObj;

    [SerializeField] private GameObject[] spellListLocal;
    public override void SetValue(Dictionary<string, int> data)
    {
        base.SetValue(data);

        if (!data.ContainsKey("id") ||
            !data.ContainsKey("level") ||
            !data.ContainsKey("grade")) 
            return;
        //Turns.unitChoose = obj;
        _audioSource.PlayOneShot(_on);
        gameObject.SetActive(true);

        imageBG.sprite = _character.Attributes.Fraction.CombatBg;

        /*        if (obj.GetComponent<Spells>() != null)
                {
                    Spells spells = obj.GetComponent<Spells>();
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
                        else if (spells.SpellList[i].GetComponent<AbstractSpell>().state == "Effect" || 
                            spells.SpellList[i].GetComponent<AbstractSpell>().state == "Ball" ||
                            spells.SpellList[i].GetComponent<AbstractSpell>().state == "Melee" || 
                            spells.SpellList[i].GetComponent<AbstractSpell>().state == "nonTarget")
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
                }*/
        imageFramePortrait.sprite = _character.Attributes.Rang.Frame;

        _avatarObject.SetActive(true);
        if (data.ContainsKey("damageFight"))
            textDmgFight.text = Convert.ToString(data["damageFight"]);
        if (data.ContainsKey("hpFight"))
            textHPFight.text = Convert.ToString(data["hpFight"]);
        if (data.ContainsKey("accuracyFight"))
            textAccFight.text = Convert.ToString(data["accuracyFight"]);
        if (data.ContainsKey("initiativeFight"))
            textInitFight.text = Convert.ToString(data["initiativeFight"]);
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
