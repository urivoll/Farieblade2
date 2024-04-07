using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class StartIni : MonoBehaviour
{
    public static Action setViewTrops;
    [SerializeField] private GameObject blockPrefub;
    public GameObject[] planks;

    [SerializeField] private Button buttonAIPrefub;
    [SerializeField] private Image viewLine;
    [SerializeField] private Sprite[] viewSprite;
    [SerializeField] private Image aiImage;
    [SerializeField] private Sprite[] aiSprite;
    [SerializeField] private Image[] playersFrame;

    public static GameObject unitProperties;
    public static GameObject debuffs;
    public static Button buttonAI;
    public static Image[] playersBars;
    public static BattleVoice soundVoice;
    public static BattleVoice sound;
    public static Action cardRayCastOff;
    public static Action cardRayCastOn;
    public static GameObject unitUi;
    public static BattleNetwork battleNetwork;
    public static Animator animatorShakeStatic;
    public static GameObject[] turnEffect;
    public static Image circleProperties;
    public static SideUnitUi sideUnitUi;
    public static MultiplayerDraft multiplayerDraft;
    public static bool work = false;

    [SerializeField] private TextMeshProUGUI[] nick;
    [SerializeField] private Image[] portrait;
    public Animator animatorShake;

    [SerializeField] private GameObject[] turnEffectPrefub;
    [SerializeField] private GameObject lostConnectionPrefub;
    [SerializeField] private GameObject loadingScreenPrefub;
    [SerializeField] private Image loadingScreenImagePrefub;
    [SerializeField] private GameObject loadingPrefub;
    [SerializeField] private TextMeshProUGUI loadingTextPrefub;
    [SerializeField] private GameObject unitUIPrefub;
    [SerializeField] private Sprite[] accountPortarit;
    [SerializeField] private GameObject unitPropertiesPrefub;
    [SerializeField] private GameObject debuffsPrefub;
    [SerializeField] private Image[] playersBarsPrefub;
    [SerializeField] private Image circlePropertiesPrefub;
    [SerializeField] private SideUnitUi sideUnitUiPrefub;
    [SerializeField] private MultiplayerDraft multiplayerDraftPrefub;
    [SerializeField] private BattleVoice soundVoicePrefub;

    public float fillTime = 20f;
    public float fillSpeed;
    private bool stopCoroutine = false;
    [SerializeField] private GameObject timeOutObj;
    [SerializeField] private TextMeshProUGUI textWaitForPlayer;
    [SerializeField] private string[] textWait;


    private void Start()
    {
        fillSpeed = 1f / fillTime;
        DefenitionStart();
        if(BattleNetwork.doingQueue.Count > 0 || BattleNetwork.attackResultQueue.Count > 0)
        {
            BattleNetwork.doingQueue.Clear();
            BattleNetwork.attackResultQueue.Clear();
        }
        StartCoroutine(TimeOutTimer());
        StartCoroutine(GetComponent<BattleNetwork>().Game(0));

/*        Energy.mode = 1;
            StartCoroutine(GetComponent<BattleNetwork>().Game(1));
            LoadingManager.LoadingScreenAfter = -1;
            Start2();*/
    }
    private IEnumerator TimeOutTimer()
    {
        int count = 0;
        string text = textWait[PlayerData.language];
        string extraString;
        for (int i = 20; i > 0; i--)
        {
            if (stopCoroutine) yield break;
            count++;
            if (count == 0) extraString = "";
            else if (count == 1) extraString = ".";
            else if (count == 2) extraString = "..";
            else
            {
                extraString = "...";
                count = 0;
            }
            text += extraString;
            textWaitForPlayer.text = text;
            yield return new WaitForSeconds(1f);
        }
        print("я запустилс€!");
        textWaitForPlayer.gameObject.SetActive(false);
        timeOutObj.SetActive(true);
    }
    public void Start2()
    {
        stopCoroutine = true;
        DefinitionMode();
        StartUI();
        StartCoroutine(GetComponent<Turns>().Start2());
    }
    private void DefenitionStart()
    {
        soundVoice = soundVoicePrefub;
        multiplayerDraft = multiplayerDraftPrefub;
        playersBars = playersBarsPrefub;
        sideUnitUi = sideUnitUiPrefub;
        circleProperties = circlePropertiesPrefub;
        debuffs = debuffsPrefub;
        unitProperties = unitPropertiesPrefub;
        battleNetwork = GetComponent<BattleNetwork>();
        turnEffect = turnEffectPrefub;
        animatorShakeStatic = animatorShake;
        unitUi = unitUIPrefub;
        PlayerData.lostConnection = lostConnectionPrefub;
        LoadingManager.textLoadingScreenText = loadingTextPrefub;
        LoadingManager.LoadingScreenAfterImage = loadingScreenImagePrefub;
        LoadingManager.LoadingScreenAfterObj = loadingScreenPrefub;
        LoadingManager.LoadingIcon = loadingPrefub;
        buttonAI = buttonAIPrefub;
        PlayerData.accountPortraitIndex = accountPortarit;
        Sound.sound = GetComponent<AudioSource>();
        if (LoadingManager.LoadingScreenAfter != -1)
        {
            LoadingManager.LoadingScreenAfterObj.SetActive(true);
            if (PlayerData.language == 0) LoadingManager.textLoadingScreenText.text = LoadingManager.LoadingScreenAfterStringEng[LoadingManager.LoadingScreenText];
            else if (PlayerData.language == 1) LoadingManager.textLoadingScreenText.text = LoadingManager.LoadingScreenAfterStringRus[LoadingManager.LoadingScreenText];
            LoadingManager.LoadingScreenAfterImage.sprite = LoadingManager.LoadingScreenAfterSprite[LoadingManager.LoadingScreenAfter];
        }
    }
    private void StartUI()
    {
        //buttonAI.interactable = false;

        if (PlayerData.combatView == 1)
        {
            planks[0].SetActive(false);
            planks[1].SetActive(true);
            viewLine.sprite = viewSprite[0];
        }
        else if (PlayerData.combatView == 2)
        {
            planks[1].SetActive(false);
            planks[0].SetActive(true);
            viewLine.sprite = viewSprite[1];
        }
        else
        {
            PlayerData.combatView = 1;
            planks[0].SetActive(false);
            planks[1].SetActive(true);
            viewLine.sprite = viewSprite[0];
        }
        if (PlayerData.ai == 1)
        {
            aiImage.sprite = aiSprite[0];
        }
        else if (PlayerData.ai == 2)
        {
            aiImage.sprite = aiSprite[1];
        }
        else
        {
            aiImage.sprite = aiSprite[0];
            PlayerData.ai = 1;
        }
    }
    public void AiButton()
    {
        if (PlayerData.ai == 1)
        {
            PlayerPrefs.SetInt("ai", 2);
            PlayerData.ai = 2;
            if (Turns.turnUnit != null && Turns.turnUnit.sideOnMap == BattleNetwork.sideOnBattle && Turns.aiMay) GetComponent<BattleAI>().AI0();
            aiImage.sprite = aiSprite[1];
        }

        else if (PlayerData.ai == 2)
        {
            PlayerPrefs.SetInt("ai", 1);
            PlayerData.ai = 1;
            aiImage.sprite = aiSprite[0];
        }
        //buttonAI.interactable = false;
    }
    public void SetView()
    {
        if (PlayerData.combatView == 1)
        {
            PlayerPrefs.SetInt("CombatViewPrefs", 2);
            PlayerData.combatView = 2;
            viewLine.sprite = viewSprite[1];
            planks[1].SetActive(false);
            planks[0].SetActive(true);
        }

        else if (PlayerData.combatView == 2)
        {
            PlayerPrefs.SetInt("CombatViewPrefs", 1);
            PlayerData.combatView = 1;
            viewLine.sprite = viewSprite[0];
            planks[0].SetActive(false);
            planks[1].SetActive(true);
        }
        setViewTrops?.Invoke();
    }
    public void DefinitionMode()
    {
        ParticleSystem particle;
        ParticleSystem.MainModule mainModule;
        if (BattleNetwork.sideOnBattle == 0)
        {
            portrait[1].sprite = PlayerData.accountPortraitIndex[Campany.enemyPortraitInt];
            portrait[0].sprite = PlayerData.accountPortraitIndex[PlayerData.portrait];
            nick[1].text = Campany.enemyNick;
            nick[0].text = PlayerData.nick;
            playersFrame[0].color = new(0, 0.8f, 1f);
            playersFrame[1].color = new(1f, 0, 0);
            particle = turnEffect[1].GetComponent<ParticleSystem>();
            mainModule = particle.main;
            mainModule.startColor = new Color(1f, 0, 0);

            particle = turnEffect[0].GetComponent<ParticleSystem>();
            mainModule = particle.main;
            mainModule.startColor = new Color(0, 0.8f, 1f);
        }
        else
        {
            portrait[0].sprite = PlayerData.accountPortraitIndex[Campany.enemyPortraitInt];
            portrait[1].sprite = PlayerData.accountPortraitIndex[PlayerData.portrait];
            nick[0].text = Campany.enemyNick;
            nick[1].text = PlayerData.nick;
            playersFrame[1].color = new(0, 0.8f, 1f);
            playersFrame[0].color = new(1f, 0, 0);
            particle = turnEffect[0].GetComponent<ParticleSystem>();
            mainModule = particle.main;
            mainModule.startColor = new Color(1f, 0, 0);

            particle = turnEffect[1].GetComponent<ParticleSystem>();
            mainModule = particle.main;
            mainModule.startColor = new Color(0, 0.8f, 1f);
        }

    }
    public void FixedUpdate()
    {
        if (!work || Turns.turnUnit == null) return;
        // ”меньшаем заполнение с посто€нной скоростью
        float newFillAmount = playersBars[Turns.turnUnit.sideOnMap].fillAmount - (fillSpeed * Time.fixedDeltaTime);
        playersBars[Turns.turnUnit.sideOnMap].fillAmount = Mathf.Clamp01(newFillAmount);

        // »змен€ем цвет в соответствии с заполнением
        float fillPercentage = playersBars[Turns.turnUnit.sideOnMap].fillAmount;
        Color color;
        if (fillPercentage < 0.5f) color = Color.Lerp(Color.red, Color.yellow, fillPercentage * 2f);
        else color = Color.Lerp(Color.yellow, Color.green, (fillPercentage - 0.5f) * 2f);
        playersBars[Turns.turnUnit.sideOnMap].color = color;

        // ѕровер€ем, достигли ли нулевого заполнени€
        if (playersBars[Turns.turnUnit.sideOnMap].fillAmount == 0) work = false;
    }
}
