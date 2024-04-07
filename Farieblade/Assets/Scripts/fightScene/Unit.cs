using System;
using UnityEngine;
public class Unit : MonoBehaviour
{
    public int ID;
    public int initiative;
    public int damageType;
    public int resist;
    public int vulnerability;
    public int accuracy;
    public int times;
    public int hpBase;
    public int damage;
    public int fraction;
    public int rang;
    public int Energy—onsumption;
    public int Type;
    public int expNeed;
    public int level;
    public int grade;
    public int exp;
    public int accuracyDefault;
    public float hpBaseDefault;
    public float damageDefault;
    public int state;
    public int squad;

    private GameObject _panel;
    public GameObject modelPanel;
    [HideInInspector] public GameObject card;
    [HideInInspector] public float expReward;
    public bool forSort = true;
    [HideInInspector] public int goldReward;
    public bool You;
    public float Power;
    public int onAnIs = 0;
    public int stateInGame;
    public bool onMultiplayer = false;
    [HideInInspector] public UnitProperties Model;
    public Sprite ImgDescription;
    public AudioClip[] presentAudio;

    private void Awake()
    {
        modelPanel = transform.Find("ModelPanel").gameObject;
        Model = transform.Find("Fight/Model").GetComponent<UnitProperties>();
        card = transform.Find("Card").gameObject;
        if (onMultiplayer == false)
        {
            if (GameObject.Find("draft") != null)
            {
                gameObject.transform.Find("Fight").gameObject.SetActive(true);
                gameObject.transform.Find("Card").gameObject.SetActive(false);
                _panel = StartIni.unitProperties;
            }
            else
            {
                gameObject.transform.Find("Fight").gameObject.SetActive(false);
                gameObject.transform.Find("Card").gameObject.SetActive(true);
                _panel = Camera.main.GetComponent<PanelPropertisMainMenu>().Panel;
            }
            SetValues();
        }
        else
        {
            gameObject.transform.Find("Fight").gameObject.SetActive(false);
            gameObject.transform.Find("Card").gameObject.SetActive(true);
            _panel = StartIni.unitProperties;
            level = IdUnit.idLevel[ID];
            grade = IdUnit.idGrade[ID];
        }
    }
    public void ShowUnitProperties()
    {
        if (GameObject.Find("draft") == null) _panel.GetComponent<PanelProperties>().SetValue(this);
        else _panel.GetComponent<PanelPropertiesFight>().SetValue(this);
    }
    public void SetValues()
    {
        int baseRang = 0;
        float tempHp = hpBaseDefault;
        float tempDamage = damageDefault;
        tempHp += hpBaseDefault * (0.2f * level);
        tempDamage += damageDefault * (0.1f * level);
        hpBase = Convert.ToInt32(tempHp + (tempHp * (0.4f * grade)));
        damage = Convert.ToInt32(tempDamage + (tempDamage * (0.4f * grade)));
        accuracy = accuracyDefault + grade;
        CountExp();
        if (rang == 0) baseRang = 75;
        else if (rang == 1) baseRang = 100;
        else if (rang == 2) baseRang = 125;
        else baseRang = 150;
        
        Power = baseRang * ((level - 1 / 2 + 1) * (1 + ((grade + 1) * 0.5f)));
        if (Type == 1) Power *= 2f;
    }
    public void CountExp()
    {
        float expNeedTemp = 0;
        if (level > 0 && level < 11)
        {
            if      (level == 1) expNeedTemp = 50;
            else if (level == 2) expNeedTemp = 200;
            else if (level == 3) expNeedTemp = 300;
            else if (level == 4) expNeedTemp = 500;
            else if (level == 5) expNeedTemp = 600;
            else if (level == 6) expNeedTemp = 800;
            else if (level == 7) expNeedTemp = 1100;
            else if (level == 8) expNeedTemp = 1400;
            else if (level == 9) expNeedTemp = 1700;
            else if (level == 10) expNeedTemp = 2000;
        }
        if (level > 10 && level < 21)
        {
            if      (level == 11) expNeedTemp = 2400;
            else if (level == 12) expNeedTemp = 2900;
            else if (level == 13) expNeedTemp = 3500;
            else if (level == 14) expNeedTemp = 3800;
            else if (level == 15) expNeedTemp = 4300;
            else if (level == 16) expNeedTemp = 4800;
            else if (level == 17) expNeedTemp = 5400;
            else if (level == 18) expNeedTemp = 6000;
            else if (level == 19) expNeedTemp = 6600;
            else if (level == 20) expNeedTemp = 7200;
        }
        if (level > 20 && level < 31)
        {
            if      (level == 21) expNeedTemp = 8000;
            else if (level == 22) expNeedTemp = 8500;
            else if (level == 23) expNeedTemp = 9200;
            else if (level == 24) expNeedTemp = 9600;
            else if (level == 25) expNeedTemp = 12000;
            else if (level == 26) expNeedTemp = 12900;
            else if (level == 27) expNeedTemp = 13600;
            else if (level == 28) expNeedTemp = 14500;
            else if (level == 29) expNeedTemp = 15200;
            else if (level == 30) expNeedTemp = 16000;
        }
        if (level > 30 && level < 41)
        {
            if      (level == 31) expNeedTemp = 17000;
            else if (level == 32) expNeedTemp = 18000;
            else if (level == 33) expNeedTemp = 19000;
            else if (level == 34) expNeedTemp = 20100;
            else if (level == 35) expNeedTemp = 21400;
            else if (level == 36) expNeedTemp = 22600;
            else if (level == 37) expNeedTemp = 24000;
            else if (level == 38) expNeedTemp = 25500;
            else if (level == 39) expNeedTemp = 27200;
            else if (level == 40) expNeedTemp = 28600;
        }
        if (level > 40 && level < 51)
        {
            if      (level == 41) expNeedTemp = 30000;
            else if (level == 42) expNeedTemp = 32000;
            else if (level == 43) expNeedTemp = 34000;
            else if (level == 44) expNeedTemp = 36100;
            else if (level == 45) expNeedTemp = 38400;
            else if (level == 46) expNeedTemp = 41000;
            else if (level == 47) expNeedTemp = 43200;
            else if (level == 48) expNeedTemp = 46500;
            else if (level == 49) expNeedTemp = 49200;
            else if (level == 50) expNeedTemp = 52600;
        }
        if (level > 50 && level < 61)
        {
            if      (level == 51) expNeedTemp = 55000;
            else if (level == 52) expNeedTemp = 58000;
            else if (level == 53) expNeedTemp = 54000;
            else if (level == 54) expNeedTemp = 57100;
            else if (level == 55) expNeedTemp = 62400;
            else if (level == 56) expNeedTemp = 66000;
            else if (level == 57) expNeedTemp = 70200;
            else if (level == 58) expNeedTemp = 74500;
            else if (level == 59) expNeedTemp = 78200;
            else if (level == 60) expNeedTemp = 83600;
        }
        if (rang == 0) expNeed = Convert.ToInt32(expNeedTemp / 1.75f);
        else if (rang == 1) expNeed = Convert.ToInt32(expNeedTemp / 1.5f);
        else if (rang == 2) expNeed = Convert.ToInt32(expNeedTemp / 1.25f);
        else if (rang == 3) expNeed = Convert.ToInt32(expNeedTemp / 1f);
    }
}
