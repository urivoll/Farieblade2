using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnIs : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Name;
    [SerializeField] private TextMeshProUGUI Descripton;
    [SerializeField] private TextMeshProUGUI textPower;
    [SerializeField] private TextMeshProUGUI textDuration;
    [SerializeField] private StringArray[] warning;
    [SerializeField] private GameObject myCollection;
    [SerializeField] private UIDropHandlerAnIs[] dropHandler;
    [SerializeField] private AnIsMap anIsMap;
    [SerializeField] private GameObject content;
    [SerializeField] private AudioClip clip;
    [SerializeField] private GameObject[] Items;
    [SerializeField] private Sprite[] ImgLocation;
    [SerializeField] private Image Img;
    public static int CurrentLoc;
    public void Location(int location)
    {
        CurrentLoc = location;
        AnIsLoc anisloc = anIsMap.Locations[location].GetComponent<AnIsLoc>();
        Img.sprite = ImgLocation[location];
        textPower.text = anisloc.powerNeed.ToString();
        AnIsCollection.PowerNeedAnIs = anisloc.powerNeed;
        Campany.anIsCurrentPlace = anisloc.idPlace;
        textDuration.text = Convert.ToString(anisloc.timeNeed);
        Name.text = anisloc.Name[PlayerData.language];
        Descripton.text = anisloc.Description[PlayerData.language];
        for (int i = 0; i < anisloc.itemReward.Length; i++)
        {
            Items[anisloc.itemReward[i]].SetActive(true);
            Items[anisloc.itemReward[i]].transform.Find("Amount").GetComponent<TextMeshProUGUI>().text = Convert.ToString(anisloc.itemAmount[i]) + " - " + Convert.ToString(anisloc.itemAmount[i] * 2);
        }
    }
    private void OnDisable()
    {
        for (int i = 0; i < Items.Length; i++)
        {
            Items[i].SetActive(false);
        }
    }
    public void CheckSquad()
    {
        if (CheckFreeSlotAnIs() == -1)
        {
            PlayerData.warning.SetActive(true);
            PlayerData.textWarning.text = warning[0].intArray[PlayerData.language];
        }
        else
        {
            Sound.sound.PlayOneShot(clip);
            myCollection.SetActive(true);
        }
    }
    public void SetSquad()
    {
        StartCoroutine(SetSquad2());
    }
    public IEnumerator SetSquad2()
    {
        int[] units = new int[3];
        LoadingManager.LoadingIcon.SetActive(true);
        for (int i = 0; i < dropHandler.Length; i++)
        {
            if (dropHandler[i].newObject != null)
            {
                units[i] = dropHandler[i].newObject.GetComponent<Unit>().ID;
            }
            else units[i] = -666;
        }
        string json = "";
        Dictionary<string, string> form = new Dictionary<string, string>
            {
                { "id0", $"{units[0]}" },
                { "id1", $"{units[1]}" },
                { "id2", $"{units[2]}" },
                { "loc", $"{Campany.anIsCurrentPlace}" }
            };
        var cor = StartCoroutine(Http.HttpQurey(answer => json = answer, "anIs/sendSquad", form));
        yield return cor;
        print(json);
        if (json == "-1")
        {
            PlayerData.warning.SetActive(true);
            PlayerData.textWarning.text = warning[0].intArray[PlayerData.language];
        }
        else if (json == "-2")
        {
            PlayerData.warning.SetActive(true);
            PlayerData.textWarning.text = warning[2].intArray[PlayerData.language];
        }
        else if (json == "-3")
        {
            PlayerData.warning.SetActive(true);
            PlayerData.textWarning.text = warning[1].intArray[PlayerData.language];
        }
        else
        {
            AnIsSend obj = JsonConvert.DeserializeObject<AnIsSend>(json);
            Campany.anIsTimeNeed[obj.index] = obj.timeNeed;
            Campany.anIsLocation[obj.index] = Campany.anIsCurrentPlace;
            for (int i = 0; i < dropHandler.Length; i++)
            {
                if (dropHandler[i].newObject != null)
                {
                    PlayerData.myCollection[dropHandler[i].newObject.GetComponent<Unit>().ID].GetComponent<Unit>().onAnIs = Campany.anIsCurrentPlace;
                    PlayerData.myCollection[dropHandler[i].newObject.GetComponent<Unit>().ID].transform.Find("Card").gameObject.GetComponent<CardVeiw>().SetCardValues();
                }
            }

            if (obj.TaskIdD2 != -666)
            {
                TaskManager.Daily[2] = obj.TaskIdD2;
                PlayerData.floatNote.SetActive(true);
                PlayerData.floatNote.GetComponent<TaskFloatNote>().SetValues(2);
            }
            if (obj.taskProgressDaily != -666)
            {
                TaskManager.taskProgressDaily = obj.taskProgressDaily;
            }
            if(obj.taskProgressWeekly != -666)
            {
                TaskManager.taskProgressWeekly = obj.taskProgressWeekly;
            }
            if (obj.TaskIdW1 != -666)
            {
                TaskManager.Weekly[1] = obj.TaskIdW1;
            }
            if (obj.TaskIdW2 != -666)
            {
                TaskManager.Weekly[2] = obj.TaskIdW2;
            }
        }
        for (int i = 0; i < dropHandler.Length; i++)
        {
            if (dropHandler[i].newObject != null)
            {
                dropHandler[i].newObject.transform.SetParent(content.transform);
                dropHandler[i].newObject.transform.Find("Card").gameObject.GetComponent<UIDragHandlerAnIs>().parentCircle = null;
                dropHandler[i].newObject = null;
            }
        }
        AnIsCollection.PowerNeedAnIs = 0;
        AnIsCollection.PowerAnIs = 0;
        AnIsCollection.ChanceAnIs = 0;
        myCollection.GetComponent<AnIsCollection>().SetPower2();
        anIsMap.SetCirclesOnMap();
        myCollection.SetActive(false);
        gameObject.SetActive(false);
        LoadingManager.LoadingIcon.SetActive(false);
    }
    public int CheckFreeSlotAnIs()
    {
        for (int i = 0; i < Campany.anIsTimeNeed.Length; i++)
        {
            if (Campany.anIsTimeNeed[i] == 0)
                return i;
        }
        return -1;
    }
}
public class AnIsSend
{
    public int index;
    public int timeStart;
    public int timeNeed;

    public int TaskIdD2;
    public int TaskIdW1;
    public int TaskIdW2;
    public int taskProgressWeekly;
    public int taskProgressDaily;
}
