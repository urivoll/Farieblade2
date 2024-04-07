using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskDaily : MonoBehaviour
{
    [SerializeField] private Image[] Bars;
    [SerializeField] private int[] Need;
    [SerializeField] private TextMeshProUGUI[] textDailyNow;
    [SerializeField] private TextMeshProUGUI DailyProgress;
    [SerializeField] private int[] NeedPoints;
    [SerializeField] private Button[] Button;
    [SerializeField] private GameObject chestReward;
    private void OnEnable()
    {
        if (StickerManager.things[1] == 1)
        {
            StickerManager.ChangeStick(1, 0);
        }
        for (int i = 0; i < TaskManager.Daily.Length; i++)
        {
            textDailyNow[i].text = TaskManager.Daily[i].ToString();
            Bars[i].fillAmount = Convert.ToSingle(TaskManager.Daily[i]) * 100f / Need[i] / 100f;
            DailyProgress.text = TaskManager.taskProgressDaily.ToString();
        }
        for (int i = 0; i < TaskManager.DailyGive.Length; i++)
        {
            if(TaskManager.taskProgressDaily >= NeedPoints[i])
            {
                if(TaskManager.DailyGive[i] == 0)
                {
                    Button[i].interactable = true;
                    StickerManager.ChangeStick(i + 17, 1);
                }
                else Button[i].gameObject.SetActive(false);
            }
            else Button[i].interactable = false;
        }
    }
    private void OnDisable()
    {
        for (int i = 0; i < TaskManager.DailyGive.Length; i++)
        {
            Button[i].gameObject.SetActive(true);
        }
    }
    public void FinishChestDaily(int rar)
    {
        StartCoroutine(FinishChestAsync(rar));
    }
    private IEnumerator FinishChestAsync(int slot)
    {
        string json = "";
        Dictionary<string, string> form = new Dictionary<string, string>
            {
                { "slot", $"{slot}" },
                { "kind", $"D" }
            };
        var cor = Http.HttpQurey(answer => json = answer, "taskReward", form);
        yield return cor;
        if (json == "1")
        {
            TaskManager.WeeklyGive[slot] = 1;
            Button[slot].gameObject.SetActive(false);
            StickerManager.ChangeStick(slot + 17, 0);
            chestReward.SetActive(true);
            chestReward.GetComponent<TaskReward>().SetReward(slot, 0);
        }
    }
}
