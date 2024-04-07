using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DateTimeServer : MonoBehaviour
{
    public static int serverTime;
    public static int dayOfYear;
    public static int weekOfYear;
    public static int lastEnter;
    public static int dayInRow;

    [SerializeField] private PlayerData playerData;
    [SerializeField] private string serverUrl;
    [SerializeField] private Demo _demo;
    public IEnumerator SendRequest()
    {
        var cor = StartCoroutine(GetTime());
        yield return cor;
        StartCoroutine(playerData.AfterConnect());
    }
    public IEnumerator LoadLocalData()
    {
        string json = "";
        var cor = Http.HttpQurey(answer => json = answer, "dailyBouns");
        yield return cor;
        DailyConvert obj = JsonConvert.DeserializeObject<DailyConvert>(json);
        if(obj.show == 1)
            _demo.ShowDailyPanel(obj.id23, obj.id24, obj.dayInRow);
        if (obj.TaskIdW6 != -666)
        {
            TaskManager.Weekly[6] = obj.TaskIdW6;
        }
        if (obj.taskProgressWeekly != -666)
        {
            TaskManager.taskProgressWeekly = obj.taskProgressWeekly;
        }
    }
    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus && PlayerData.focus)
            StartCoroutine(GetTime());
    }
    public static IEnumerator GetTime()
    {
        string json = "";
        var cor = Http.HttpQurey(answer => json = answer, "getTime");
        yield return cor;
        print(json);
        StartDataResponse response = JsonUtility.FromJson<StartDataResponse>(json);
        dayOfYear = response.day_of_year;
        weekOfYear = response.week_number;
        serverTime = response.unixtime;
    }
}
public class DailyConvert
{
    public int id23;
    public int id24;
    public int show;
    public int dayInRow;
    public int TaskIdW6;
    public int taskProgressWeekly;
}
