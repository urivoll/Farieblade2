using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Dealer : MonoBehaviour
{
    public static int[] Product = new int[5];
    public static int[] Amount = new int[5];
    public static int[] Price = new int[5];
    public static int[] ProductSell = new int[5];
    public static int[] ProductSellAmount = new int[5];
    public static int Time;
    [SerializeField] private GameObject[] Items;
    [SerializeField] private GameObject[] ItemsSell;
    [HideInInspector] public GameObject CurrentObj;
    private void OnEnable()
    {
        StartCoroutine(ShowAsync());
    }
    private IEnumerator ShowAsync()
    {
        Coroutine cor;
        if (DateTimeServer.dayOfYear != Time)
        {
            cor = StartCoroutine(ChangeItems());
            yield return cor;
            Show();
        }
        else Show();
    }
    private void Show()
    {
        for (int i = 0; i < Product.Length; i++)
        {
            Items[Product[i]].SetActive(true);
            Items[Product[i]].transform.Find("Amount").GetComponent<TextMeshProUGUI>().text = Amount[i].ToString();
            Items[Product[i]].transform.Find("Price").GetComponent<TextMeshProUGUI>().text = Price[i].ToString();
            if (Amount[i] == 0)
            {
                Items[Product[i]].transform.Find("Price").gameObject.SetActive(false);
                Items[Product[i]].GetComponent<Image>().color = new(100, 0, 0);
            }
            else Items[Product[i]].GetComponent<DealerCard>().id = i;

            ItemsSell[ProductSell[i]].SetActive(true);
            ItemsSell[ProductSell[i]].transform.Find("Amount").GetComponent<TextMeshProUGUI>().text = ProductSellAmount[i].ToString();
            ItemsSell[ProductSell[i]].transform.Find("have").GetComponent<TextMeshProUGUI>().text = Inventory.InventoryPlayer[ItemsSell[ProductSell[i]].GetComponent<InvenoryShowItem>().id].ToString();
            if (ProductSellAmount[i] == 0)
            {
                ItemsSell[ProductSell[i]].transform.Find("Price").gameObject.SetActive(false);
                ItemsSell[ProductSell[i]].GetComponent<Image>().color = new(100, 0, 0);
            }
            else ItemsSell[ProductSell[i]].GetComponent<DealerCard>().id = i;
        }
    }
    private void OnDisable()
    {
        for(int i = 0; i< Product.Length; i++)
        {
            Items[i].transform.Find("Price").gameObject.SetActive(true);
            Items[i].SetActive(false);
            Items[i].GetComponent<Image>().color = new(255, 255, 255);
            Items[Product[i]].GetComponent<DealerCard>().id = -1;
            ItemsSell[i].SetActive(false);
            ItemsSell[i].GetComponent<Image>().color = new(255, 255, 255);
            ItemsSell[ProductSell[i]].GetComponent<DealerCard>().id = -1;
        }
        CurrentObj = null;
    }
    private IEnumerator ChangeItems()
    {
        LoadingManager.LoadingIcon.SetActive(true);
        Coroutine cor;
        int rand;
        List<int> tempItems = new List<int>();
        for (int i = 0; i < Items.Length; i++) tempItems.Add(i);
        for(int i = 0; i < Product.Length; i++)
        {
            rand = Random.Range(0, tempItems.Count);
            Product[i] = tempItems[rand];
            if (rand < 6)
            {
                Amount[i] = Random.Range(10, 20);
                Price[i] = Amount[i] * 15;
            }
            else if (rand > 5 && rand < 11)
            {
                Amount[i] = Random.Range(5, 10);
                Price[i] = Amount[i] * 50;
            }
            else
            {
                Amount[i] = Random.Range(1, 5);
                Price[i] = Amount[i] * 150;
            }
            cor = StartCoroutine(DataBase.UpdateData($"id{i}", "Dealer", Product[i]));
            yield return cor;
            cor = StartCoroutine(DataBase.UpdateData($"id{i}Amount", "Dealer", Amount[i]));
            yield return cor;
            cor = StartCoroutine(DataBase.UpdateData($"id{i}Price", "Dealer", Price[i]));
            yield return cor;
            tempItems.RemoveAt(rand);
        }
        tempItems.Clear();
        for (int i = 0; i < Items.Length; i++) tempItems.Add(i);
        for (int i = 0; i < ProductSell.Length; i++)
        {
            rand = Random.Range(0, tempItems.Count);
            ProductSell[i] = tempItems[rand];
            if (rand < 6)
            {
                ProductSellAmount[i] = Random.Range(10, 20);
            }
            else if (rand > 5 && rand < 11)
            {
                ProductSellAmount[i] = Random.Range(5, 10);
            }
            else
            {
                ProductSellAmount[i] = Random.Range(1, 5);
            }
            cor = StartCoroutine(DataBase.UpdateData($"id{i}Sell", "Dealer", ProductSell[i]));
            yield return cor;
            cor = StartCoroutine(DataBase.UpdateData($"id{i}AmountSell", "Dealer", ProductSellAmount[i]));
            yield return cor;
            tempItems.RemoveAt(rand);
        }
        tempItems.Clear();
        Time = DateTimeServer.dayOfYear;
        cor = StartCoroutine(DataBase.UpdateData($"time", "Dealer", Time));
        yield return cor;
        LoadingManager.LoadingIcon.SetActive(false);
    }
    public void SetCurrent(GameObject obj) => CurrentObj = obj;
    public void Buy(int i)
    {
        if(i == 1)
            StartCoroutine(BuyAsync());
        else StartCoroutine(SellAsync());
    }
    private IEnumerator BuyAsync()
    {
        Coroutine cor;
        if(CurrentObj.GetComponent<DealerCard>().id != -1 && Inventory.InventoryPlayer[23] >= Price[CurrentObj.GetComponent<DealerCard>().id])
        {
            LoadingManager.LoadingIcon.SetActive(true);
            int i = CurrentObj.GetComponent<DealerCard>().id;
            int i2 = CurrentObj.GetComponent<InvenoryShowItem>().id;
            Inventory.InventoryPlayer[23] -= Price[i];
            PlayerData.ChangeGoldAF();
            Inventory.InventoryPlayer[i2] += Amount[i];
            Amount[i] = 0;
            Show();
            cor = StartCoroutine(DataBase.UpdateData($"id{i}Amount", "Dealer", 0));
            yield return cor;
            cor = StartCoroutine(DataBase.UpdateData("gold", "Gamers", Inventory.InventoryPlayer[23]));
            yield return cor;
            cor = StartCoroutine(DataBase.UpdateData($"id{i2}", "Inventory", Inventory.InventoryPlayer[i2]));
            yield return cor;
            LoadingManager.LoadingIcon.SetActive(false);
        }
    }
    private IEnumerator SellAsync()
    {
        Coroutine cor;
        if (CurrentObj.GetComponent<DealerCard>().id != -1 && Inventory.InventoryPlayer[CurrentObj.GetComponent<InvenoryShowItem>().id] > 0)
        {
            LoadingManager.LoadingIcon.SetActive(true);
            int money = 0;
            int i = CurrentObj.GetComponent<DealerCard>().id;
            int i2 = CurrentObj.GetComponent<InvenoryShowItem>().id;
            if (Inventory.InventoryPlayer[i2] >= ProductSellAmount[i])
            {
                Inventory.InventoryPlayer[i2] -= ProductSellAmount[i];
                money = ProductSellAmount[i] * CurrentObj.GetComponent<DealerCard>().price;
                ProductSellAmount[i] = 0;
            }
            else
            {
                money = (Inventory.InventoryPlayer[i2] - ProductSellAmount[i]) * -1 * CurrentObj.GetComponent<DealerCard>().price;
                ProductSellAmount[i] -= Inventory.InventoryPlayer[i2];
                Inventory.InventoryPlayer[i2] = 0;
            }
            Show();
            if (CurrentObj.GetComponent<DealerCard>().currency == "gold")
            {
                Inventory.InventoryPlayer[23] += money;
                cor = StartCoroutine(DataBase.UpdateData("gold", "Gamers", Inventory.InventoryPlayer[23]));
                yield return cor;
            }
            else
            {
                Inventory.InventoryPlayer[24] += money;
                cor = StartCoroutine(DataBase.UpdateData("af", "Gamers", Inventory.InventoryPlayer[24]));
                yield return cor;
            }
            if (TaskManager.Daily[1] < 2)
            {
                TaskManager.Daily[1] += 1;
                PlayerData.floatNote.SetActive(true);
                PlayerData.floatNote.GetComponent<TaskFloatNote>().SetValues(1);
                cor = StartCoroutine(DataBase.UpdateData($"id1", "Tasks", TaskManager.Daily[1]));
                yield return cor;
                if (TaskManager.Daily[1] == 2)
                {
                    TaskManager.taskProgressDaily += 10;
                    cor = StartCoroutine(DataBase.UpdateData($"taskProgressDaily", "Tasks", TaskManager.taskProgressDaily));
                    yield return cor;
                    if (TaskManager.Weekly[1] < 500)
                    {
                        TaskManager.Weekly[1] += 10;
                        cor = StartCoroutine(DataBase.UpdateData($"idW1", "Tasks", TaskManager.Weekly[1]));
                        yield return cor;
                        if (TaskManager.Weekly[1] >= 500)
                        {
                            TaskManager.Weekly[1] = 500;
                            TaskManager.taskProgressWeekly += 10;
                            cor = StartCoroutine(DataBase.UpdateData($"taskProgressWeekly", "Tasks", TaskManager.taskProgressWeekly));
                            yield return cor;
                        }
                    }
                }
            }
            PlayerData.ChangeGoldAF();
            cor = StartCoroutine(DataBase.UpdateData($"id{i}AmountSell", "Dealer", ProductSellAmount[i]));
            yield return cor;

            cor = StartCoroutine(DataBase.UpdateData($"id{i2}", "Inventory", Inventory.InventoryPlayer[i2]));
            yield return cor;
            LoadingManager.LoadingIcon.SetActive(false);
        }
    }
}
