using Spine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinsManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Skins;
    [SerializeField] private TextMeshProUGUI[] SkinName;
    [SerializeField] private GameObject[] SkinClosed;
    private GameObject[] SkinsObj;
    [SerializeField] private Button ButtonWear;
    private int index2;
    public GameObject obj;
    public void SetButtons(ModelPanel modelPanel)
    {
        SkinsObj = modelPanel.Skins;
        for (int i = 0; i < SkinsObj.Length; i++)
        {
            if (i != 0)
            {
                Skins[i].SetActive(true);
                SkinName[i].text = SkinsObj[i].GetComponent<Skin>().Name[PlayerData.language];
                if (IdUnit.idSkin[SkinsObj[i].GetComponent<Skin>().id] == 0)
                    SkinClosed[i].SetActive(true);
                else SkinClosed[i].SetActive(false);
            }
        }
    }
    public void SetSkin(int index)
    {
        index2 = index;
        transform.parent.GetComponent<PanelProperties>()._avatarObject.GetComponent<ModelPanel>().SetSkin(index);
        if (index != 0)
        {
            if (IdUnit.idSkin[SkinsObj[index].GetComponent<Skin>().id] == 1) ButtonWear.interactable = true;
            else ButtonWear.interactable = false;
        }
        else ButtonWear.interactable = true;
    }
    public void SetSkin2()
    {
        StartCoroutine(SetSkin2Async());
    }
    private IEnumerator SetSkin2Async()
    {
        int id = obj.GetComponent<Unit>().ID;
        string json = "";
        Dictionary<string, string> form = new Dictionary<string, string> 
        { 
            { "skin", $"{index2}" },
            { "unit", $"{id}" }
        };
        var cor = Http.HttpQurey(answer => json = answer, "skinCheck", form);
        yield return cor;
        if(json == "1")
        {
            //PlayerData.myCollection[id].GetComponent<Unit>().currentSkin = index2;
            IdUnit.idSkinWear[id] = index2;
        }
    }
    private void OnDisable()
    {
        //transform.parent.GetComponent<PanelProperties>()._avatarObject.GetComponent<ModelPanel>().SetSkin(Turns.unitChoose.transform.parent.parent.GetComponent<Unit>().currentSkin);
    }
    public void Exit()
    {
        for (int i = 0; i < Skins.Length; i++)
        {
            if (i != 0)
            {
                Skins[i].SetActive(false);
            }
        }
    }
}