using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Networking;
public class RewardedAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private UnityEngine.UI.Button button;
    [SerializeField] private string androidAdID = "Rewarded_Android";
    [SerializeField] private string iOSAdID = "Rewarded_iOS";
    [SerializeField] private GameObject obj;

    private string adID;
    private void Awake()
    {
        adID = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? iOSAdID
            : androidAdID;
        button.interactable = false;

    }
    private void Start()
    {
        LoadAd();
    }
    private void LoadAd()
    {
        print("Загрузка рекламы:" + adID);
        Advertisement.Load(adID, this);
    }
    public void ShowAd()
    {
        StartCoroutine(ShowAdAsync());
    }
    public IEnumerator ShowAdAsync()
    {
        WWWForm form = new();
        form.AddField("Id", FirstStart.newProdID);
        form.AddField("action", "test");
        UnityWebRequest request = UnityWebRequest.Post("http://46.8.21.206/test/freedf.php", form);
        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.Success)
        {
            print("Тут ошибка!");
            //request.Dispose();
/*            PlayerData.LostConnection.SetActive(true);
            while (true)
            {
                yield return null;
            }*/
        }
        else
        {
            if (request.downloadHandler.text == "0")
            {
                print("Истрачены все возможности");
            }
            else if(request.downloadHandler.text == "1")
            {
                button.interactable = false;
                Advertisement.Show(adID, this);
            }
            else
            {
                print(request.downloadHandler.text);
            }
        }
        request.Dispose();
    }

    public void OnUnityAdsAdLoaded(string adUnitID)
    {
        print("Реклама запустилась " + adUnitID);
        if (adUnitID.Equals(adID))
        {
            button.onClick.AddListener(ShowAd);
            button.interactable = true;
        }
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        print("Старт!");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowComplete(string adUnitID, UnityAdsShowCompletionState showCompletionState)
    {
        if(adUnitID.Equals(adID) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            //PlayerData.sound.PlayOneShot(Sounds.endTurn);
            obj.SetActive(true);
            Inventory.InventoryPlayer[24] += 5;
            PlayerData.ChangeGoldAF();
        }
    }
}
