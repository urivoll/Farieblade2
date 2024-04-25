using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChestTake : MonoBehaviour
{
    private int Af;
    [SerializeField] private TextMeshProUGUI[] textAF;
    [SerializeField] private GameObject[] AfObj;
    [SerializeField] private GameObject chestReward;
    public static GameObject[] chestsSlots;
    [SerializeField] private GameObject[] chestsSlotsPrefub;
    public static int[] slotTime = new int[5];
    public static int[] slotRar = new int[5];
    [HideInInspector] public int bronzeChest = 0;
    [HideInInspector] public int silverChest = 0;
    [HideInInspector] public int goldChest = 0;
    [HideInInspector] public int dimondChest = 0;
    private void Awake()
    {
        chestsSlots = chestsSlotsPrefub;
    }
    public IEnumerator SetChest()
    {
        for (int i = 0; i < slotTime.Length; i++)
        {
            if (slotTime[i] == 0)
            {
                LoadingManager.LoadingIcon.SetActive(true);
                int bronze2 = bronzeChest;
                int silver2 = bronzeChest + silverChest;
                int gold2 = bronzeChest + silverChest + goldChest;
                int rar;

                int rand = Random.Range(0, 101);
                if (rand < bronze2)
                    rar = 0;
                else if (rand >= bronze2 && rand < silver2)
                    rar = 1;
                else if (rand >= silver2 && rand < gold2)
                    rar = 2;
                else
                    rar = 3;
                slotTime[i] = DateTimeServer.serverTime;
                slotRar[i] = rar;
                var cor = StartCoroutine(DataBase.UpdateData($"slot{i}TimeNeed", "Chests", DateTimeServer.serverTime));
                yield return cor;
                cor = StartCoroutine(DataBase.UpdateData($"slot{i}Rar", "Chests", rar));
                yield return cor;
                LoadingManager.LoadingIcon.SetActive(false);
                break;
            }
        }
    }
    private void OnEnable()
    {
        SetTimeChest();
        InvokeRepeating("SetTimeChest", 1f, 1f);
    }
    public void SetTimeChest()
    {
        for (int i = 0; i < slotTime.Length; i++)
        {
            if (slotTime[i] != 0 && slotTime[i] != -1)
            {
                chestsSlots[i].GetComponent<ChestSlota>().Slots[slotRar[i]].SetActive(true);
                if (DateTimeServer.serverTime >= slotTime[i])
                {
                    slotTime[i] = -1;
                    AfObj[i].SetActive(false);
                    chestsSlots[i].GetComponent<ChestSlota>().Time.SetActive(false);
                    StickerManager.ChangeStick(i + 4, 1);
                }
                else
                {
                    int TimeDifference = slotTime[i] - DateTimeServer.serverTime;
                    chestsSlots[i].GetComponent<ChestSlota>().Time.SetActive(true);
                    if (TimeDifference < 3600 && TimeDifference > 60)
                        chestsSlots[i].GetComponent<ChestSlota>().textTime.text = Convert.ToString(TimeDifference / 60 + "m");
                    else if (TimeDifference <= 60)
                        chestsSlots[i].GetComponent<ChestSlota>().textTime.text = Convert.ToString(TimeDifference + "s");
                    else
                        chestsSlots[i].GetComponent<ChestSlota>().textTime.text = Convert.ToString(TimeDifference / 60 / 60 + "h");
                }
            }
        }
    }
    private int SetTimeRar(int i)
    {
        if (i == 0) return 15;
        else if (i == 1) return 60;
        else if (i == 2) return 240;
        else return 720;
    }
    public void SetAFPrice(int slot)
    {
        int timeNow = DateTimeServer.serverTime;
        int timeStart = slotTime[slot];
        int timeNeed = SetTimeRar(slotRar[slot]);
        int time = timeNeed - (timeNow - timeStart);
        if      (time > 39600) Af = 60;
        else if (time <= 39600 && time > 36000) Af = 55;
        else if (time <= 36000 && time > 32400) Af = 50;
        else if (time <= 32400 && time > 28800) Af = 45;
        else if (time <= 28800 && time > 25200) Af = 40;
        else if (time <= 25200 && time > 21600) Af = 35;
        else if (time <= 21600 && time > 18000) Af = 30;
        else if (time <= 18000 && time > 14400) Af = 25;
        else if (time <= 14400 && time > 10800) Af = 20;
        else if (time <= 10800 && time > 7200)  Af = 15;
        else if (time <= 7200  && time > 3600)  Af = 10;
        else Af = 5;
        textAF[slot].text = Convert.ToString(Af);
    }

    private void OnDisable()
    {
        for(int i = 0; i < AfObj.Length; i++)
        {
            AfObj[i].SetActive(false);
        }
    }
    public void BuyChest(int slot)
    {
        if(Af <= Inventory.InventoryPlayer[24])
        {
            StickerManager.ChangeStick(slot + 4, 1);
            slotTime[slot] -= 43200;
            StartCoroutine(DataBase.UpdateData($"slot{slot}Time", "Chests", slotTime[slot]));
            AfObj[slot].SetActive(false);
            Inventory.InventoryPlayer[24] -= Af;
            PlayerData.ChangeGoldAF();
            StartCoroutine(DataBase.UpdateData("AF", "Gamers", Inventory.InventoryPlayer[24]));
        }
    }
    public void FinishChest(int slot)
    {
        StartCoroutine(FinishChestAsync(slot));
    }
    private IEnumerator FinishChestAsync(int slot)
    {
        string json = "";
        Dictionary<string, string> form = new Dictionary<string, string> {{ "slot", $"{slot}" }};
        var cor = Http.HttpQurey(answer => json = answer, "chestsReward", form);
        yield return cor;
        chestReward.SetActive(true);
        StickerManager.ChangeStick(slot + 4, 0);
        chestReward.GetComponent<ChestReward>().SetReward(json);
        chestsSlots[slot].GetComponent<ChestSlota>().Slots[slotRar[slot]].SetActive(false);
        slotTime[slot] = 0;
        slotRar[slot] = -666;
    }
}