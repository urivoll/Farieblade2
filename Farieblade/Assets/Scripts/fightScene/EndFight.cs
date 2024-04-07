using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
public class EndFight : MonoBehaviour
{
    [SerializeField] private GameObject[] exp;
    [SerializeField] private TextMeshProUGUI[] textPlus;
    [SerializeField] private TextMeshProUGUI[] textExpNeed;
    [SerializeField] private GameObject[] expLvlup;
    [SerializeField] private Image[] expBar;
    [SerializeField] private TextMeshProUGUI textGoldReward;
    [SerializeField] private TextMeshProUGUI textAFReward;
    //[SerializeField] private TextMeshProUGUI _textEnd;
    [SerializeField] private GameObject AccLvlup;
    [SerializeField] private GameObject rewardAF;
    [SerializeField] private GameObject _endGameContent;
    [SerializeField] private GameObject _wonTable;
    [SerializeField] private GameObject _lostTable;
    [SerializeField] private GameObject[] lost;
    [SerializeField] private AudioClip _clipWin;
    [SerializeField] private AudioClip _clipLose;
    [SerializeField] private AudioClip swish;
    [SerializeField] private AudioClip hit;
    [SerializeField] private AudioClip zoom;
    public bool unitPanel = false;
    void Start()
    {
        MusicFight.StartMusic.Stop();
    }
    public IEnumerator EndScene(bool win)
    {
        BattleNetwork.end = true;
        LoadingManager.LoadingIcon.SetActive(true);
        if (unitPanel == true)
        {
            unitPanel = false;
            GameObject.FindGameObjectWithTag("UnitPropertiesPanel").SetActive(false);
        }
        yield return new WaitForSeconds(0.01f);

        if (win == true)
        {
            _wonTable.SetActive(true);
            BattleSound.sound.PlayOneShot(_clipWin);
            for (int i = 0; i < BattleNetwork.winnersCount; i++)
            {
                exp[i].SetActive(true);
                CreateCard(i);
                float barValue = Convert.ToSingle(BattleNetwork.winners[i, 2]) * 100 / Convert.ToSingle(UnitReward.unitReward[BattleNetwork.winners[i, 1], 0]);
                if (BattleNetwork.winners[i, 5] != 0) expLvlup[i].SetActive(true);
                if (BattleNetwork.winners[i, 4] != 0)
                {
                    textPlus[i].text = "+" + Convert.ToString(BattleNetwork.winners[i, 4]);
                    textExpNeed[i].text = Convert.ToString(BattleNetwork.winners[i, 2]) + " / " + Convert.ToString(UnitReward.unitReward[BattleNetwork.winners[i, 1], 0]);
                    expBar[i].fillAmount = barValue / 100;
                }
                else
                {
                    textExpNeed[i].text = "Max level";
                    expBar[i].fillAmount = 1;
                }
            }
        }
        //Поражение!
        else
        {
            _lostTable.SetActive(true);
            BattleSound.sound.PlayOneShot(_clipLose);
        }
        Turns.numberTurn = 1;
        Turns.listUnitAll.Clear();
        //Turns.eventEndCard.Clear();
        Turns.listUnitLeft.Clear();
        Turns.listUnitRight.Clear();
        Turns.listUnitEnemy.Clear();
        Turns.listUnitOur.Clear();
        Turns.listFinishRight.Clear();
        Turns.finishEndEvent = false;
        if(LoadingManager.LoadingScreenAfterSprite != null)
        {
            LoadingManager.LoadingScreenAfter = Random.Range(0, LoadingManager.LoadingScreenAfterSprite.Length);
            LoadingManager.LoadingScreenAfterImage.sprite = LoadingManager.LoadingScreenAfterSprite[LoadingManager.LoadingScreenAfter];

            LoadingManager.LoadingScreenText = Random.Range(0, LoadingManager.LoadingScreenAfterStringRus.Length);
            if (PlayerData.language == 0) LoadingManager.textLoadingScreenText.text = LoadingManager.LoadingScreenAfterStringEng[LoadingManager.LoadingScreenText];
            else if (PlayerData.language == 1) LoadingManager.textLoadingScreenText.text = LoadingManager.LoadingScreenAfterStringRus[LoadingManager.LoadingScreenText];

        }
        LoadingManager.LoadingIcon.SetActive(false);
    }
    private void CreateCard(int i)
    {
        GameObject newObject = Instantiate(PlayerData.defaultCards[BattleNetwork.winners[i, 0]], _endGameContent.transform);
        newObject.GetComponent<Animator>().SetTrigger("end");
        newObject.GetComponent<Unit>().card.SetActive(true);
        newObject.transform.Find("Fight").gameObject.SetActive(false);
        Destroy(newObject.transform.Find("Card").gameObject.GetComponent<UIDragHandler>());
        newObject.GetComponent<Unit>().level = BattleNetwork.winners[i, 1];
        newObject.GetComponent<Unit>().grade = BattleNetwork.winners[i, 3];
        newObject.GetComponent<Unit>().SetValues();
        newObject.transform.Find("Card").gameObject.GetComponent<CardVeiw>().SetCardValues();
    }
/*    private IEnumerator LevelUpEffect(GameObject prefub, Unit i, int count)
    {
        yield return new WaitForSeconds(0.2f);
        Turns.fightSound.PlayOneShot(hit);
        prefub.GetComponent<Unit>().level = i.level;
        prefub.GetComponent<Unit>().SetValues();
        prefub.transform.Find("Card").gameObject.GetComponent<CardVeiw>().LvlupEffect.SetActive(true);
        prefub.transform.Find("Card").gameObject.GetComponent<CardVeiw>().SetCardValues();
        expLvlup[count].SetActive(true);
    }*/
}