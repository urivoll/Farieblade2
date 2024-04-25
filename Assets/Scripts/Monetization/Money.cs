using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
public class Money : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] private string androidGameID = "5501866";
    [SerializeField] private string iosGameID = "5501867";
    [SerializeField] bool testMode = true;
    private string gameID;
    private void Awake()
    {
        InitializeAds();
    }
    public void InitializeAds()
    {
        gameID = (Application.platform == RuntimePlatform.IPhonePlayer) ? iosGameID : androidGameID;
        Advertisement.Initialize(gameID, testMode, this);
    }

    public void OnInitializationComplete()
    {
        //print("Получилось!");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        print("Не получилось!");
    }
}
