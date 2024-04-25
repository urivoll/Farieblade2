using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class SignInUp : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputEmail;
    [SerializeField] private TMP_InputField inputPassword;
    [SerializeField] private TMP_InputField inputPasswordCon;
    [SerializeField] private StringArray[] warning;
    public void CreateNewAccount()
    {
        StartCoroutine(CreateNewAccountAsync());
    }
    public IEnumerator CreateNewAccountAsync()
    {
        string json = "";
        Dictionary<string, string> form = new Dictionary<string, string>
        {
            { "action", "changePortrait" },
            { "name", inputEmail.text },
            { "newPassword", inputPassword.text },
            { "newPasswordCon", inputPasswordCon.text }
        };
        var cor = Http.HttpQurey(answer => json = answer, "account/registr", form);
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
        else if (json == "-4")
        {
            PlayerData.warning.SetActive(true);
            PlayerData.textWarning.text = warning[3].intArray[PlayerData.language];
        }
        else if (json == "-5")
        {
            PlayerData.warning.SetActive(true);
            PlayerData.textWarning.text = warning[4].intArray[PlayerData.language];
        }
        else if (json == "-6")
        {
            PlayerData.warning.SetActive(true);
            PlayerData.textWarning.text = warning[5].intArray[PlayerData.language];
        }
        else if (json == "1")
        {
            PlayerData.warning.SetActive(true);
            PlayerData.textWarning.text = warning[6].intArray[PlayerData.language];
            PlayerPrefs.SetString("user_password", inputPassword.text);
            inputEmail.text = "";
            inputPassword.text = "";
            inputPasswordCon.text = "";
            FirstStart.newPassword = inputPassword.text;
        }
    }
    public void OnDisable()
    {
        inputEmail.text = "";
        inputPassword.text = "";
        inputPasswordCon.text = "";
    }
}