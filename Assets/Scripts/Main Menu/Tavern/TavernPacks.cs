using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TavernPacks : MonoBehaviour
{
    [SerializeField] private GameObject _idCard1;
    [SerializeField] private GameObject _idCard2;
    [SerializeField] private GameObject _idCard3;
    [SerializeField] private int _idRang;
    [SerializeField] private int _price;
    [SerializeField] private string _money;
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private GameObject buyCard2;
    [SerializeField] private BuyCard buyCard;
    public void SetCards()
    {
        string name = textName.text;
        Sprite card = gameObject.GetComponent<Image>().sprite;
        buyCard2.SetActive(true);
        //buyCard.SetPanel(_idCard1, _idCard2, _idCard3, _idRang, _price, _money, name, card);
    }
}
