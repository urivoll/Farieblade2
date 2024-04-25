using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LogIn : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputEmail;
    [SerializeField] private TMP_InputField inputPassword;
    [SerializeField] private StringArray[] warning;
    public void DataBaseSelectLogin()
    {
        StartCoroutine(DataBaseSelectLoginAsync());
    }
    public IEnumerator DataBaseSelectLoginAsync()
    {
        LoadingManager.LoadingIcon.SetActive(true);
        WWWForm form = new();
        form.AddField("name", inputEmail.text);
        form.AddField("password", inputPassword.text);
        UnityWebRequest request = UnityWebRequest.Post("http://46.8.21.206/test/account/login.php", form);
        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
        {
            PlayerData.warning.SetActive(true);
            PlayerData.textWarning.text = warning[0].intArray[PlayerData.language];
        }
        else
        {
            if(request.downloadHandler.text == "-1" || request.downloadHandler.text == "-1-1")
            {
                PlayerData.warning.SetActive(true);
                PlayerData.textWarning.text = warning[1].intArray[PlayerData.language];
            }
            else
            {
                PlayerPrefs.DeleteAll();
                PlayerData.afterFight = 1;
                PlayerPrefs.SetInt("user_id", Convert.ToInt32(request.downloadHandler.text));
                PlayerPrefs.SetInt("user_password", Convert.ToInt32(inputPassword));
                yield return new WaitForSeconds(0.3f);
                LoadingManager.LoadingIcon.SetActive(false);
                SceneManager.LoadScene(0);
            }
        }
        request.Dispose();
        LoadingManager.LoadingIcon.SetActive(false);
    }
    public void OnDisable()
    {
        inputEmail.text = "";
        inputPassword.text = "";
    }
}
