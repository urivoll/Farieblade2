using System;
using TMPro;
using UnityEngine;

public class MyCollection : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textAmount;
    [SerializeField] private TextMeshProUGUI textPower;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Animator animatorStr;
    [SerializeField] private Animator animatorAmount;
    [SerializeField] private Animator panel;
    [SerializeField] private CollectionReturn _return;
    void Awake()
    {
        textPower.text = Convert.ToString(PlayerData.totalPower);
        SetAmount(true);
    }
    public void SetPower()
    {
        textPower.text = Convert.ToString(PlayerData.totalPower);
        animatorStr.SetTrigger("on");
    }
    public void SetAmount(bool start = false)
    {
        textAmount.text = Convert.ToString(PlayerData.troopAmount) + " / 4" ;
        if (PlayerData.troopAmount == 4 || PlayerData.troopAmount == 3) textAmount.color = new(0, 255, 0);
        else if (PlayerData.troopAmount == 2) textAmount.color = new(255, 255, 0);
        else if (PlayerData.troopAmount == 1 || PlayerData.troopAmount == 0) textAmount.color = new(255, 0, 0);
        if (start == false)
            animatorAmount.SetTrigger("on");
    }
    private void OnEnable()
    {
        panel.SetTrigger("off");
        _return.Enable();
    }
    private void OnDisable()
    {
        if(panel != null)
        panel.SetTrigger("on");
        if (_return != null) _return.Disable();
    }
}
