using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ChangePortrait : MonoBehaviour
{
    [SerializeField] private GameObject accountPortrait;
    [SerializeField] private Button[] buttons;
    [SerializeField] private StringArray[] warning;
    private void OnEnable()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (PlayerData.myCollection[i].GetComponent<Unit>().level != 0) buttons[i].interactable = true;
            else buttons[i].interactable = false;
        }
    }
    public void SetPortrait(int id)
    {
        StartCoroutine(SetPortraitAsync(id));
    }
    public IEnumerator SetPortraitAsync(int id)
    {
        string json = "";
        Dictionary<string, string> form = new Dictionary<string, string>
        {
            { "action", "changePortrait" },
            { "idUnit", id.ToString() },
        };
        var cor = Http.HttpQurey(answer => json = answer, "account/smallChanges", form);
        yield return cor;
        if (json == "-2" || json == "-3")
        {
            PlayerData.warning.SetActive(true);
            PlayerData.textWarning.text = warning[0].intArray[PlayerData.language];
        }
        else if (json == "1")
        {
            PlayerData.warning.SetActive(true);
            PlayerData.textWarning.text = warning[1].intArray[PlayerData.language];
            accountPortrait.GetComponent<Image>().sprite = PlayerData.accountPortraitIndex[id];
        }
    }
}
