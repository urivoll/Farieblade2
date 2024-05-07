using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Sorting : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textSort;
    public static Coroutine showUnits;
    public List<GameObject> tempList = new List<GameObject>();
    [SerializeField] private GameObject content;
    void Awake()
    {
        if (PlayerPrefs.HasKey("sorting") == false)
            PlayerPrefs.SetInt("sorting", 1);
    }
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
        SetSort(PlayerPrefs.GetInt("sorting"), 0);
    }
    public void Sort()
    {
        //По урону!
        if (PlayerData.sorting == 1)
        {
            int count = 0;
            while (true)
            {
                int maxDamage = -1;
                GameObject MaxDamageUnit = null;
                foreach (GameObject item in PlayerData.myCollection)
                {
                    if (item.GetComponent<Unit>().damage * item.GetComponent<Unit>().times > maxDamage &&
                        item.GetComponent<Unit>().forSort == true && item.GetComponent<Unit>().level != 0)
                    {
                        MaxDamageUnit = item;
                        maxDamage = item.GetComponent<Unit>().damage * item.GetComponent<Unit>().times;
                    }
                }
                if (MaxDamageUnit == null) break;
                MaxDamageUnit.GetComponent<Unit>().forSort = false;
                MaxDamageUnit.transform.SetSiblingIndex(count);
                //tempList.Add(MaxDamageUnit);
                count++;
            }
            while (true)
            {
                int maxDamage = -1;
                GameObject MaxDamageUnit = null;
                foreach (GameObject item in PlayerData.myCollection)
                {
                    if (item.GetComponent<Unit>().damage * item.GetComponent<Unit>().times > maxDamage &&
                        item.GetComponent<Unit>().forSort == true)
                    {
                        MaxDamageUnit = item;
                        maxDamage = item.GetComponent<Unit>().damage * item.GetComponent<Unit>().times;
                    }
                }

                if (MaxDamageUnit == null) break;
                MaxDamageUnit.GetComponent<Unit>().forSort = false;
                MaxDamageUnit.transform.SetSiblingIndex(count);
                //tempList.Add(MaxDamageUnit);
                count++;
            }
            foreach (GameObject item in PlayerData.myCollection)
            {
                item.GetComponent<Unit>().forSort = true;
            }
        }

        // Сортировка по хп!
        else if (PlayerData.sorting == 2)
        {
            int count2 = 0;
            while (true)
            {
                int MaxHp = 0;
                GameObject MaxHpUnit = null;
                foreach (GameObject item in PlayerData.myCollection)
                {
                    if (item.GetComponent<Unit>().hpBase > MaxHp &&
                        item.GetComponent<Unit>().forSort == true &&
                        item.GetComponent<Unit>().level != 0)
                    {
                        MaxHpUnit = item;
                        MaxHp = item.GetComponent<Unit>().hpBase;
                    }
                }
                if (MaxHpUnit == null) break;
                MaxHpUnit.GetComponent<Unit>().forSort = false;
                MaxHpUnit.transform.SetSiblingIndex(count2);
                count2++;
            }
            while (true)
            {
                float MaxHp = 0;
                GameObject MaxHpUnit = null;
                foreach (GameObject item in PlayerData.myCollection)
                {
                    if (item.GetComponent<Unit>().hpBase > MaxHp &&
                        item.GetComponent<Unit>().forSort == true)
                    {
                        MaxHpUnit = item;
                        MaxHp = item.GetComponent<Unit>().hpBase;
                    }
                }

                if (MaxHpUnit == null) break;
                MaxHpUnit.GetComponent<Unit>().forSort = false;
                MaxHpUnit.transform.SetSiblingIndex(count2);
                count2++;
            }
            foreach (GameObject item in PlayerData.myCollection)
            {
                item.GetComponent<Unit>().forSort = true;
            }
        }

        // Сортировка по уровню!
        else if (PlayerData.sorting == 3)
        {
            int count3 = 0;
            while (true)
            {
                int MaxLevel = 0;
                GameObject MaxLvlUnit = null;
                foreach (GameObject item in PlayerData.myCollection)
                {
                    if (item.GetComponent<Unit>().level > MaxLevel &&
                        item.GetComponent<Unit>().forSort == true &&
                        item.GetComponent<Unit>().level != 0)
                    {
                        MaxLvlUnit = item;
                        MaxLevel = item.GetComponent<Unit>().level;
                    }
                }
                if (MaxLvlUnit == null) break;
                MaxLvlUnit.GetComponent<Unit>().forSort = false;
                MaxLvlUnit.transform.SetSiblingIndex(count3);
                count3++;
            }
            while (true)
            {
                int MaxLevel = 0;
                GameObject MaxLvlUnit = null;
                foreach (GameObject item in PlayerData.myCollection)
                {
                    if (item.GetComponent<Unit>().level > MaxLevel &&
                        item.GetComponent<Unit>().forSort == true)
                    {
                        MaxLvlUnit = item;
                        MaxLevel = item.GetComponent<Unit>().level;
                    }
                }

                if (MaxLvlUnit == null) break;
                MaxLvlUnit.GetComponent<Unit>().forSort = false;
                MaxLvlUnit.transform.SetSiblingIndex(count3);
                count3++;
            }
            foreach (GameObject item in PlayerData.myCollection)
            {
                item.GetComponent<Unit>().forSort = true;
            }
        }

        // Сортировка по Слиянию!
        else if (PlayerData.sorting == 4)
        {
            int count4 = 0;
            while (true)
            {
                int MaxGrade = 0;
                GameObject MaxGradeUnit = null;
                foreach (GameObject item in PlayerData.myCollection)
                {
                    if (item.GetComponent<Unit>().grade > MaxGrade &&
                        item.GetComponent<Unit>().forSort == true &&
                        item.GetComponent<Unit>().level != 0)
                    {
                        MaxGradeUnit = item;
                        MaxGrade = item.GetComponent<Unit>().grade;
                    }
                }
                if (MaxGradeUnit == null) break;
                MaxGradeUnit.GetComponent<Unit>().forSort = false;
                MaxGradeUnit.transform.SetSiblingIndex(count4);
                count4++;
            }
            while (true)
            {
                int MaxGrade = 0;
                GameObject MaxGradeUnit = null;
                foreach (GameObject item in PlayerData.myCollection)
                {
                    if (item.GetComponent<Unit>().grade > MaxGrade &&
                        item.GetComponent<Unit>().forSort == true)
                    {
                        MaxGradeUnit = item;
                        MaxGrade = item.GetComponent<Unit>().grade;
                    }
                }

                if (MaxGradeUnit == null) break;
                MaxGradeUnit.GetComponent<Unit>().forSort = false;
                MaxGradeUnit.transform.SetSiblingIndex(count4);
                count4++;
            }
            foreach (GameObject item in PlayerData.myCollection)
            {
                item.GetComponent<Unit>().forSort = true;
            }
        }
        for (int i = 0; i < PlayerData.myCollection.Length - PlayerData.troops.Count; i++)
        {
            tempList.Add(content.transform.GetChild(i).gameObject);
        }
    }
    private IEnumerator ShowStart()
    {
        if (PlayerData.troops.Count != 0)
        {
            for (int i = 0; i < PlayerData.troops.Count; i++)
            {
                PlayerData.myCollection[PlayerData.troops[i]].SetActive(true);
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
        for (int i = 0; i < PlayerData.myCollection.Length; i++)
        {
            PlayerData.myCollection[i].SetActive(false);
            PlayerData.myCollection[i].GetComponent<Unit>().forSort = true;
        }
    }
    public void ShowInstantly()
    {
        if (showUnits != null) StopCoroutine(showUnits);
        for (int i = 0; i < PlayerData.myCollection.Length; i++)
            PlayerData.myCollection[i].SetActive(true);
    }
}