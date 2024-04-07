using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskReward : MonoBehaviour
{
    [SerializeField] private GameObject[] RewardSlot;
    private int[,] ArrayD;
    private int[,] ArrayDAmount;
    private int[,] ArrayW;
    private int[,] ArrayWAmount;
    private int[,] currentArray;
    private int[,] currentArrayAmount;
    [SerializeField] private AudioClip money;
    [SerializeField] private AudioClip endTurn;
    private void Awake()
    {
              ArrayD = new int[,] { { 23, 0 }, { 23, 0 }, { 23, 1 }, { 24, 0 }, { 24, 1 } };
        ArrayDAmount = new int[,] { { 50, 5 }, { 100, 10 }, { 5, 5 }, { 10, 10 }, { 15, 10 } };
              ArrayW = new int[,] { { 23, 0 }, { 23, 1 }, { 24, 0 }, { 24, 1 }, { 24, 2 } };
        ArrayWAmount = new int[,] { { 200, 15 }, { 300, 15 }, { 20, 5 }, { 25, 10 }, { 30, 10 } };
    }
    public void SetReward(int slot, int kind)
    {
        Sound.sound.PlayOneShot(money);
        Sound.sound.PlayOneShot(endTurn);
        if (kind == 0)
        {
            currentArray = ArrayD;
            currentArrayAmount = ArrayDAmount;
        }
        else
        {
            currentArray = ArrayW;
            currentArrayAmount = ArrayWAmount;
        }
        for (int i = 0; i < 2; i++)
        {
            GameObject tempObj = RewardSlot[Decode(currentArray[slot, i])];
            tempObj.SetActive(true);
            tempObj.GetComponentInChildren<TextMeshProUGUI>().text = currentArrayAmount[slot, i].ToString();
            Inventory.InventoryPlayer[currentArray[slot, i]] += currentArrayAmount[slot, i];
            PlayerData.ChangeGoldAF();
        }
    }
    private void OnDisable()
    {
        for(int i = 0; i< RewardSlot.Length; i++)
        {
            RewardSlot[i].SetActive(false);
        }
    }
    private int Decode(int num)
    {
        if (num == 23) return 3;
        else if (num == 24) return 4;
        else return num;
    }
}
