using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class PlayerData : MonoBehaviour
{

    public static List<int> troops = new List<int>();
    public static Sprite accountPortrait;
    public static Sprite[] accountPortraitIndex;
    public static string nick;
    public static int[] troop = new int[6];
    public static int troopAmount;
    public static int afterFight = 1;
    public static int combatView;
    public static int ai;
    public static int sorting;
    public static int accountLevel;
    public static int accountExp;
    public static int accountExpNeed;
    public static int traning = -1;
    public static int portrait;
    public static int rank;
    public static int rankBefore;
    public static int strUnitId;
    public static int totalPower;
    public static int totalLevel;
    public static int totalGrade;
    public static int maxLevel;
    public static int maxGrade;
    public static int minLevel;
    public static int minGrade;
    public static int freeDf;
    public static int language;
    public static int league;

    public static GameObject lostConnection;
    public static GameObject currentPlace;
    public static GameObject floatNote;
    public static GameObject warning;
    public static GameObject auth;
    public static GameObject[] unitFields;
    public static GameObject[] defaultCards;
    public static GameObject[] myCollection;

    public static TextMeshProUGUI textWarning;
    private static TextMeshProUGUI textGold;
    private static TextMeshProUGUI textAf;
    public static bool focus;

    [SerializeField] private GameObject AuthPrefub;
    [SerializeField] private GameObject WarningPrefub;
    [SerializeField] private GameObject LostConnectionPrefub;
    [SerializeField] private GameObject[] unitFieldsPrefub;
    [SerializeField] private GameObject deck;
    [SerializeField] private GameObject tavern;
    [SerializeField] private GameObject fight;
    [SerializeField] private GameObject FloatNotePrefub;
    [SerializeField] private GameObject coach;
    [SerializeField] private GameObject city;
    [SerializeField] private GameObject collections;
    [SerializeField] private GameObject anIs;
    [SerializeField] private GameObject adventure;
    [SerializeField] private GameObject multiplayer;
    [SerializeField] private GameObject gold_af;
    [SerializeField] private GameObject[] myUnits;
    [SerializeField] private GameObject[] DefaultCardsPrefub;
    [SerializeField] private GameObject Portrait;
    [SerializeField] private GameObject CityButton;
    [SerializeField] private GameObject CollectionButton;
    [SerializeField] private GameObject AnIsButton;
    [SerializeField] private GameObject AdventureButton;
    [SerializeField] private GameObject MultiplayerButton;
    [SerializeField] private TextMeshProUGUI textWarningPrefub;
    [SerializeField] private TextMeshProUGUI textGoldPrefub;
    [SerializeField] private TextMeshProUGUI textAfPrefub;
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private TextMeshProUGUI textAccountLevel;
    [SerializeField] private DateTimeServer timeServer;
    [SerializeField] private Sprite[] accountPortraitPrefub;
    [SerializeField] private Image barExpAcc;

    private void Awake()
    {
        focus = false;
        unitFields = unitFieldsPrefub;
        auth = AuthPrefub;
        warning = WarningPrefub;
        textWarning = textWarningPrefub;

        floatNote = FloatNotePrefub;
        textGold = textGoldPrefub;
        textAf = textAfPrefub;
        myCollection = myUnits;
        defaultCards = DefaultCardsPrefub;
        accountPortraitIndex = accountPortraitPrefub;
        lostConnection = LostConnectionPrefub;
    }
    public IEnumerator AfterConnect()
    {

        //Загрузка основных данных игрока
        var cor = StartCoroutine(GetComponent<PlayerDataBase>().GeneralSettings());
        yield return cor;

        //Загрузка уровней карт
        cor = StartCoroutine(GetComponent<PlayerDataBase>().Levels());
        yield return cor;

        //Магазин
        cor = StartCoroutine(GetComponent<StoreCardsChange>().ChangeCards());
        yield return cor;

        GetComponent<StickerManager>().SetStikers();
        SetAmountSquad();
        AccountSet();
        AfterFightScene();
        anIs.GetComponent<AnIsMap>().LocationsClose();
        GetComponent<LoadingManager>().SetLoading();

        cor = StartCoroutine(timeServer.LoadLocalData());
        yield return cor;
        focus = true;
        InvokeRepeating("TimeSec", 1, 1);
    }

    //Количество существ
    private void SetAmountSquad() 
    {
        if (PlayerPrefs.HasKey("CombatViewPrefs") == false)
        {
            PlayerPrefs.SetInt("CombatViewPrefs", 1);
            combatView = 1;
        }
        else combatView = PlayerPrefs.GetInt("CombatViewPrefs");
        if (PlayerPrefs.HasKey("ai") == false)
        {
            PlayerPrefs.SetInt("ai", 1);
            ai = 1;
        }
        else ai = PlayerPrefs.GetInt("ai");
        troopAmount = 0;
        foreach (GameObject i in unitFields)
        {
            i.GetComponent<UIDropHandler>().Arrangement();
        }
        GetComponent<AdventureMap>().SetMap();
        for (int i = 0; i < myCollection.Length; i++)
        {
            myCollection[i].GetComponent<Unit>().level = IdUnit.idLevel[i];
            myCollection[i].GetComponent<Unit>().grade = IdUnit.idGrade[i];
            myCollection[i].GetComponent<Unit>().exp = IdUnit.idExp[i];
            myCollection[i].GetComponent<Unit>().onAnIs = IdUnit.idOnAnIs[i];
        }
        int tempMaxLevel = 0;
        int tempMaxGrade = 0;
        int tempMinLevel = 70;
        int tempMinGrade = 70;
        totalPower = 0;
        for (int i = 0; i< troop.Length; i++)
        {
            if (troop[i] != -666)
            {
                if (myCollection[troop[i]].GetComponent<Unit>().level > tempMaxLevel) tempMaxLevel = myCollection[troop[i]].GetComponent<Unit>().level;
                if (myCollection[troop[i]].GetComponent<Unit>().grade > tempMaxGrade) tempMaxGrade = myCollection[troop[i]].GetComponent<Unit>().grade;
                if (myCollection[troop[i]].GetComponent<Unit>().level < tempMinLevel) tempMinLevel = myCollection[troop[i]].GetComponent<Unit>().level;
                if (myCollection[troop[i]].GetComponent<Unit>().grade < tempMinGrade) tempMinGrade = myCollection[troop[i]].GetComponent<Unit>().grade;
                troops.Add(troop[i]);
                myCollection[troop[i]].GetComponent<Unit>().SetValues();
                totalPower += Convert.ToInt32(myCollection[troop[i]].GetComponent<Unit>().Power);
                totalLevel += myCollection[troop[i]].GetComponent<Unit>().level;
                totalGrade += myCollection[troop[i]].GetComponent<Unit>().grade;
            }
        }
        maxLevel = tempMaxLevel;
        maxGrade = tempMaxGrade;
        minLevel = tempMinLevel;
        minGrade = tempMinGrade;
    }

    //Настройка окна аккаунта
    private void AccountSet()
    {
        Portrait.GetComponent<Image>().sprite = accountPortraitIndex[portrait];
        textName.text = nick;
        accountExpNeed = 100 + 50 * accountLevel;
        CountExp();
        float barValue = Convert.ToSingle(accountExp) * 100 / Convert.ToSingle(accountExpNeed);
        barExpAcc.fillAmount = barValue / 100;
        textAccountLevel.text = Convert.ToString(accountLevel);
        ChangeGoldAF();
    }
    //После битвы выборка локации
    private void AfterFightScene()
    {
        if (afterFight == 1)
        {
            currentPlace = city;
            city.SetActive(true);
        }
        else if (afterFight == 2)
        {
            currentPlace = adventure;
            adventure.SetActive(true);
        }
        else if (afterFight == 3)
        {
            currentPlace = collections;
            collections.SetActive(true);
        }
        else if (afterFight == 4)
        {
            currentPlace = city;
            tavern.SetActive(true);
        }
        else if (afterFight == 23)
        {
            currentPlace = multiplayer;
            multiplayer.GetComponent<Multiplayer>().after = true;
            multiplayer.SetActive(true);
            multiplayer.GetComponent<Multiplayer>().AfterFight();
        }
    }
    public void SetLayer()
    {
        currentPlace.SetActive(true);
    }
    public void SetLayer3()
    {
        currentPlace.SetActive(false);
    }
    public void SetLayer2(GameObject obj)
    {
        currentPlace = obj;
    }
    public static void ChangeGoldAF()
    {
        textGold.text = Convert.ToString(Inventory.InventoryPlayer[23]);
        textAf.text = Convert.ToString(Inventory.InventoryPlayer[24]);
    }
    public GameObject UnitCreate(int UnitId)
    {
        int count = 0;
        while (UnitId < myCollection.Length + 1)
        {
            count++;
            if (UnitId == count) return myCollection[count];
        }
        return null;
    }
    private void TimeSec()
    {
        DateTimeServer.serverTime += 1;
        StoreCardsChange.CardsChangeTimeSec++;
    }
    public static void CountExp()
    {
        int expNeedTemp = 0;
        if      (accountLevel == 1) expNeedTemp = 200;
        else if (accountLevel == 2) expNeedTemp = 250;
        else if (accountLevel == 3) expNeedTemp = 300;
        else if (accountLevel == 4) expNeedTemp = 500;
        else if (accountLevel == 5) expNeedTemp = 600;
        else if (accountLevel == 6) expNeedTemp = 800;
        else if (accountLevel == 7) expNeedTemp = 1100;
        else if (accountLevel == 8) expNeedTemp = 1400;
        else if (accountLevel == 9) expNeedTemp = 1700;
        else if (accountLevel == 10) expNeedTemp = 2000;

        else if (accountLevel == 11) expNeedTemp = 2400;
        else if (accountLevel == 12) expNeedTemp = 2900;
        else if (accountLevel == 13) expNeedTemp = 3500;
        else if (accountLevel == 14) expNeedTemp = 3800;
        else if (accountLevel == 15) expNeedTemp = 4300;
        else if (accountLevel == 16) expNeedTemp = 4800;
        else if (accountLevel == 17) expNeedTemp = 5400;
        else if (accountLevel == 18) expNeedTemp = 6000;
        else if (accountLevel == 19) expNeedTemp = 6600;
        else if (accountLevel == 20) expNeedTemp = 7200;

        else if (accountLevel == 21) expNeedTemp = 8000;
        else if (accountLevel == 22) expNeedTemp = 8500;
        else if (accountLevel == 23) expNeedTemp = 9200;
        else if (accountLevel == 24) expNeedTemp = 9600;
        else if (accountLevel == 25) expNeedTemp = 12000;
        else if (accountLevel == 26) expNeedTemp = 12900;
        else if (accountLevel == 27) expNeedTemp = 13600;
        else if (accountLevel == 28) expNeedTemp = 14500;
        else if (accountLevel == 29) expNeedTemp = 15200;
        else if (accountLevel == 30) expNeedTemp = 16000;

        else if (accountLevel == 31) expNeedTemp = 17000;
        else if (accountLevel == 32) expNeedTemp = 18000;
        else if (accountLevel == 33) expNeedTemp = 19000;
        else if (accountLevel == 34) expNeedTemp = 20100;
        else if (accountLevel == 35) expNeedTemp = 21400;
        else if (accountLevel == 36) expNeedTemp = 22600;
        else if (accountLevel == 37) expNeedTemp = 24000;
        else if (accountLevel == 38) expNeedTemp = 25500;
        else if (accountLevel == 39) expNeedTemp = 27200;
        else if (accountLevel == 40) expNeedTemp = 28600;

        else if (accountLevel == 41) expNeedTemp = 30000;
        else if (accountLevel == 42) expNeedTemp = 32000;
        else if (accountLevel == 43) expNeedTemp = 34000;
        else if (accountLevel == 44) expNeedTemp = 36100;
        else if (accountLevel == 45) expNeedTemp = 38400;
        else if (accountLevel == 46) expNeedTemp = 41000;
        else if (accountLevel == 47) expNeedTemp = 43200;
        else if (accountLevel == 48) expNeedTemp = 46500;
        else if (accountLevel == 49) expNeedTemp = 49200;
        else if (accountLevel == 50) expNeedTemp = 52600;

        else if (accountLevel == 51) expNeedTemp = 55000;
        else if (accountLevel == 52) expNeedTemp = 58000;
        else if (accountLevel == 53) expNeedTemp = 54000;
        else if (accountLevel == 54) expNeedTemp = 57100;
        else if (accountLevel == 55) expNeedTemp = 62400;
        else if (accountLevel == 56) expNeedTemp = 66000;
        else if (accountLevel == 57) expNeedTemp = 70200;
        else if (accountLevel == 58) expNeedTemp = 74500;
        else if (accountLevel == 59) expNeedTemp = 78200;
        else if (accountLevel == 60) expNeedTemp = 83600;
        accountExpNeed = expNeedTemp * 2;
    }
}