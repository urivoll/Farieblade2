using Newtonsoft.Json;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ChestReward : MonoBehaviour
{
    [SerializeField] private GameObject[] chests;
    [SerializeField] private AudioClip Open;
    [SerializeField] private GameObject[] items;
    [SerializeField] private GameObject[] Frags;
    [SerializeField] private GameObject Reward;
    [SerializeField] private GameObject Take;
    private int Rare;
    public void SetReward(string json)
    {
        StartCoroutine(SetRewardAsync(json));
    }
    private IEnumerator SetRewardAsync(string json)
    {
        Rar obj = JsonConvert.DeserializeObject<Rar>(json);
        Rare = obj.rar;
        Sound.sound.PlayOneShot(Open);
        chests[Rare].SetActive(true);
        yield return new WaitForSeconds(1.5f);
        Reward.SetActive(true);
        Frags[Rare].SetActive(true);

        items[0].transform.Find("Text").GetComponent<TextMeshProUGUI>().text = obj.id0.ToString();
        Inventory.InventoryPlayer[23] += obj.id0;
        items[1].transform.Find("Text").GetComponent<TextMeshProUGUI>().text = obj.id1.ToString();
        Inventory.InventoryPlayer[24] += obj.id1;
        items[2].transform.Find("Text").GetComponent<TextMeshProUGUI>().text = obj.id2.ToString();
        Inventory.InventoryPlayer[19] += obj.id2;
        Frags[Rare].transform.Find("Text").GetComponent<TextMeshProUGUI>().text = obj.id3.ToString();
        Inventory.InventoryPlayer[Frags[Rare].GetComponent<InvenoryShowItem>().id] += obj.id3;

        PlayerData.ChangeGoldAF();
        Take.SetActive(true);
        if (obj.TaskIdD5 != -666)
        {
            TaskManager.Daily[5] = obj.TaskIdD5;
            PlayerData.floatNote.SetActive(true);
            PlayerData.floatNote.GetComponent<TaskFloatNote>().SetValues(5);
        }
        if (obj.taskProgressDaily != -666)
        {
            TaskManager.taskProgressDaily = obj.taskProgressDaily;
        }
        if (obj.taskProgressWeekly != -666)
        {
            TaskManager.taskProgressWeekly = obj.taskProgressWeekly;
        }
        if (obj.TaskIdW1 != -666)
        {
            TaskManager.Weekly[1] = obj.TaskIdW1;
        }
        if (obj.TaskIdW5 != -666)
        {
            TaskManager.Weekly[5] = obj.TaskIdW5;
        }
    }
    public void OnDisable()
    {
        Frags[Rare].SetActive(false);
        Take.SetActive(false);
        Reward.SetActive(false);
        gameObject.SetActive(false);
        for (int i = 0; i < chests.Length; i++)
        {
            chests[i].SetActive(false);
        }
    }
}
public class Rar
{
    public int id0;
    public int id1;
    public int id2;
    public int id3;
    public int rar;
    public int TaskIdD5;
    public int TaskIdW5;
    public int TaskIdW1;
    public int taskProgressDaily;
    public int taskProgressWeekly;
}