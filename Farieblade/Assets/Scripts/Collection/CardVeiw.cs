using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardVeiw : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _hp;
    [SerializeField] private TextMeshProUGUI _damage;
    [SerializeField] private TextMeshProUGUI _grade;
    [SerializeField] private TextMeshProUGUI _level;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private Image _icon;
    [SerializeField] private GameObject _damageImg;
    [SerializeField] private GameObject _HPImg;
    [SerializeField] private GameObject _gradeImg;
    [SerializeField] private GameObject _closed;
    [SerializeField] private Image _GradeImage;
    [SerializeField] private Sprite[] spriteGrade;
    [SerializeField] private Image _RangImage;

    [SerializeField] private Sprite[] spriteRang;

    [SerializeField] private Image _StateImage;
    [SerializeField] private Sprite[] _type;
    [SerializeField] private Image _Fraction;
    [SerializeField] private Sprite[] spriteFractions;
    [SerializeField] private Animator animator;
    public GameObject LvlupEffect;
    [SerializeField] private GameObject anIs;
    [SerializeField] private StringArray[] squad;
    [SerializeField] private TextMeshProUGUI achive;

    void Start() => SetCardValues();
    public void SetCardValues()
    {
        Unit _parent = gameObject.transform.parent.gameObject.GetComponent<Unit>();
        if (_parent.squad != -666) achive.text = squad[_parent.squad].intArray[PlayerData.language];
        _RangImage.sprite = spriteRang[_parent.rang];
        _GradeImage.sprite = spriteGrade[_parent.rang];
        _StateImage.sprite = _type[_parent.Type];
        _Fraction.sprite = spriteFractions[_parent.fraction];
        if (_parent.level == 0)
        {
            _damageImg.SetActive(false);
            _HPImg.SetActive(false);
            _gradeImg.SetActive(false);
            _icon.color = new Color(80 / 255.0f, 80 / 255.0f, 80 / 255.0f);
            _closed.SetActive(true);
            return;
        }
        if (_parent.state == 3 || _parent.state == 4) _damageImg.SetActive(false);
        else _damageImg.SetActive(true);
        _HPImg.SetActive(true);
        if (_parent.grade == 0) _gradeImg.SetActive(false);
        else _gradeImg.SetActive(true);
        _icon.color = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f);
        _closed.SetActive(false);
        if (gameObject.name != "CardClone")
        {
            _hp.text = Convert.ToString(_parent.hpBase);
            _damage.text = Convert.ToString(_parent.damage);
            _grade.text = Convert.ToString(_parent.grade);
            _level.text = Convert.ToString(_parent.level);
            if (gameObject.name == "Card" && _parent.onAnIs != -666) anIs.SetActive(true);
            else if (gameObject.name == "Card" && _parent.onAnIs == -666) anIs.SetActive(false);
            return;
        }
        _gradeImg.SetActive(false);
        _hp.text = Convert.ToString(_parent.hpBaseDefault);
        _damage.text = Convert.ToString(_parent.damageDefault);
        _level.text = "1";
        _name.text = transform.parent.transform.Find("Card/shell/Name").gameObject.GetComponent<TextMeshProUGUI>().text;
    }
}