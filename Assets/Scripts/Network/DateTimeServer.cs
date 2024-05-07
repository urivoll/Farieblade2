using Newtonsoft.Json;
using System.Collections;
using UnityEngine;

public class DateTimeServer : MonoBehaviour
{
    public static int serverTime;
    public static int dayOfYear;
    public static int weekOfYear;
    public static int lastEnter;
    public static int dayInRow;

    [SerializeField] private DailyBonus _dailyBouns;

    public IEnumerator LoadLocalData()
    {
        string json = "";
        var cor = Http.HttpQurey(answer => json = answer, "dailyBouns");
        yield return cor;
        DailyConvert obj = JsonConvert.DeserializeObject<DailyConvert>(json);
        if(obj.show == 1)
            _dailyBouns.ShowDailyPanel(obj.id23, obj.id24, obj.dayInRow);
        if (obj.TaskIdW6 != -666)
            TaskManager.Weekly[6] = obj.TaskIdW6;
        if (obj.taskProgressWeekly != -666)
            TaskManager.taskProgressWeekly = obj.taskProgressWeekly;
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
