using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PanelPropertiesInfo : MonoBehaviour
{
    [SerializeField] private Button buttonLvlup;
    [SerializeField] private TextMeshProUGUI textButtonLvlup;
    [SerializeField] private Button buttonExp;
    [SerializeField] private TextMeshProUGUI textButtonExp;
    [SerializeField] private GameObject Effect;
    [SerializeField] private GameObject EffectExp;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private GameObject closedLvlup;
    public void Start2() => SetAmount();
    public void SetLvlUp() => StartCoroutine(SetLvlUpAsync());
    private IEnumerator SetLvlUpAsync()
    {
        Unit unit = PanelProperties.CurrentObj.GetComponent<Unit>();
        string json = "";
        Dictionary<string, string> form = new Dictionary<string, string> { { "unit", $"{unit.ID}" } };
        var cor = Http.HttpQurey(answer => json = answer, "eliksirUp", form);
        yield return cor;
        Lvlup obj = JsonConvert.DeserializeObject<Lvlup>(json);

        Effect.SetActive(true);
        unit.exp = 0;
        unit.level += 1;
        Destroy(GetComponent<PanelProperties>()._avatarObject);
        unit.SetValues();
        unit.transform.Find("Card").GetComponent<CardVeiw>().SetCardValues();
        unit.ShowUnitProperties();
        Inventory.InventoryPlayer[4] -= 1;
        SetAmount();
        textButtonLvlup.text = Inventory.InventoryPlayer[4].ToString();

        if (obj.TaskIdD4 != -666)
        {
            TaskManager.Daily[4] = obj.TaskIdD4;
            PlayerData.floatNote.SetActive(true);
            PlayerData.floatNote.GetComponent<TaskFloatNote>().SetValues(4);
        }
        if (obj.taskProgressDaily != -666)
        {
            TaskManager.taskProgressDaily = obj.taskProgressDaily;
        }
        if (obj.taskProgressWeekly != -666)
        {
            TaskManager.taskProgressWeekly = obj.taskProgressWeekly;
        }
        if (obj.TaskIdW1 != -666)
        {
            TaskManager.Weekly[1] = obj.TaskIdW1;
        }
    }
    public void SetAmount()
    {
        if (Inventory.InventoryPlayer[4] == 0 || PanelProperties.CurrentObj.GetComponent<Unit>().level >= 60 ||
            PanelProperties.CurrentObj.GetComponent<Unit>().level == 0 || PanelProperties.CurrentObj.GetComponent<Unit>().You == false)
            closedLvlup.SetActive(true);
        else closedLvlup.SetActive(false);
        textButtonLvlup.text = Inventory.InventoryPlayer[4].ToString();
    }
}
public class Lvlup
{
    public int taskProgressDaily;
    public int taskProgressWeekly;
    public int TaskIdW1;
    public int TaskIdD4;
}
