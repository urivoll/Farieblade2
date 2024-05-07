using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Networking;

public class DataBase : MonoBehaviour
{
    public static string targetURL;
    [SerializeField] private static string targetURLPrefub;
    public static GameObject LostConnection;
    [SerializeField] private GameObject Lost;
    public static string startJson;
    private void Awake()
    {
        LostConnection = Lost;
    }
    public static IEnumerator UpdateData(string obj, string where, int value, string value2 = "null")
    {
        WWWForm form = new();
        form.AddField("action", "update");
        form.AddField("obj", obj);
        form.AddField("where", where);
        form.AddField("id", FirstStart.newProdID);
        form.AddField("password", FirstStart.newPassword);
        if (value2 == "null")
            form.AddField("value", value);
        else
            form.AddField("value", value2);
        UnityWebRequest request = UnityWebRequest.Post("http://46.8.21.206/test/script1.php", form);
        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
        {
            request.Dispose();
            PlayerData.lostConnection.SetActive(true);
            while (true)
            {
                yield return null;
            }
        }
        else
        {
        }
        request.Dispose();
    }
}
