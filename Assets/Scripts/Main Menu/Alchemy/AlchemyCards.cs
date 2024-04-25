using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AlchemyCards : MonoBehaviour
{
    [SerializeField] private GameObject[] Slot1;
    [SerializeField] private GameObject[] slot2;
    [SerializeField] private GameObject[] slot3;
    [SerializeField] private GameObject[] slot4;
    [SerializeField] private int[] slotNeed1;
    [SerializeField] private int[] slotNeed2;
    [SerializeField] private int[] slotNeed3;
    [SerializeField] private int[] slotNeed4;
    [SerializeField] private AlchemyTable alchemy;
    [SerializeField] private Sprite[] Sprite;
    [SerializeField] private int[] items;
    [SerializeField] private int type;
    private void OnEnable()
    {
        SetCraft();
    }
    public void SetCraft()
    {
        for (int i = 0; i < 4; i++)
        {
            alchemy.tempListIndex[i] = 0;
        }
        alchemy.tempInventory = new int[Inventory.InventoryPlayer.Length];
        Array.Copy(Inventory.InventoryPlayer, alchemy.tempInventory, Inventory.InventoryPlayer.Length);
        alchemy.type = type;
        alchemy.Count = 0;
        //alchemy.tempInventory.AddRange(Inventory.InventoryPlayer);
        alchemy.ItemsCreate = items;
        alchemy.CurrentItem1 = Slot1;
        alchemy.CurrentItem2 = slot2;
        alchemy.CurrentItem3 = slot3;
        alchemy.CurrentItem4 = slot4;
        alchemy.CurrentItemNeed1 = slotNeed1;
        alchemy.CurrentItemNeed2 = slotNeed2;
        alchemy.CurrentItemNeed3 = slotNeed3;
        alchemy.CurrentItemNeed4 = slotNeed4;
        alchemy.SetCardsBefore();
        alchemy.SetSprites(Sprite);

    }
}
