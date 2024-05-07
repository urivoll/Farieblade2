using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Http : MonoBehaviour
{
    static private StringArray[] warning;
    [SerializeField] private StringArray[] warningPrefub;
    private void Start()
    {
        warning = warningPrefub;
    }
    public static IEnumerator HttpQurey(Action<string> callback, string where, Dictionary<string, string> what = null)
    {
        LoadingManager.LoadingIcon.SetActive(true);
        string answer = "";
        WWWForm form = new();
        int count = 0;
        if (what != null)
        {
            foreach (var i in what)
            {
                print($"#{count} Элемент: " + i.Key + " - " + i.Value);
                form.AddField(i.Key, i.Value);
                count++;
            }
        }
        form.AddField("id", FirstStart.newProdID);
        form.AddField("password", FirstStart.newPassword);
        UnityWebRequest request = UnityWebRequest.Post($"http://localhost/test/{where}.php", form);
        yield return request.SendWebRequest();
        //print("Куда: " + where);
        if (request.result != UnityWebRequest.Result.Success)
        {
            print(request.downloadHandler.text);
            request.Dispose();
            PlayerData.lostConnection.SetActive(true);
            while (true)
            {
                yield return null;
            }
        }
        else
        {
            answer = request.downloadHandler.text;
            print("Ответ: - " + answer);
            request.Dispose();
            if (answer == "-555")
            {
                PlayerPrefs.DeleteAll();
                PlayerData.auth.SetActive(true);
            }
            else if (answer == "-554" || answer == "-552" || answer == "-551")
            {
                PlayerData.warning.SetActive(true);
                PlayerData.textWarning.text = warning[0].intArray[PlayerData.language];
            }
            else if (answer == "-553")
            {
                PlayerData.warning.SetActive(true);
                PlayerData.textWarning.text = warning[1].intArray[PlayerData.language];
            }
            else
            {
                callback(answer);
            }
        }
        LoadingManager.LoadingIcon.SetActive(false);
    }
}
