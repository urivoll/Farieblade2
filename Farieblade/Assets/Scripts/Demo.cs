using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Demo : MonoBehaviour
{
    [SerializeField] private DateTimeServer dailyReward;
    [SerializeField] private TextMeshProUGUI textGold;
    [SerializeField] private TextMeshProUGUI textAF;
    [SerializeField] private TextMeshProUGUI dayInRow;
    [SerializeField] private GameObject panel;
    public void ShowDailyPanel(int gold, int af, int dayInRowIn)
    {
        panel.SetActive(true);
        if (PlayerData.language == 0) dayInRow.text = $"You logged in {dayInRowIn} days in a row";
        else if (PlayerData.language == 1) dayInRow.text = $"Вы заходили {dayInRowIn} дней в подряд";

        textGold.text = Convert.ToString(gold);
        textAF.text = Convert.ToString(af);
        Inventory.InventoryPlayer[23] += gold;
        Inventory.InventoryPlayer[24] += af;
        PlayerData.ChangeGoldAF();
    }
}
