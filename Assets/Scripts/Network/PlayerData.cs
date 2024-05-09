using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerData : MonoBehaviour
{
    public static List<int> troops = new List<int>();
    public static Sprite accountPortrait;
    public static Sprite[] accountPortraitIndex;
    public static string nick;
    public static int[] troop = new int[6];
    public static int troopAmount;

    public static int combatView;
    public static int ai;
    public static int sorting;
    public static int accountLevel;
    public static int accountExp;
    public static int accountExpNeed;
    public static int portrait;
    public static int rank;
    public static int rankBefore;
    public static int strUnitId;
    public static int totalPower;
    public static int totalLevel;
    public static int totalGrade;
    public static int freeDf;
    public static int language;
    public static int league;

    public static GameObject lostConnection;

    public static GameObject floatNote;
    public static GameObject warning;
    public static GameObject auth;
    public static GameObject[] unitFields;
    public static GameObject[] defaultCards;
    //public static GameObject[] myCollection;

    public static TextMeshProUGUI textWarning;
    private static TextMeshProUGUI textGold;
    private static TextMeshProUGUI textAf;
    public static bool focus;

    [SerializeField] private GameObject AuthPrefub;
    [SerializeField] private GameObject WarningPrefub;
    [SerializeField] private GameObject LostConnectionPrefub;
    [SerializeField] private GameObject[] unitFieldsPrefub;
    [SerializeField] private GameObject FloatNotePrefub;
    [SerializeField] private GameObject anIs;
    [SerializeField] private GameObject gold_af;
    [SerializeField] private GameObject[] myUnits;
    [SerializeField] private GameObject[] DefaultCardsPrefub;
    [SerializeField] private GameObject Portrait;
    [SerializeField] private TextMeshProUGUI textWarningPrefub;
    [SerializeField] private TextMeshProUGUI textGoldPrefub;
    [SerializeField] private TextMeshProUGUI textAfPrefub;
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private TextMeshProUGUI textAccountLevel;
    [SerializeField] private DateTimeServer timeServer;
    [SerializeField] private Sprite[] accountPortraitPrefub;
    [SerializeField] private Image barExpAcc;
    [SerializeField] private ExpNeeder _expNeeder;

    [SerializeField] private GameObject _cardPrefub;
    [SerializeField] private Transform _tableCollection;
    [Inject] private DiContainer _diContainer;

    public void Init()
    {
        focus = false;
        unitFields = unitFieldsPrefub;
        auth = AuthPrefub;
        warning = WarningPrefub;
        textWarning = textWarningPrefub;

        floatNote = FloatNotePrefub;
        textGold = textGoldPrefub;
        textAf = textAfPrefub;
        defaultCards = DefaultCardsPrefub;
        accountPortraitIndex = accountPortraitPrefub;
        lostConnection = LostConnectionPrefub;
    }
    public IEnumerator AfterConnect()
    {
        PlayerDataBase playerDataBase = new();
        //Загрузка основных данных игрока
        yield return StartCoroutine(playerDataBase.GeneralSettings());

        //Загрузка уровней карт
        yield return StartCoroutine(playerDataBase.Levels());

        //Магазин
        yield return StartCoroutine(GetComponent<StoreCardsChange>().ChangeCards());

        GetComponent<StickerManager>().SetStikers();
        SetAmountSquad();
        AccountSet();
        GetComponent<AfterFightPlacePicker>().AfterFightScene();
        anIs.GetComponent<AnIsMap>().LocationsClose();
        GetComponent<LoadingManager>().SetLoading();

        yield return StartCoroutine(timeServer.LoadLocalData());
        focus = true;
        InvokeRepeating("TimeSec", 1, 1);
    }

    //Количество существ
    private void SetAmountSquad() 
    {
        combatView = PlayerPrefs.GetInt("CombatViewPrefs", 1);
        ai = PlayerPrefs.GetInt("ai", 1);
        troopAmount = 0;
/*        foreach (GameObject i in unitFields)
        {
            i.GetComponent<UIDropHandler>().Arrangement();
        }*/
        GetComponent<AdventureMap>().SetMap();
        GameObject card;
        for (int i = 0; i < IdUnit.idLevel.Length; i++)
        {
            if(_diContainer != null)
            {
                print(_cardPrefub);
                print(_tableCollection);
            }
            else
            {
                print("Хуй");
                return;
            }
            card = _diContainer.InstantiatePrefab(_cardPrefub, _tableCollection);
            card.GetComponent<CardVeiw>().Init(i, IdUnit.idLevel[i], IdUnit.idGrade[i], IdUnit.idExp[i], IdUnit.idOnAnIs[i]);
        }
        totalPower = 0;
/*        for (int i = 0; i< troop.Length; i++)
        {
            if (troop[i] != -666)
            {
                troops.Add(troop[i]);
                myCollection[troop[i]].GetComponent<Unit>().SetValues();
                totalPower += Convert.ToInt32(myCollection[troop[i]].GetComponent<Unit>().Power);
                totalLevel += myCollection[troop[i]].GetComponent<Unit>().level;
                totalGrade += myCollection[troop[i]].GetComponent<Unit>().grade;
            }
        }*/
    }

    //Настройка окна аккаунта
    private void AccountSet()
    {
        Portrait.GetComponent<Image>().sprite = accountPortraitIndex[portrait];
        textName.text = nick;
        accountExpNeed = _expNeeder.Exp[accountLevel - 1];
        float barValue = Convert.ToSingle(accountExp) * 100 / Convert.ToSingle(accountExpNeed);
        barExpAcc.fillAmount = barValue / 100;
        textAccountLevel.text = Convert.ToString(accountLevel);
        ChangeGoldAF();
    }


    public static void ChangeGoldAF()
    {
        textGold.text = Convert.ToString(Inventory.InventoryPlayer[23]);
        textAf.text = Convert.ToString(Inventory.InventoryPlayer[24]);
    }
    private void TimeSec()
    {
        DateTimeServer.serverTime += 1;
        StoreCardsChange.CardsChangeTimeSec++;
    }
}