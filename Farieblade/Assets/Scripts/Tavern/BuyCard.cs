using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class BuyCard : MonoBehaviour
{
    [SerializeField] private Image currency;
    [SerializeField] private Image card2;
    [SerializeField] private Sprite gold;
    [SerializeField] private Sprite af;
    [SerializeField] private TextMeshProUGUI textDescription;
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private TextMeshProUGUI textPrice;
    [SerializeField] private string[] warning;
    [SerializeField] private Sprite[] cardSprite;
    [SerializeField] private GameObject _deckTable;
    [SerializeField] private Deck _deck;
    [SerializeField] private AudioClip clip;
    [SerializeField] private StringArray[] text;
    [SerializeField] private StringArray[] textNameType;
    [SerializeField] private Button buy;
    [SerializeField] private int[] textPriceType;
    [SerializeField] private int[] textCurrency;
    [SerializeField] private int CurrentIndex;
    public void SetPanel(int index)
    {
        CurrentIndex = index;
        textName.text = textNameType[index].intArray[PlayerData.language];
        card2.sprite = cardSprite[index];
        textDescription.text = text[index].intArray[PlayerData.language];
        textPrice.text = textPriceType[index].ToString();
        if (index == 0 || index == 1) currency.sprite = af;
        else currency.sprite = gold;
        if (Inventory.InventoryPlayer[textCurrency[index]] >= textPriceType[index]) buy.interactable = true;
        else buy.interactable = false;
    }
    public void SetCards() => StartCoroutine(SetCardsAsync());
    public IEnumerator SetCardsAsync()
    {
        string json = "";
        Dictionary<string, string> form = new Dictionary<string, string>
        {
            { "index", $"{CurrentIndex}" },
            { "action", "buy" }
        };
        var cor = StartCoroutine(Http.HttpQurey(answer => json = answer, "tavernBuy", form));
        yield return cor;
        if (json == "-1")
        {
            PlayerData.warning.SetActive(true);
            PlayerData.textWarning.text = warning[PlayerData.language];
        }
        else
        {
            if (CurrentIndex == 0)
                Inventory.InventoryPlayer[24] -= 450;
            else if(CurrentIndex == 1)
                Inventory.InventoryPlayer[24] -= 300;
            else
                Inventory.InventoryPlayer[23] -= 4000;
            PlayerData.ChangeGoldAF();
            _deckTable.SetActive(true);
            _deck.BuyCard(json);
        }
    }
}