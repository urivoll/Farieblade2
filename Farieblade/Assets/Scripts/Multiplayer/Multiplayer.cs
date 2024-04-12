using System;
using System.Collections;
using TMPro;
using UnityEngine;
public class Multiplayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textPoints;
    [SerializeField] private TextMeshProUGUI textRank;
    [SerializeField] private TextMeshProUGUI textRecStr;
    [SerializeField] private GameObject imageRank;
    [SerializeField] private UnityEngine.UI.Image imageBar;
    [SerializeField] private Sprite[] rankSprite;
    public static int indexSprite;
    public static int tempIndexSprite;
    [SerializeField] private TextMeshProUGUI textTime;
    [SerializeField] private GameObject[] Stars;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioClip up;
    [SerializeField] private AudioClip workSound;
    public static bool win = false;
    private bool work = false;
    private float num;
    private float rankBefore;
    private float rankAfter;
    public bool after = false;
    void Start()
    {
        if (after == false)
        {
            textPoints.text = Convert.ToString(PlayerData.rank);
            FindLegaue(PlayerData.rank);
            imageBar.fillAmount = SetFill(PlayerData.rank);
            imageRank.GetComponent<SpriteRenderer>().sprite = rankSprite[indexSprite];
        }
        else
        {
            textPoints.text = Convert.ToString(PlayerData.rankBefore);
            FindLegaue(PlayerData.rankBefore, true);
            imageBar.fillAmount = SetFill(PlayerData.rankBefore);
            imageRank.GetComponent<SpriteRenderer>().sprite = rankSprite[tempIndexSprite];
        }
    }
    public void AfterFight() => StartCoroutine(AfterFightAsync());
    public IEnumerator AfterFightAsync()
    {
        rankBefore = Convert.ToSingle(PlayerData.rankBefore);
        rankAfter = Convert.ToSingle(PlayerData.rank);
        num = SetFill(PlayerData.rank);
        yield return new WaitForSeconds(1f);
        after = false;
        work = true;
        Sound.sound.PlayOneShot(workSound);
        yield return new WaitForSeconds(0.5f);
        FindLegaue(PlayerData.rank);
        if(win == true)
        {
            StartCoroutine(GetComponent<ChestTake>().SetChest());
            LoadingManager.LoadingIcon.SetActive(true);
            Coroutine cor;
            if (TaskManager.Daily[3] < 5)
            {
                TaskManager.Daily[3] += 1;
                PlayerData.floatNote.SetActive(true);
                PlayerData.floatNote.GetComponent<TaskFloatNote>().SetValues(3);
                cor = StartCoroutine(DataBase.UpdateData($"id3", "Tasks", TaskManager.Daily[3]));
                yield return cor;
                if (TaskManager.Daily[3] == 5)
                {
                    TaskManager.taskProgressDaily += 20;
                    cor = StartCoroutine(DataBase.UpdateData($"taskProgressDaily", "Tasks", TaskManager.taskProgressDaily));
                    yield return cor;

                    if (TaskManager.Weekly[1] < 500)
                    {
                        TaskManager.Weekly[1] += 20;
                        cor = StartCoroutine(DataBase.UpdateData($"idW1", "Tasks", TaskManager.Weekly[1]));
                        yield return cor;
                        if (TaskManager.Weekly[1] >= 500)
                        {
                            TaskManager.Weekly[1] = 500;
                            TaskManager.taskProgressWeekly += 10;
                            cor = StartCoroutine(DataBase.UpdateData($"taskProgressWeekly", "Tasks", TaskManager.taskProgressWeekly));
                            yield return cor;
                        }
                    }
                }
            }
            if (TaskManager.Weekly[3] < 20)
            {
                TaskManager.Weekly[3] += 1;
                cor = StartCoroutine(DataBase.UpdateData($"idW3", "Tasks", TaskManager.Weekly[3]));
                yield return cor;
                if (TaskManager.Weekly[3] == 20)
                {
                    TaskManager.taskProgressWeekly += 20;
                    cor = StartCoroutine(DataBase.UpdateData($"taskProgressWeekly", "Tasks", TaskManager.taskProgressWeekly));
                    yield return cor;
                }
            }
            LoadingManager.LoadingIcon.SetActive(false);
        }

        if (indexSprite != tempIndexSprite)
        {
            Sound.sound.PlayOneShot(up);
            animator.SetTrigger("up");
            imageRank.GetComponent<SpriteRenderer>().sprite = rankSprite[indexSprite];
            tempIndexSprite = indexSprite;
        }
        work = false;
    }

    private void FixedUpdate()
    {
        if (work)
        {
            imageBar.fillAmount = Mathf.Lerp(imageBar.fillAmount, num, 6f * Time.deltaTime);
            rankBefore = Mathf.Lerp(rankBefore, rankAfter, 6f * Time.deltaTime);
            textPoints.text = Convert.ToString(Convert.ToInt32(rankBefore));
        }
    }
    private float SetFill(int rank)
    {
        string num = Convert.ToString(rank);
        string porsent = "";
        if (rank >= 10) porsent += num[num.Length - 2];
        porsent += num[num.Length - 1];
        return Convert.ToSingle(int.Parse(porsent)) / 100;
    }
    private void FindLegaue(int rank, bool before = false)
    {
        //новый
        if (rank < 100)
        {
            //textRecStr.text = "df";
            indexSprite = 0;
            PlayerData.league = 30;
            Stars[0].SetActive(false);
            Stars[1].SetActive(false);
            Stars[2].SetActive(false);
            GetComponent<ChestTake>().bronzeChest = 100;
        }
        else if (rank >= 100 && rank < 200)
        {
            indexSprite = 0;
            PlayerData.league = 29;
            Stars[0].SetActive(true);
            Stars[1].SetActive(false);
            Stars[2].SetActive(false);
            GetComponent<ChestTake>().bronzeChest = 100;
        }
        else if (rank >= 200 && rank < 300)
        {
            indexSprite = 0;
            PlayerData.league = 28;
            Stars[0].SetActive(true);
            Stars[1].SetActive(true);
            Stars[2].SetActive(false);
            GetComponent<ChestTake>().bronzeChest = 90;
            GetComponent<ChestTake>().silverChest = 10;
        }
        else if (rank >= 300 && rank < 400)
        {
            indexSprite = 0;
            PlayerData.league = 27;
            Stars[0].SetActive(true);
            Stars[1].SetActive(true);
            Stars[2].SetActive(true);
            GetComponent<ChestTake>().bronzeChest = 90;
            GetComponent<ChestTake>().silverChest = 10;
        }
        //новый
        else if (rank >= 400 && rank < 500)
        {
            indexSprite = 1;
            PlayerData.league = 26;
            Stars[0].SetActive(false);
            Stars[1].SetActive(false);
            Stars[2].SetActive(false);
            GetComponent<ChestTake>().bronzeChest = 80;
            GetComponent<ChestTake>().silverChest = 15;
            GetComponent<ChestTake>().goldChest = 5;
        }
        else if (rank >= 500 && rank < 600)
        {
            indexSprite = 1;
            PlayerData.league = 25;
            Stars[0].SetActive(true);
            Stars[1].SetActive(false);
            Stars[2].SetActive(false);
            GetComponent<ChestTake>().bronzeChest = 80;
            GetComponent<ChestTake>().silverChest = 15;
            GetComponent<ChestTake>().goldChest = 5;
        }
        else if (rank >= 600 && rank < 700)
        {
            indexSprite = 1;
            PlayerData.league = 24;
            Stars[0].SetActive(true);
            Stars[1].SetActive(true);
            Stars[2].SetActive(false);
            GetComponent<ChestTake>().bronzeChest = 80;
            GetComponent<ChestTake>().silverChest = 15;
            GetComponent<ChestTake>().goldChest = 5;
        }
        else if (rank >= 700 && rank < 800)
        {
            indexSprite = 1;
            PlayerData.league = 23;
            Stars[0].SetActive(true);
            Stars[1].SetActive(true);
            Stars[2].SetActive(true);
            GetComponent<ChestTake>().bronzeChest = 80;
            GetComponent<ChestTake>().silverChest = 15;
            GetComponent<ChestTake>().goldChest = 5;
        }
        //новый
        else if (rank >= 800 && rank < 900)
        {
            indexSprite = 2;
            PlayerData.league = 22;
            Stars[0].SetActive(false);
            Stars[1].SetActive(false);
            Stars[2].SetActive(false);
            GetComponent<ChestTake>().bronzeChest = 60;
            GetComponent<ChestTake>().silverChest = 25;
            GetComponent<ChestTake>().goldChest = 10;
            GetComponent<ChestTake>().dimondChest = 5;
        }
        else if (rank >= 900 && rank < 1000)
        {
            indexSprite = 2;
            PlayerData.league = 21;
            Stars[0].SetActive(true);
            Stars[1].SetActive(false);
            Stars[2].SetActive(false);
            GetComponent<ChestTake>().bronzeChest = 60;
            GetComponent<ChestTake>().silverChest = 25;
            GetComponent<ChestTake>().goldChest = 10;
            GetComponent<ChestTake>().dimondChest = 5;
        }
        else if (rank >= 1000 && rank < 1100)
        {
            indexSprite = 2;
            PlayerData.league = 20;
            Stars[0].SetActive(true);
            Stars[1].SetActive(true);
            Stars[2].SetActive(false);
            GetComponent<ChestTake>().bronzeChest = 60;
            GetComponent<ChestTake>().silverChest = 25;
            GetComponent<ChestTake>().goldChest = 10;
            GetComponent<ChestTake>().dimondChest = 5;
        }
        else if (rank >= 1100 && rank < 1200)
        {
            indexSprite = 2;
            PlayerData.league = 19;
            Stars[0].SetActive(true);
            Stars[1].SetActive(true);
            Stars[2].SetActive(true);
            GetComponent<ChestTake>().bronzeChest = 60;
            GetComponent<ChestTake>().silverChest = 25;
            GetComponent<ChestTake>().goldChest = 10;
            GetComponent<ChestTake>().dimondChest = 5;
        }
        //18
        else if (rank >= 1200 && rank < 1300)
        {
            indexSprite = 3;
            PlayerData.league = 18;
            Stars[0].SetActive(false);
            Stars[1].SetActive(false);
            Stars[2].SetActive(false);
            GetComponent<ChestTake>().bronzeChest = 40;
            GetComponent<ChestTake>().silverChest = 35;
            GetComponent<ChestTake>().goldChest = 15;
            GetComponent<ChestTake>().dimondChest = 10;
        }
        else if (rank >= 1300 && rank < 1400)
        {
            indexSprite = 3;
            PlayerData.league = 17;
            Stars[0].SetActive(true);
            Stars[1].SetActive(false);
            Stars[2].SetActive(false);
            GetComponent<ChestTake>().bronzeChest = 40;
            GetComponent<ChestTake>().silverChest = 35;
            GetComponent<ChestTake>().goldChest = 15;
            GetComponent<ChestTake>().dimondChest = 10;
        }
        else if (rank >= 1400 && rank < 1500)
        {
            indexSprite = 3;
            PlayerData.league = 16;
            Stars[0].SetActive(true);
            Stars[1].SetActive(true);
            Stars[2].SetActive(false);
            GetComponent<ChestTake>().bronzeChest = 40;
            GetComponent<ChestTake>().silverChest = 35;
            GetComponent<ChestTake>().goldChest = 15;
            GetComponent<ChestTake>().dimondChest = 10;
        }
        else if (rank >= 1500 && rank < 1600)
        {
            indexSprite = 3;
            PlayerData.league = 15;
            Stars[0].SetActive(true);
            Stars[1].SetActive(true);
            Stars[2].SetActive(true);
            GetComponent<ChestTake>().bronzeChest = 40;
            GetComponent<ChestTake>().silverChest = 35;
            GetComponent<ChestTake>().goldChest = 15;
            GetComponent<ChestTake>().dimondChest = 10;
        }
        //14
        else if (rank >= 1600 && rank < 1700)
        {
            indexSprite = 4;
            PlayerData.league = 14;
            Stars[0].SetActive(false);
            Stars[1].SetActive(false);
            Stars[2].SetActive(false);
            GetComponent<ChestTake>().bronzeChest = 20;
            GetComponent<ChestTake>().silverChest = 40;
            GetComponent<ChestTake>().goldChest = 30;
            GetComponent<ChestTake>().dimondChest = 10;
        }
        else if (rank >= 1700 && rank < 1800)
        {
            indexSprite = 4;
            PlayerData.league = 13;
            Stars[0].SetActive(true);
            Stars[1].SetActive(false);
            Stars[2].SetActive(false);
            GetComponent<ChestTake>().bronzeChest = 20;
            GetComponent<ChestTake>().silverChest = 40;
            GetComponent<ChestTake>().goldChest = 30;
            GetComponent<ChestTake>().dimondChest = 10;
        }
        else if (rank >= 1800 && rank < 1900)
        {
            indexSprite = 4;
            PlayerData.league = 12;
            Stars[0].SetActive(true);
            Stars[1].SetActive(true);
            Stars[2].SetActive(false);
            GetComponent<ChestTake>().bronzeChest = 20;
            GetComponent<ChestTake>().silverChest = 40;
            GetComponent<ChestTake>().goldChest = 30;
            GetComponent<ChestTake>().dimondChest = 10;
        }
        else if (rank >= 1900 && rank < 2000)
        {
            indexSprite = 4;
            PlayerData.league = 11;
            Stars[0].SetActive(true);
            Stars[1].SetActive(true);
            Stars[2].SetActive(true);
            GetComponent<ChestTake>().bronzeChest = 20;
            GetComponent<ChestTake>().silverChest = 40;
            GetComponent<ChestTake>().goldChest = 30;
            GetComponent<ChestTake>().dimondChest = 10;
        }
        //10
        else if (rank >= 2000 && rank < 2100)
        {
            indexSprite = 5;
            PlayerData.league = 10;
            Stars[0].SetActive(false);
            Stars[1].SetActive(false);
            Stars[2].SetActive(false);
            GetComponent<ChestTake>().bronzeChest = 10;
            GetComponent<ChestTake>().silverChest = 50;
            GetComponent<ChestTake>().goldChest = 30;
            GetComponent<ChestTake>().dimondChest = 10;
        }
        else if (rank >= 2100 && rank < 2200)
        {
            indexSprite = 5;
            PlayerData.league = 9;
            Stars[0].SetActive(true);
            Stars[1].SetActive(false);
            Stars[2].SetActive(false);
            GetComponent<ChestTake>().bronzeChest = 10;
            GetComponent<ChestTake>().silverChest = 50;
            GetComponent<ChestTake>().goldChest = 30;
            GetComponent<ChestTake>().dimondChest = 10;
        }
        else if (rank >= 2200 && rank < 2300)
        {
            indexSprite = 5;
            PlayerData.league = 8;
            Stars[0].SetActive(true);
            Stars[1].SetActive(true);
            Stars[2].SetActive(false);
            GetComponent<ChestTake>().bronzeChest = 10;
            GetComponent<ChestTake>().silverChest = 50;
            GetComponent<ChestTake>().goldChest = 30;
            GetComponent<ChestTake>().dimondChest = 10;
        }
        else if (rank >= 2300 && rank < 2400)
        {
            indexSprite = 5;
            PlayerData.league = 7;
            Stars[0].SetActive(true);
            Stars[1].SetActive(true);
            Stars[2].SetActive(true);
            GetComponent<ChestTake>().bronzeChest = 10;
            GetComponent<ChestTake>().silverChest = 50;
            GetComponent<ChestTake>().goldChest = 30;
            GetComponent<ChestTake>().dimondChest = 10;
        }
        //6
        else if (rank >= 2400 && rank < 2500)
        {
            indexSprite = 6;
            PlayerData.league = 6;
            Stars[0].SetActive(false);
            Stars[1].SetActive(false);
            Stars[2].SetActive(false);
            GetComponent<ChestTake>().silverChest = 60;
            GetComponent<ChestTake>().goldChest = 30;
            GetComponent<ChestTake>().dimondChest = 10;
        }
        else if (rank >= 2500 && rank < 2600)
        {
            indexSprite = 6;
            PlayerData.league = 5;
            Stars[0].SetActive(true);
            Stars[1].SetActive(false);
            Stars[2].SetActive(false);
            GetComponent<ChestTake>().silverChest = 60;
            GetComponent<ChestTake>().goldChest = 30;
            GetComponent<ChestTake>().dimondChest = 10;
        }
        else if (rank >= 2600 && rank < 2700)
        {
            indexSprite = 6;
            PlayerData.league = 4;
            Stars[0].SetActive(true);
            Stars[1].SetActive(true);
            Stars[2].SetActive(false);
            GetComponent<ChestTake>().silverChest = 60;
            GetComponent<ChestTake>().goldChest = 30;
            GetComponent<ChestTake>().dimondChest = 10;
        }
        else if (rank >= 2700 && rank < 2800)
        {
            indexSprite = 6;
            PlayerData.league = 3;
            Stars[0].SetActive(true);
            Stars[1].SetActive(true);
            Stars[2].SetActive(true);
            GetComponent<ChestTake>().silverChest = 60;
            GetComponent<ChestTake>().goldChest = 30;
            GetComponent<ChestTake>().dimondChest = 10;
        }
        //3
        else if (rank >= 2800 && rank < 2900)
        {
            indexSprite = 7;
            PlayerData.league = 3;
            Stars[0].SetActive(false);
            Stars[1].SetActive(false);
            Stars[2].SetActive(false);
            GetComponent<ChestTake>().silverChest = 50;
            GetComponent<ChestTake>().goldChest = 35;
            GetComponent<ChestTake>().dimondChest = 15;
        }
        else if (rank >= 2900 && rank < 3000)
        {
            indexSprite = 7;
            PlayerData.league = 2;
            Stars[0].SetActive(true);
            Stars[1].SetActive(false);
            Stars[2].SetActive(false);
            GetComponent<ChestTake>().silverChest = 50;
            GetComponent<ChestTake>().goldChest = 35;
            GetComponent<ChestTake>().dimondChest = 15;
        }
        else if (rank >= 3000 && rank < 3100)
        {
            indexSprite = 7;
            PlayerData.league = 1;
            Stars[0].SetActive(true);
            Stars[1].SetActive(true);
            Stars[2].SetActive(false);
            GetComponent<ChestTake>().silverChest = 50;
            GetComponent<ChestTake>().goldChest = 35;
            GetComponent<ChestTake>().dimondChest = 15;
        }
        else if (rank >= 3100 && rank < 3200)
        {
            indexSprite = 7;
            PlayerData.league = 0;
            Stars[0].SetActive(true);
            Stars[1].SetActive(true);
            Stars[2].SetActive(true);
            GetComponent<ChestTake>().silverChest = 50;
            GetComponent<ChestTake>().goldChest = 35;
            GetComponent<ChestTake>().dimondChest = 15;
        }
        if (before == true) tempIndexSprite = indexSprite;
        textRank.text = Convert.ToString(PlayerData.league);
    }
}