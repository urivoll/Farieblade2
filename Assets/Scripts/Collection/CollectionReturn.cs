using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionReturn : MonoBehaviour
{
    [SerializeField] private GameObject[] circle;
    private int[] tempTroop = new int[6];
    [SerializeField] private Sorting sort;
    public void Enable()
    {
        for (int i = 0; i < tempTroop.Length; i++)
        {
            tempTroop[i] = PlayerData.troop[i];
        }
    }
    public void Disable()
    {
        if(gameObject != null)
        StartCoroutine(CheckCollectionChangedAsync());
    }
    public IEnumerator CheckCollectionChangedAsync()
    {
        PlayerData.troops.Clear();
        for (int i = 0; i < PlayerData.troop.Length; i++)
        {
            if (circle[i].GetComponent<UIDropHandler>().newObject != null)
            {
                PlayerData.troops.Add(circle[i].GetComponent<UIDropHandler>().newObject.GetComponent<Unit>().ID);
                PlayerData.troop[i] = circle[i].GetComponent<UIDropHandler>().newObject.GetComponent<Unit>().ID;
            }
            else
                PlayerData.troop[i] = -666;
        }
        int count2 = 0;
        int countChange = 0;
        for (int i = 0; i < tempTroop.Length; i++)
        {
            if (tempTroop[i] == PlayerData.troop[i])
                count2++;
            else countChange++;
        }
        //Проверка на наличие изменений
        if (count2 != 6)
        {
            string json = "";
            Dictionary<string, string> form = new Dictionary<string, string> 
            { 
                { "id0", $"{PlayerData.troop[0]}" },
                { "id1", $"{PlayerData.troop[1]}" },
                { "id2", $"{PlayerData.troop[2]}" },
                { "id3", $"{PlayerData.troop[3]}" },
                { "id4", $"{PlayerData.troop[4]}" },
                { "id5", $"{PlayerData.troop[5]}" }
            };
            var cor = Http.HttpQurey(answer => json = answer, "collection/collectionReturn", form);
            yield return cor;
        }
        sort.Again();
        sort.tempList.Clear();
    }
}
public class Troops
{
    public int id1_1;
    public int id2_1;
    public int id1_2;
    public int id2_2;
    public int id1_3;
    public int id2_3;
}
