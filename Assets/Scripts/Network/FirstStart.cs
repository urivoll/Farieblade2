using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class FirstStart : MonoBehaviour
{
    public static int newProdID;
    public static string newPassword;
    [SerializeField] private DateTimeServer timeServer;
    [SerializeField] private PlayerData playerData;
    private void Awake()
    {
        PlayerData.language = PlayerPrefs.GetInt("language", 1);
        Sound.soundLevel = PlayerPrefs.GetFloat("sound", 1);
        Sound.musicLevel = PlayerPrefs.GetFloat("music", 1);
        Sound.voiceLevel = PlayerPrefs.GetFloat("voice", 1);
        Sound.ambLevel = PlayerPrefs.GetFloat("amb", 1);
    }
    private void Start()
    {
        StartCoroutine(CheckNewPlayer());
    }
    public IEnumerator CheckNewPlayer()
    {
        if (LoadingManager.LoadingScreenAfter != -1)
        {
            LoadingManager.LoadingScreenAfterObj.SetActive(true);
            if (PlayerData.language == 0) LoadingManager.textLoadingScreenText.text = LoadingManager.LoadingScreenAfterStringEng[LoadingManager.LoadingScreenText];
            else if (PlayerData.language == 1) LoadingManager.textLoadingScreenText.text = LoadingManager.LoadingScreenAfterStringRus[LoadingManager.LoadingScreenText];
            LoadingManager.LoadingScreenAfterImage.sprite = LoadingManager.LoadingScreenAfterSprite[LoadingManager.LoadingScreenAfter];
        }
        else LoadingManager.LoadingScreen.SetActive(true);
        //PlayerPrefs.DeleteKey("user_id");
        if (PlayerPrefs.HasKey("user_id") == false)
        {
            PlayerPrefs.DeleteAll();
            WWWForm form = new();
            form.AddField("action", "insert");
            UnityWebRequest request = UnityWebRequest.Post("http://localhost/test/account/createNewAcc.php", form);
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                request.Dispose();
                PlayerData.lostConnection.SetActive(true);
            }
            else
            {
                CreateNewAcc obj = JsonConvert.DeserializeObject<CreateNewAcc>(request.downloadHandler.text);
                newProdID = obj.id;
                newPassword = obj.password;
            }
            request.Dispose();
            PlayerPrefs.SetInt("user_id", newProdID);
            PlayerPrefs.SetString("user_password", newPassword);
        }
        else
        {
            newProdID = PlayerPrefs.GetInt("user_id");
            newPassword = PlayerPrefs.GetString("user_password");
            print(newProdID);
            print(newPassword);
        }
        playerData.Init();
        var cor = StartCoroutine(DateTimeServer.GetTime());
        yield return cor;
        StartCoroutine(playerData.AfterConnect());
    }
}
public class CreateNewAcc
{
    public int id;
    public string password;
}

