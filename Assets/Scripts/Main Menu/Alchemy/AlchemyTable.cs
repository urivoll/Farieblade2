using JetBrains.Annotations;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlchemyTable : MonoBehaviour
{
    [SerializeField] private GameObject[] ButtonClosed;
    [SerializeField] private Image[] ButtonImg;
    [HideInInspector] public int[] ItemsCreate;
    [HideInInspector] public GameObject[] CurrentItem1;
    [HideInInspector] public GameObject[] CurrentItem2;
    [HideInInspector] public GameObject[] CurrentItem3;
    [HideInInspector] public GameObject[] CurrentItem4;
    [HideInInspector] public int[] CurrentItemNeed1;
    [HideInInspector] public int[] CurrentItemNeed2;
    [HideInInspector] public int[] CurrentItemNeed3;
    [HideInInspector] public int[] CurrentItemNeed4;
    [HideInInspector] public int type;
    [HideInInspector] public int[] tempInventory = new int[Inventory.InventoryPlayer.Length];
    [HideInInspector] public int[] tempListIndex = new int[4];
    private GameObject[] CurrentItems;
    private int[] CurrentItemNeed;
    [HideInInspector] public int Count;

    public void SetSprites(Sprite[] sprites)
    {
        for (int i = 0; i < ButtonImg.Length; i++)
        {
            ButtonImg[i].sprite = sprites[i];
        }
    }
    public void SetCardsBefore()
    {
        SetCards(CurrentItem1, CurrentItemNeed1, 0);
        SetCards(CurrentItem2, CurrentItemNeed2, 1);
        SetCards(CurrentItem3, CurrentItemNeed3, 2);
        SetCards(CurrentItem4, CurrentItemNeed4, 3);
    }
    public void SetCards(GameObject[] Cards, int[] Need, int index)
    {
        int count = 0;
        for (int i = 0; i < Cards.Length; i++)
        {
            Cards[i].transform.Find("Amount").GetComponent<TextMeshProUGUI>().text = tempInventory[Cards[i].GetComponent<InvenoryShowItem>().id].ToString();
            Cards[i].transform.Find("Text").GetComponent<TextMeshProUGUI>().text = Need[i].ToString();
            if (Need[i] <= tempInventory[Cards[i].GetComponent<InvenoryShowItem>().id])
            {
                Cards[i].GetComponent<Image>().color = new(255, 255, 255);
                count++;
            }
            else
            {
                Cards[i].GetComponent<Image>().color = new(100, 0, 0);
            }
        }
        if (count == Cards.Length) ButtonClosed[index].SetActive(false);
        else ButtonClosed[index].SetActive(true);
    }
    public void CreateItem(int index) 
    {
        Count++;
        tempListIndex[index]++;
        SetArray(index);
        for (int i = 0; i < CurrentItems.Length; i++)
        {
            tempInventory[CurrentItems[i].GetComponent<InvenoryShowItem>().id] -= CurrentItemNeed[i];
        }
        tempInventory[ItemsCreate[index]] += 1;
        SetCardsBefore();
    }
    private void SetArray(int index)
    {
        if(index == 0)
        {
            CurrentItems = CurrentItem1;
            CurrentItemNeed = CurrentItemNeed1;
        }
        else if (index == 1)
        {
            CurrentItems = CurrentItem2;
            CurrentItemNeed = CurrentItemNeed2;
        }
        else if (index == 2)
        {
            CurrentItems = CurrentItem3;
            CurrentItemNeed = CurrentItemNeed3;
        }
        else
        {
            CurrentItems = CurrentItem4;
            CurrentItemNeed = CurrentItemNeed4;
        }
    }
    public void Craft()
    {
        StartCoroutine(CraftAsync());
    }
    private IEnumerator CraftAsync()
    {
        string json = "";
        Dictionary<string, string> form = new Dictionary<string, string> { 
            { "type", $"{type}" },
            { "id0", $"{tempListIndex[0]}" },
            { "id1", $"{tempListIndex[1]}" },
            { "id2", $"{tempListIndex[2]}" },
            { "id3", $"{tempListIndex[3]}" },
            { "count", $"{Count}" }
        };
        var cor = Http.HttpQurey(answer => json = answer, "alchemy", form);
        yield return cor;
        AlchemyJson obj = JsonConvert.DeserializeObject<AlchemyJson>(json);

        if (obj.TaskIdD0 != -666)
        {
            TaskManager.Daily[0] = obj.TaskIdD0;
            PlayerData.floatNote.SetActive(true);
            PlayerData.floatNote.GetComponent<TaskFloatNote>().SetValues(0);
        }
        if (obj.taskProgressDaily != -666)
        {
            TaskManager.taskProgressDaily = obj.taskProgressDaily;
        }
        if (obj.taskProgressWeekly != -666)
        {
            TaskManager.taskProgressWeekly = obj.taskProgressWeekly;
        }
        if (obj.TaskIdW1 != -666)
        {
            TaskManager.Weekly[1] = obj.TaskIdW1;
        }
        if (obj.TaskIdW0 != -666)
        {
            TaskManager.Weekly[0] = obj.TaskIdW0;
        }
        Array.Copy(tempInventory, Inventory.InventoryPlayer, Inventory.InventoryPlayer.Length);
        Count = 0;
        for (int i = 0; i < 4; i++)
        {
            tempListIndex[i] = 0;
        }
    }
}
public class AlchemyJson
{
    public int TaskIdD0;
    public int TaskIdW0;
    public int TaskIdW1;
    public int taskProgressDaily;
    public int taskProgressWeekly;
}