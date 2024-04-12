using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlchemyItem : MonoBehaviour
{
    public int Need;
    public int id;
    [SerializeField] private TextMeshProUGUI textNeed;
    private void Awake()
    {
        id = GetComponent<InvenoryShowItem>().id;
    }
    public void SetAmount()
    {
        textNeed.text = Need.ToString();
        if (GetComponent<InvenoryShowItem>().amount != null)
        {
            if (Inventory.InventoryPlayer[id] < Need) GetComponent<Image>().color = new Color(120, 120, 120);
            else GetComponent<Image>().color = new Color(255, 255, 255);
            GetComponent<InvenoryShowItem>().amount.text = Inventory.InventoryPlayer[id].ToString();
        }
    }
}
