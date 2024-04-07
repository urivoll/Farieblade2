using Newtonsoft.Json;
using System.Collections;
using UnityEngine;

public class StoreCardsChange : MonoBehaviour
{
    [SerializeField] private GameObject[] Cards;
    public static int CardsChangeTimeDay;
    public static int CardsChangeTimeSec;
    public IEnumerator ChangeCards()
    {
        string json = "";
        var cor = StartCoroutine(Http.HttpQurey(answer => json = answer, "store"));
        yield return cor;
        StoreCardChangeJson obj = JsonConvert.DeserializeObject<StoreCardChangeJson>(json);
        if (obj.week == 1 || obj.week == 4)
        {
            Cards[0].SetActive(true);
            Cards[1].SetActive(false);
            Cards[2].SetActive(false);
        }
        else if (obj.week == 2)
        {
            Cards[0].SetActive(false);
            Cards[1].SetActive(true);
            Cards[2].SetActive(false);
        }
        else if (obj.week == 3 || obj.week == 5)
        {
            Cards[0].SetActive(false);
            Cards[1].SetActive(false);
            Cards[2].SetActive(true);
        }
        CardsChangeTimeDay = 5 - obj.week;
        CardsChangeTimeSec = obj.time;
    }
}
public class StoreCardChangeJson
{
    public int week;
    public int time;
}