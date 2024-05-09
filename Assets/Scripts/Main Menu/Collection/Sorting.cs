using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Sorting : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textSort;
    public static Coroutine showUnits;
    public List<GameObject> tempList = new();
    [SerializeField] private GameObject content;
    private List<CardVeiw> tableList = new();

    public void SetSortButton(int num) => SetSort(num, 1);
    public void SetSort(int num, int mode)
    {
        PlayerData.sorting = num;
        PlayerPrefs.SetInt("sorting", num);
        Sort();
        if (mode == 0) StartCoroutine(ShowStart());
        else ShowInstantly();
        if (num == 1)
        {
            if (PlayerData.language == 0)
                textSort.text = "Sorting: Damage";
            else if (PlayerData.language == 1)
                textSort.text = "Сортировать: Урон";
        }
        else if (num == 2)
        {
            if (PlayerData.language == 0)
                textSort.text = "Sorting: HP";
            else if (PlayerData.language == 1)
                textSort.text = "Сортировать: Здоровье";
        }
        else if (num == 3)
        {
            if (PlayerData.language == 0)
                textSort.text = "Sorting: Level";
            else if (PlayerData.language == 1)
                textSort.text = "Сортировать: Уровень";
        }
        else if (num == 4)
        {
            if (PlayerData.language == 0)
                textSort.text = "Sorting: Grade";
            else if (PlayerData.language == 1)
                textSort.text = "Сортировать: Слияние";

        }
    }
    private void OnEnable()
    {
        SetSort(PlayerPrefs.GetInt("sorting", 1), 0);
    }
    public void Sort()
    {
        tableList.Clear();
        for (int i = 0; i < content.transform.childCount; i++)
        {
            tableList.Add(content.transform.GetChild(i).GetComponent<CardVeiw>());
        }
        //По урону!
        if (PlayerData.sorting == 1)
        {
            tableList.OrderByDescending(card => card.Damage).ToList();
            for (int i = 0; i < tableList.Count; i++)
            {
                tableList[i].transform.SetSiblingIndex(i);
            }
        }

        // Сортировка по хп!
        else if (PlayerData.sorting == 2)
        {
            tableList.OrderByDescending(card => card.Hp).ToList();
            for (int i = 0; i < tableList.Count; i++)
            {
                tableList[i].transform.SetSiblingIndex(i);
            }
        }

        // Сортировка по уровню!
        else if (PlayerData.sorting == 3)
        {
            tableList.OrderByDescending(card => card.Level).ToList();
            for (int i = 0; i < tableList.Count; i++)
            {
                tableList[i].transform.SetSiblingIndex(i);
            }
        }

        // Сортировка по Слиянию!
        else if (PlayerData.sorting == 4)
        {
            tableList.OrderByDescending(card => card.Grade).ToList();
            for (int i = 0; i < tableList.Count; i++)
            {
                tableList[i].transform.SetSiblingIndex(i);
            }
        }
    }
    private IEnumerator ShowStart()
    {
        if (PlayerData.troops.Count != 0)
        {
            for (int i = 0; i < PlayerData.troops.Count; i++)
            {
                tableList[i].gameObject.SetActive(true);
            }
        }

        for (int i = 0; i < tempList.Count; i++)
        {
            tempList[i].SetActive(true);
            tempList[i].GetComponent<Animator>().SetTrigger("show");
            yield return new WaitForSeconds(0.03f);
        }
        showUnits = null;
    }
    public void Again()
    {
        if (showUnits != null) StopCoroutine(showUnits);
        for (int i = 0; i < tableList.Count; i++)
        {
            tableList[i].gameObject.SetActive(false);
        }
    }
    public void ShowInstantly()
    {
        if (showUnits != null) StopCoroutine(showUnits);
        for (int i = 0; i < tableList.Count; i++)
            tableList[i].gameObject.SetActive(true);
    }
}