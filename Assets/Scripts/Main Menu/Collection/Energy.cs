using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Energy : MonoBehaviour
{
    private bool path;
    [SerializeField] private DateTimeServer timeServer;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private StringArray[] warning;
    public static int mode;
    public void PrepareToFight(int mode2) => StartCoroutine(PrepareToFight2(mode2));
    public IEnumerator PrepareToFight2(int mode2)
    {
        mode = mode2;
        List<int> Squad = new();
        if (mode == 0) Squad = PlayerData.troops;
        else Squad = MultiplayerDraft.troops;
        path = true;
        bool onAn = false;
        for (int i = 0; i < Squad.Count; i++)
        {
            if (PlayerData.myCollection[Squad[i]].GetComponent<Unit>().onAnIs != -666) onAn = true;
        }
        if (mode == 0 && PlayerData.troopAmount == 0)
        {
            PlayerData.warning.SetActive(true);
            PlayerData.textWarning.text = warning[2].intArray[PlayerData.language];
            path = false;
        }
        if (onAn)
        {
            PlayerData.warning.SetActive(true);
            PlayerData.textWarning.text = warning[0].intArray[PlayerData.language];
            path = false;
        }
        if (!path)
        {
            PlayerData.warning.SetActive(true);
            PlayerData.textWarning.text = warning[1].intArray[PlayerData.language];
            yield break;
        }
        LoadingManager.LoadingIcon.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        LoadingManager.LoadingScreenAfter = Random.Range(0, LoadingManager.LoadingScreenAfterSprite.Length);
        LoadingManager.LoadingScreenAfterObj.SetActive(true);
        LoadingManager.LoadingScreenAfterImage.sprite = LoadingManager.LoadingScreenAfterSprite[LoadingManager.LoadingScreenAfter];
        LoadingManager.LoadingScreenText = Random.Range(0, LoadingManager.LoadingScreenAfterStringRus.Length);
        if (PlayerData.language == 0) LoadingManager.textLoadingScreenText.text = LoadingManager.LoadingScreenAfterStringEng[LoadingManager.LoadingScreenText];
        else if (PlayerData.language == 1) LoadingManager.textLoadingScreenText.text = LoadingManager.LoadingScreenAfterStringRus[LoadingManager.LoadingScreenText];
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(1);
    }
}
