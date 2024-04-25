using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MultiplayerDraft : MonoBehaviour
{
    public static int[,] units = new int[2, 6];
    public static int[,] unitsLevel = new int[2, 6];
    public static int[,] unitsGrade = new int[2, 6];
    public static int[,] unitsSkin = new int[2, 6];
    public static bool[] giant;

    public int[] troopAmount;
    public int[] totalPower;
    public static List<int> troops = new();
    public static Action exitMultiplayer;
    public GameObject[] particleLeft;
    public GameObject[] particleRight;
    [SerializeField] private Image coofBar;
    [SerializeField] private TextMeshProUGUI[] textAmount;
    [SerializeField] private TextMeshProUGUI[] textPower;
    [SerializeField] private Animator[] animatorPower;
    [SerializeField] private Animator[] animatorAmount;
    [SerializeField] private Animator animatorBar;
    [SerializeField] private TextMeshProUGUI textSpeaker;
    [SerializeField] private TextMeshProUGUI textTime;
    public GameObject blockContent;
    public GameObject[] blockCircle;
    [SerializeField] private Button toFight;
    [SerializeField] private TextMeshProUGUI[] textNick;
    [SerializeField] private Image[] portrait;
    [HideInInspector] public GameObject[] unit;
    [HideInInspector] public int ready = 0;
    [SerializeField] private AudioClip roll1;
    [SerializeField] private AudioClip roll2;
    [SerializeField] private GameObject[] leftCircles;
    [SerializeField] private GameObject[] rightCircles;
    [SerializeField] private Image[] bar;
    [SerializeField] private Image[] barTime;
    private bool[] workTime = {false, false };
    public float fillTime = 13f;
    public float fillSpeed;
    private GameObject[,] circles = new GameObject[2, 6];

    public Animator[] animator;
    public Image[] plank;
    public Image[] frames;
    public AudioClip put1;
    public AudioClip put2;
    public AudioClip StartClick;
    public float Coof2 = 0.5f;
    private bool work = false;
    [SerializeField] private GameObject MultiplayerBG;
    private int pickTurn = 0;
    private bool next = false;
    public BattleNetwork network;
    [SerializeField] private StartIni startIni;
    [SerializeField] private MusicFight music;

    public GameObject clonePrefub;
    public GameObject[] clone;
    public GameObject[] field1;
    private GameObject[] localArray = new GameObject[6];
    private void OnEnable()
    {
        fillSpeed = 1f / fillTime;
        if (BattleNetwork.sideOnBattle == 0) localArray = particleLeft;
        else localArray = particleRight;
        giant = new bool[2] { false, false };
        unit = new GameObject[2] { null, null };
        for (int i = 0; i < 6; i++)
        {
            circles[0, i] = leftCircles[i];
            circles[1, i] = rightCircles[i];
        }
        Sound.sound.PlayOneShot(StartClick);
        for (int i = 0; i < 6; i++)
        {
            units[0, i] = -666;
            units[1, i] = -666;
        }
        //Campany.enemyNick = GetComponent<NickNames>().nick[Random.Range(0, GetComponent<NickNames>().nick.Length)];
        if (BattleNetwork.sideOnBattle == 0)
        {
            bar[0].color = new(0f, 0f, 1f);
            bar[1].color = new(1f, 0f, 0f);
            frames[0].color = new Color(0f, 0.458f, 1f); // Синий цвет
            frames[1].color = new Color(1f, 0.051f, 0f); // Красный цвет
            plank[0].color = new Color(0f, 0.458f, 1f); // Синий цвет
            plank[1].color = new Color(1f, 0.051f, 0f); // Красный цвет
            textNick[0].text = PlayerData.nick;
            textNick[1].text = Campany.enemyNick;
            portrait[0].sprite = PlayerData.accountPortraitIndex[PlayerData.portrait];
            portrait[1].sprite = PlayerData.accountPortraitIndex[Campany.enemyPortraitInt];
        }
        else
        {
            bar[1].color = new(0f, 0f, 1f);
            bar[0].color = new(1f, 0f, 0f);
            frames[1].color = new Color(0f, 0.458f, 1f); // Синий цвет
            frames[0].color = new Color(1f, 0.051f, 0f); // Красный цвет
            plank[1].color = new Color(0f, 0.458f, 1f); // Синий цвет
            plank[0].color = new Color(1f, 0.051f, 0f); // Красный цвет
            textNick[1].text = PlayerData.nick;
            textNick[0].text = Campany.enemyNick;
            portrait[1].sprite = PlayerData.accountPortraitIndex[PlayerData.portrait];
            portrait[0].sprite = PlayerData.accountPortraitIndex[Campany.enemyPortraitInt];
        }

        textAmount[0].text = "0";
        textPower[0].text = "0";
        textAmount[1].text = "0";
        textPower[1].text = "0";
        StartCoroutine(Draft());
    }

    public IEnumerator Draft()
    {
        Multiplayer.win = false;
        PlayerData.rankBefore = PlayerData.rank;
        PlayerData.rank -= 23;
        if(PlayerData.rank < 0) PlayerData.rank = 0;
        PlayerData.afterFight = 23;

        blockCircle[BattleNetwork.sideOnBattle].SetActive(false);
        blockContent.SetActive(true);
        while(pickTurn < 4)
        {
            yield return new WaitForSeconds(1f);
            for (int i = 0; i < 2; i++)
            {
                animator[i].SetTrigger("on");
                unit[i] = null;
                if (giant[i] == true)
                {
                    workTime[i] = false;
                    if (i == BattleNetwork.sideOnBattle) blockContent.SetActive(true);
                    giant[i] = false;
                    animator[i].SetTrigger("done");
                }
                else
                {
                    workTime[i] = true;
                    if (i == BattleNetwork.sideOnBattle) blockContent.SetActive(false);
                }
            }
            TextTurn(pickTurn);
            //=============================================================
            while (!next) yield return null;
            //=============================================================
            animator[0].SetTrigger("off");
            animator[1].SetTrigger("off");
            yield return new WaitForSeconds(0.5f);
            int rand2 = Random.Range(0, 2);
            if (rand2 == 0)
            {
                if (unit[0] != null)
                {
                    unit[0].GetComponent<Animator>().SetTrigger("roll");
                    Sound.sound.PlayOneShot(roll1);
                }
                yield return new WaitForSeconds(0.2f);
                if (unit[1] != null)
                {
                    unit[1].GetComponent<Animator>().SetTrigger("roll");
                    Sound.sound.PlayOneShot(roll2);
                }
            }
            else
            {
                if (unit[1] != null)
                {
                    Sound.sound.PlayOneShot(roll2);
                    unit[1].GetComponent<Animator>().SetTrigger("roll");
                }
                yield return new WaitForSeconds(0.2f);
                if (unit[0] != null)
                {
                    Sound.sound.PlayOneShot(roll1);
                    unit[0].GetComponent<Animator>().SetTrigger("roll");
                }
            }
            yield return new WaitForSeconds(0.2f);
            int count = 0;
            for (int i = 0; i < 2; i++)
            {
                if (giant[i] == true)
                {
                    count++;
                    CreateGiant(unit[i], i);
                    yield return new WaitForSeconds(0.2f);
                }
            }
            yield return new WaitForSeconds(0.5f);
            SetCoof();
            work = true;
            yield return new WaitForSeconds(1f);
            work = false;
            next = false;
            if(count == 2)
            {
                pickTurn += 2;
                giant[0] = false;
                giant[1] = false;
            }
            else pickTurn++;
            print(pickTurn);
        }
        animator[0].SetTrigger("end");
        animator[1].SetTrigger("end");
        textSpeaker.text = "Please stand by...";
        yield return new WaitForSeconds(1f);
        startIni.Start2();
        while (BattleNetwork.connected == false)
            yield return null;
        gameObject.SetActive(false);
        music.Start2();
    }
    public void Exit()
    {
        troops.Clear();
        LoadingManager.LoadingScreenAfter = Random.Range(0, LoadingManager.LoadingScreenAfterSprite.Length);
        LoadingManager.LoadingScreenAfterImage.sprite = LoadingManager.LoadingScreenAfterSprite[LoadingManager.LoadingScreenAfter];

        LoadingManager.LoadingScreenText = Random.Range(0, LoadingManager.LoadingScreenAfterStringRus.Length);
        if (PlayerData.language == 0) LoadingManager.textLoadingScreenText.text = LoadingManager.LoadingScreenAfterStringEng[LoadingManager.LoadingScreenText];
        else if (PlayerData.language == 1) LoadingManager.textLoadingScreenText.text = LoadingManager.LoadingScreenAfterStringRus[LoadingManager.LoadingScreenText];
    }
    private void TextTurn(int i)
    {
        if (i == 0)
        {
            if (PlayerData.language == 0) textSpeaker.text = "First selection stage";
            else if (PlayerData.language == 1) textSpeaker.text = "Первая стадия выбора";
        }
        else if (i == 1)
        {
            if (PlayerData.language == 0) textSpeaker.text = "Second selection stage";
            else if (PlayerData.language == 1) textSpeaker.text = "Вторая стадия выбора";
        }
        else if (i == 2)
        {
            if (PlayerData.language == 0) textSpeaker.text = "Third selection stage";
            else if (PlayerData.language == 1) textSpeaker.text = "Третья стадия выбора";
        }
        else
        {
            if (PlayerData.language == 0) textSpeaker.text = "Last selection stage";
            else if (PlayerData.language == 1) textSpeaker.text = "Последняя стадия выбора";
        }
    }
/*    private IEnumerator ComputerChoose(int turn)
    {
        yield return new WaitForSeconds(Random.Range(1, 5));
        GetComponent<MultiplayerRankFinder>().FindRank(turn);
        //ready++;
        animator[1].SetTrigger("done");
    }*/
    public void SetCoof()
    {
        for (int i = 0; i < 2; i++) 
        {
            textAmount[i].text = Convert.ToString(troopAmount[i]) + " / 4";
            animatorAmount[i].SetTrigger("on");
            textPower[i].text = Convert.ToString(totalPower[i]);
            animatorPower[i].SetTrigger("on");
        }
       
        float coof;
        float coof2;
        if (totalPower[1] != 0)
        {
            if (totalPower[0] > totalPower[1])
            {
                coof = (totalPower[1] * 100) / totalPower[0];
                coof2 = (100 - coof) / 100;
                Coof2 = 0.5f + coof2;
            }
            else
            {
                coof = (totalPower[0] * 100) / totalPower[1];
                coof2 = (100 - coof) / 100;
                Coof2 = 0.5f - coof2;
            }
        }
    }
    private void FixedUpdate()
    {
        if(work == true)
            coofBar.fillAmount = Mathf.Lerp(coofBar.fillAmount, Coof2, 6f * Time.fixedDeltaTime);
        if (workTime[0] == true)
        {
            float newFillAmount = barTime[0].fillAmount - (fillSpeed * Time.fixedDeltaTime);
            barTime[0].fillAmount = Mathf.Clamp01(newFillAmount);
        }
        if (workTime[1] == true)
        {
            float newFillAmount = barTime[1].fillAmount - (fillSpeed * Time.fixedDeltaTime);
            barTime[1].fillAmount = Mathf.Clamp01(newFillAmount);
        }
    }
    public void SetUnit(int id, int side, int place, int level, int grade, int currentSkin, int pickTurn)
    {
        barTime[side].fillAmount = 1f;
        workTime[side] = false;
        if (id == -666 && unit[BattleNetwork.sideOnBattle] != null)
        {
            unit[BattleNetwork.sideOnBattle].transform.parent.GetComponent<UIDropHandlerMultiplayer>().newObject = null;
            unit[BattleNetwork.sideOnBattle].transform.SetParent(gameObject.transform.Find("Scroll View/Viewport/Content").gameObject.transform);
            unit[BattleNetwork.sideOnBattle].GetComponent<Animator>().SetTrigger("multiOff");
            unit[BattleNetwork.sideOnBattle] = null;
            blockCircle[BattleNetwork.sideOnBattle].SetActive(false);
            blockContent.SetActive(false);
            return;
        }
        animator[side].SetTrigger("done");
        print($"{id}, {side}, {place}, {level}, {pickTurn}");
        if (id != -555)
        {
            Unit newUnit;
            if (side != BattleNetwork.sideOnBattle)
            {
                GameObject obj = Instantiate(PlayerData.defaultCards[id], circles[side, place].transform);
                obj.transform.Find("Fight").gameObject.SetActive(false);
                obj.transform.Find("Card").gameObject.SetActive(true);
                //obj.GetComponent<Unit>().currentSkin = currentSkin;
                unit[side] = obj;
                obj.GetComponent<Animator>().SetTrigger("multi");
                int ran = Random.Range(0, 2);
                if (ran == 0) Sound.sound.PlayOneShot(put2);
                else Sound.sound.PlayOneShot(put1);
            }
            else blockCircle[BattleNetwork.sideOnBattle].SetActive(false);
            newUnit = unit[side].GetComponent<Unit>();
            Destroy(unit[side].transform.Find("Card").GetComponent<UIDragHandlerMultiplayer>());
            newUnit.level = level;
            newUnit.grade = grade;
            newUnit.SetValues();
            newUnit.transform.Find("Card").GetComponent<CardVeiw>().SetCardValues();

            totalPower[side] += Convert.ToInt32(newUnit.Power);
            if (newUnit.Type == 3)
            {
                giant[side] = true;
                troopAmount[side] += 2;
            }
            else troopAmount[side] += 1;
        }
        print($"Наш - {this.pickTurn}, Входящий - {pickTurn}");
        if (this.pickTurn != pickTurn) next = true;
    }
    public void CreateGiant(GameObject obj, int side)
    {
        int ran = Random.Range(0, 2);
        if (ran == 0) Sound.sound.PlayOneShot(put2);
        else Sound.sound.PlayOneShot(put1);
        GameObject newObj;
        newObj = Instantiate(clonePrefub, field1[side].transform);
        newObj.transform.Find("Card/IconMask/Icon").gameObject.GetComponent<Image>().sprite = obj.transform.Find("Card/IconMask/Icon").gameObject.GetComponent<Image>().sprite;
        clone[side] = newObj;
        newObj.GetComponent<Animator>().SetTrigger("new");
    }
    public void ShineSlot(int state)
    {
        if (state == 0)
        {
            for (int i = 0; i < 6; i += 2) if (units[BattleNetwork.sideOnBattle, i] == -666) localArray[i].SetActive(true);
        }
        else if (state == 1 || state == 2 || state == 3 || state == 4)
        {
            for (int i = 1; i < 6; i += 2) if (units[BattleNetwork.sideOnBattle, i] == -666) localArray[i].SetActive(true);
        }
    }
    public void HideSlot()
    {
        for(int i = 0; i < 6; i++) localArray[i].SetActive(false);
    }
    public void GameStop()
    {
        workTime[0] = false;
        workTime[1] = false;
    }
}