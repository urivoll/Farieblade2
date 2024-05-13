using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CardVeiw : MonoBehaviour
{
    public int Damage => _damage;
    public int Hp => _hp;
    public int Level => level;
    public int Id => id;
    public int Grade => grade;

    private int id;
    private int level;
    private int grade;
    private int exp;
    private int onAnIs;
    private int _damage;
    private int _hp;
    [SerializeField] private TextMeshProUGUI _hpText;
    [SerializeField] private TextMeshProUGUI _damageText;
    [SerializeField] private TextMeshProUGUI _gradeText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI achive;

    [SerializeField] private GameObject _damageImg;
    [SerializeField] private GameObject _HPImg;
    [SerializeField] private GameObject _gradeImg;
    [SerializeField] private GameObject _closed;
    [SerializeField] private GameObject anIs;

    [SerializeField] private Image _GradeImage;
    [SerializeField] private Image _RangImage;
    [SerializeField] private Image _icon;
    [SerializeField] private Image _StateImage;
    [SerializeField] private Image _Fraction;

    [SerializeField] private IDCaracter _character;
    [SerializeField] private Squads _squads;
    [Inject] private AbstractPanelProperties _panelProperties;

    public void Init(int id, int level, int grade, int exp = 0, int onAnIs = -666)
    {
        this.onAnIs = onAnIs;
        this.exp = exp;
        this.id = id;
        this.level = level;
        this.grade = grade;
        CharacterData character = _character.CharacterData[id];
        if (character == null) return;
        achive.text = _squads.Squad[character.Attributes.Squad].intArray[PlayerData.language];
        _name.text = character.Name[PlayerData.language];
        _icon.sprite = character.CardShell;
        _RangImage.sprite = character.Attributes.Rang.Frame;
        _GradeImage.sprite = character.Attributes.Rang.Icon;
        _StateImage.sprite = character.Attributes.Type.Icon;
        _Fraction.sprite = character.Attributes.Fraction.Icon;
        gameObject.name = character.Name[PlayerData.language];

        if (level == 0)
        {
            _damageImg.SetActive(false);
            _HPImg.SetActive(false);
            _icon.color = new Color(80 / 255.0f, 80 / 255.0f, 80 / 255.0f);
            _closed.SetActive(true);
            return;
        }
        if (grade == 0) _gradeImg.SetActive(false);
        else _gradeImg.SetActive(true);
        _HPImg.SetActive(true);
        _icon.color = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f);
        _closed.SetActive(false);
        _hp = character.Attributes.GetHp(level, grade);
        _damage = character.Attributes.GetHp(level, grade);
        _hpText.text = Convert.ToString(_hp);
        _damageText.text = Convert.ToString(_damage);
        _gradeText.text = Convert.ToString(grade);
        _levelText.text = Convert.ToString(level);
        if (this.onAnIs != -666) anIs.SetActive(true);
        else anIs.SetActive(false);
    }
    public void OpenPanelProperties()
    {
        Dictionary<string, int> data = new();
        data["id"] = id;
        data["level"] = level;
        data["grade"] = grade;
        data["exp"] = exp;
        _panelProperties.gameObject.SetActive(true);
        _panelProperties.GetComponent<AbstractPanelProperties>().SetValue(data);
    }
}