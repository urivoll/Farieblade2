using System;
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

    [SerializeField] private Animator animator;

    [SerializeField] private StringArray[] squad;

    [SerializeField] private IDCaracter _character;
    [SerializeField] private Squads _squads;
    [SerializeField] private AbstractPanelProperties panelProperties;

    public void Init(int id, int level, int grade)
    {
        CharacterData character = _character.CharacterData[id];
        //Unit _parent = gameObject.transform.parent.gameObject.GetComponent<Unit>();
        achive.text = _squads.Squad[character.Attributes.Squad].intArray[PlayerData.language];
        _RangImage.sprite = character.Attributes.Grade.Frame;
        _GradeImage.sprite = character.Attributes.Grade.Icon;
        _StateImage.sprite = character.Attributes.Type.Icon;
        _Fraction.sprite = character.Attributes.Fraction.Icon;
        gameObject.name = character.Name[PlayerData.language];
        if (level == 0)
        {
            _damageImg.SetActive(false);
            _HPImg.SetActive(false);
            _gradeImg.SetActive(false);
            _icon.color = new Color(80 / 255.0f, 80 / 255.0f, 80 / 255.0f);
            _closed.SetActive(true);
            return;
        }
        _HPImg.SetActive(true);
        if (grade == 0) _gradeImg.SetActive(false);
        else _gradeImg.SetActive(true);
        _icon.color = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f);
        _closed.SetActive(false);

        if (gameObject.name != "CardClone")
        {
            _hp.text = Convert.ToString(character.Attributes.GetHp(level, grade));
            _damage.text = Convert.ToString(character.Attributes.GetHp(level, grade));
            _grade.text = Convert.ToString(grade);
            _level.text = Convert.ToString(level);
            /*            if (gameObject.name == "Card" && _parent.onAnIs != -666) anIs.SetActive(true);
                        else if (gameObject.name == "Card" && _parent.onAnIs == -666) anIs.SetActive(false);*/
        }
        else
        {
            _gradeImg.SetActive(false);
            _hp.text = Convert.ToString(character.Attributes.Hp);
            _damage.text = Convert.ToString(character.Attributes.Damage);
            _level.text = "1";
            _name.text = transform.parent.transform.Find("Card/shell/Name").gameObject.GetComponent<TextMeshProUGUI>().text;
        }
    }
    public void OpenPanelProperties()
    {

    }
}
public interface IShowable
{
    public void Show(int id, int level, int grade, int exp);
}