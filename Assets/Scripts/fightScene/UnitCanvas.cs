using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textHP;
    [SerializeField] private TextMeshProUGUI _textDmg;
    [SerializeField] private Image _hpBar;
    [SerializeField] private Animator _hp;
    [SerializeField] private Animator _dmg;
    [SerializeField] private EnergyUnit energy;
    [SerializeField] private Gradient _gradient;
    private Unit parent;
    private int tempDamage;

/*    private void Start()
    {
        parent = gameObject.transform.parent.transform.parent.gameObject.GetComponent<Unit>();
        tempDamage = parent.damage;
    }*/
    public void UnitPropTextRenderer(float inpHp, float inpDamage, float hpProsent, string hpDmg)
    {
        if (hpDmg == "dmg")
            _dmg.SetTrigger("Alarm");
        else if (hpDmg == "hp")
            _hp.SetTrigger("Alarm");
        else if (hpDmg == "hpdmg")
        {
            _dmg.SetTrigger("Alarm");
            _hp.SetTrigger("Alarm");
        }
        /*            if (hpDmg != "none")
                    {
        *//*                if (unit.damage < tempDamage)       
                            _textDmg.color = new Color(255, 0, 0);
                        else if (unit.damage > tempDamage)  *//*
                            //_textDmg.color = new Color(0, 255, 0);
                        else                               
                            _textDmg.color = new Color(255, 255, 255);
                    }*/
        _textHP.text = Convert.ToString(inpHp);
        _hpBar.fillAmount = hpProsent /= 100;
        _hpBar.color = _gradient.Evaluate(hpProsent);
    }
}