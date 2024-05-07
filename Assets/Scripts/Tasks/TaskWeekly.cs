using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskWeekly : MonoBehaviour
{
    [SerializeField] private Image[] Bars;
    [SerializeField] private int[] Need;
    [SerializeField] private TextMeshProUGUI[] textWeeklyNow;
    [SerializeField] private TextMeshProUGUI WeeklyProgress;
    [SerializeField] private int[] NeedPoints;
    [SerializeField] private Button[] Button;
    [SerializeField] private GameObject chestReward;
    private void OnEnable()
    {
        if (StickerManager.things[2] == 1) StickerManager.ChangeStick(2, 0);
        for (int i = 0; i < TaskManager.Weekly.Length; i++)
        {
            textWeeklyNow[i].text = TaskManager.Weekly[i].ToString();
            Bars[i].fillAmount = Convert.ToSingle(TaskManager.Weekly[i]) * 100 / Need[i] / 100;
            WeeklyProgress.text = TaskManager.taskProgressWeekly.ToString();
        }
        for (int i = 0; i < TaskManager.WeeklyGive.Length; i++)
        {
            if(TaskManager.taskProgressWeekly >= NeedPoints[i])
            {
                if(TaskManager.WeeklyGive[i] == 0)
                {
                    Button[i].interactable = true;
                    StickerManager.ChangeStick(i + 22, 1);
                }
                else Button[i].gameObject.SetActive(false);
            }
            else Button[i].interactable = false;
        }
    }
    private void OnDisable()
    {
        for (int i = 0; i < TaskManager.WeeklyGive.Length; i++) Button[i].gameObject.SetActive(true);
    }
    public void FinishChestWeekly(int rar)
    {
        StartCoroutine(FinishChestAsync(rar));
    }
    private IEnumerator FinishChestAsync(int slot)
    {
        string json = "";
        Dictionary<string, string> form = new Dictionary<string, string>
            {
                { "slot", $"{slot}" },
                { "kind", $"W" }
            };
        var cor = Http.HttpQurey(answer => json = answer, "taskReward", form);
        yield return cor;
        if(json == "1")
        {
            TaskManager.WeeklyGive[slot] = 1;
            Button[slot].gameObject.SetActive(false);
            StickerManager.ChangeStick(slot + 22, 0);
            chestReward.SetActive(true);
            chestReward.GetComponent<TaskReward>().SetReward(slot, 1);
        }
    }
}
