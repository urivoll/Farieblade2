using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUseItems : MonoBehaviour
{
    [SerializeField] private GameObject deckTable;
    [SerializeField] private Deck deck;
    public IEnumerator SetCardsAsync()
    {
        string json = "";
        Dictionary<string, string> form = new Dictionary<string, string>
        {
            { "rang", $"{Inventory.CurrentItem}" },
            { "action", "use" }
        };
        var cor = StartCoroutine(Http.HttpQurey(answer => json = answer, "tavernBuy", form));
        yield return cor;
        if (json != "-1")
        {
            Inventory.InventoryPlayer[Inventory.CurrentItem] -= 1;
            GetComponent<Inventory>().SetAmountItems();
            deck.BuyCard(json);
        }
    }
    public void SetUseItem()
    {
        if (Inventory.CurrentItem > 9 && Inventory.CurrentItem < 14)
        {
            deckTable.SetActive(true);
            int count = 0;
            for (int i = 10; i < 14; i++)
            {
                count++;
                if (Inventory.CurrentItem == i)
                {
                    StartCoroutine(SetCardsAsync());
                    break;
                }
            }
        }
    }
}
