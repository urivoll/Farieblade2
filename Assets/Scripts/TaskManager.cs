using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static int[] Daily = new int[7];
    public static int[] Weekly = new int[7];
    public static int[] DailyGive = new int[5];
    public static int[] WeeklyGive = new int[5];
    public static int taskProgressDaily;
    public static int taskProgressWeekly;
    public static int Day;
    public static int Week;
}
