using System.Collections;
using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.Networking;
using System.Collections.Generic;
using System;

public class ChangeNick : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputText;
    [SerializeField] private TextMeshProUGUI textNickname;
    [SerializeField] private StringArray[] warning;
    public void SetNewNick()
    {
        StartCoroutine(SetNewNick2());
    }
    public void OnDisable()
    {
        inputText.text = "";
    }
    public IEnumerator SetNewNick2()
    {
        string json = "";
        Dictionary<string, string> form = new Dictionary<string, string>
        {
            { "action", "changeNick" },
            { "nick", inputText.text }
        };
        var cor = Http.HttpQurey(answer => json = answer, "account/smallChanges", form);
        yield return cor;
        if (json == "-1")
        {
            PlayerData.warning.SetActive(true);
            PlayerData.textWarning.text = warning[0].intArray[PlayerData.language];
        }
        else if (json == "-2")
        {
            PlayerData.warning.SetActive(true);
            PlayerData.textWarning.text = warning[1].intArray[PlayerData.language];
        }
        else if (json == "-3")
        {
            PlayerData.warning.SetActive(true);
            PlayerData.textWarning.text = warning[2].intArray[PlayerData.language];
        }
        else if (json == "1")
        {
            PlayerData.warning.SetActive(true);
            PlayerData.textWarning.text = warning[3].intArray[PlayerData.language];
            textNickname.text = inputText.text;
        }
    }
}
