using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public static GameObject LoadingScreen;
    public static GameObject LoadingIcon;
    public static int LoadingScreenAfter = -1;
    public static GameObject LoadingScreenAfterObj;
    public static Image LoadingScreenAfterImage;
    public static Sprite[] LoadingScreenAfterSprite;
    public static string[] LoadingScreenAfterStringEng;
    public static string[] LoadingScreenAfterStringRus;
    public static int LoadingScreenText;
    public static TextMeshProUGUI textLoadingScreenText;

    [SerializeField] private Sprite[] LoadingScreenAfterSpritePrefub;
    [SerializeField] private GameObject LoadingScreenAfterObjPrefub;
    [SerializeField] private Image LoadingScreenAfterImagePrefub;
    [SerializeField] private TextMeshProUGUI textLoadingScreenTextPrefub;
    [SerializeField] private string[] LoadingScreenAfterStringEngPrefub;
    [SerializeField] private string[] LoadingScreenAfterStringRusPrefub;
    [SerializeField] private GameObject Loading;
    [SerializeField] private GameObject LoadingIconPrefub;

    private void Awake()
    {
        LoadingScreenAfterStringEng = LoadingScreenAfterStringEngPrefub;
        LoadingScreenAfterStringRus = LoadingScreenAfterStringRusPrefub;
        textLoadingScreenText = textLoadingScreenTextPrefub;
        LoadingScreenAfterImage = LoadingScreenAfterImagePrefub;
        LoadingScreenAfterObj = LoadingScreenAfterObjPrefub;
        LoadingScreenAfterSprite = LoadingScreenAfterSpritePrefub;
        LoadingIcon = LoadingIconPrefub;
        LoadingScreen = Loading;
        LoadingIcon = LoadingIconPrefub;
        LoadingScreen = Loading;
    }
    public void SetLoading()
    {
        if (LoadingScreenAfter != -1)
            LoadingScreenAfterObj.SetActive(false);
        else LoadingScreen.SetActive(false);
    }
}
