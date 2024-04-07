using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static int[] InventoryPlayer = new int[26];
    public GameObject[] InventorySlot;
    [SerializeField] private GameObject Description;
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private TextMeshProUGUI textDesc;
    [SerializeField] private Image Portrait;
    public static int CurrentItem;
    [SerializeField] private GameObject useButton;
    public void SetDescription(int index)
    {
        CurrentItem = index;
        Description.SetActive(true);
        textName.text = InventorySlot[index].GetComponent<InvenoryShowItem>().Name[PlayerData.language];
        textDesc.text = InventorySlot[index].GetComponent<InvenoryShowItem>().Description[PlayerData.language];
        Portrait.sprite = InventorySlot[index].GetComponent<Image>().sprite;
    }
    private void OnEnable()
    {
        SetAmountItems();
    }
    private void OnDisable()
    {
        for (int i = 0; i < InventoryPlayer.Length; i++)
        {
            InventorySlot[i].SetActive(false);
        }
    }
    public void SetAmountItems()
    {
        for (int i = 0; i < InventoryPlayer.Length; i++)
        {
            if (InventoryPlayer[i] != 0)
            {
                InventorySlot[i].SetActive(true);
                InventorySlot[i].GetComponent<InvenoryShowItem>().amount.text = InventoryPlayer[i].ToString();
            }
            else InventorySlot[i].SetActive(false);
        }
    }
}
