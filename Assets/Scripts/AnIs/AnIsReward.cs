using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class AnIsReward : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textSuccsess;
    [SerializeField] private TextMeshProUGUI textReturn;
    [SerializeField] private TextMeshProUGUI textReward;
    [SerializeField] private GameObject rewardObj;
    [SerializeField] private GameObject reward;
    [SerializeField] private Image image;
    [SerializeField] private Sprite luck;
    [SerializeField] private Sprite nonLuck;
    [SerializeField] private AnIsMap anIs;
    [SerializeField] private GameObject[] Item;
    [SerializeField] private AudioClip[] winLose;
    public void SetSuccess(bool lucky)
    {
        if (lucky)
        {
            Sound.sound.PlayOneShot(winLose[0]);
            reward.SetActive(true);
            image.sprite = luck;
            if (PlayerData.language == 0)
            {
                textSuccsess.text = "Succsess!";
                textReturn.text = "Get reward!";
                textReward.text = "Reward";
            }
            else if (PlayerData.language == 1)
            {
                textSuccsess.text = "Успех!";
                textReturn.text = "Получить награду!";
                textReward.text = "Награда";
            } 
        }
        else
        {
            Sound.sound.PlayOneShot(winLose[1]);
            reward.SetActive(false);
            image.sprite = nonLuck;
            if (PlayerData.language == 0)
            {
                textSuccsess.text = "Failure";
                textReturn.text = "Ok";
                textReward.text = "Better luck next time";
            }
            else if (PlayerData.language == 1)
            {
                textSuccsess.text = "Неудача";
                textReturn.text = "Ок";
                textReward.text = "Повезет в другой раз";
            }
        }
    }

    public void CheckLocationAndReward(int location)
    {

        StartCoroutine(CheckLocationAndReward2(location));
    }
    private IEnumerator CheckLocationAndReward2(int location)
    {
        for (int i = 0; i < Campany.anIsLocation.Length; i++)
        {
            if (Campany.anIsLocation[i] == location)
            {
                string json = "";
                Dictionary<string, string> form = new Dictionary<string, string> { { "index", $"{i}" } };
                var cor2 = Http.HttpQurey(answer => json = answer, "anIs/checkTime", form);
                yield return cor2;
                if (json[0] == '+')
                {
                    var cor = StartCoroutine(DateTimeServer.GetTime());
                    yield return cor;
                    Campany.anIsTimeNeed[i] = Convert.ToInt32(json.Substring(1));
                }
                else
                {
                    rewardObj.SetActive(true);
                    if (json == "0")
                    {
                        SetSuccess(false);
                    }
                    else
                    {
                        ItemsIn obj = JsonConvert.DeserializeObject<ItemsIn>(json);
                        List<List<int>> tempList = new List<List<int>>();
                        tempList.Add(obj.id0);
                        tempList.Add(obj.id1);
                        tempList.Add(obj.id2);
                        tempList.Add(obj.id3);
                        for (int i2 = 0; i2 < tempList.Count; i2++)
                        {
                            int idItem = tempList[i2][0];
                            int amount = tempList[i2][1];
                            if (tempList[i2][0] != -666)
                            {
                                int index = GetComponent<DefinitionId>().unCode(idItem);
                                Item[index].SetActive(true);
                                Item[index].transform.Find("Amount").GetComponent<TextMeshProUGUI>().text = amount.ToString();
                                Inventory.InventoryPlayer[idItem] += amount;
                            }
                        }
                        PlayerData.ChangeGoldAF();
                        if (Campany.anIsProgress == Campany.anIsLocation[i])
                            Campany.anIsProgress += 1;
                        SetSuccess(true);
                    }
                    StickerManager.ChangeStick(location + 27, 0);
                    Campany.anIsTimeNeed[i] = 0;
                    foreach (GameObject i2 in PlayerData.myCollection)
                    {
                        if (i2.GetComponent<Unit>().onAnIs == Campany.anIsLocation[i])
                        {
                            i2.GetComponent<Unit>().onAnIs = -666;
                            i2.transform.Find("Card").gameObject.GetComponent<CardVeiw>().SetCardValues();
                        }
                    }
                    Campany.anIsLocation[i] = -666;
                    Campany.anIsProsent[i] = 0;
                    AnIsMap.circle[i].SetActive(false);
                    anIs.LocationsClose();
                }
            }
        }
    }
    public void ItemsDefault()
    {
        for(int i = 0; i < Item.Length; i++)
        {
            Item[i].SetActive(false);
        }
    }
}
public class ItemsIn
{
    public List<int> id0;
    public List<int> id1;
    public List<int> id2;
    public List<int> id3;
}
