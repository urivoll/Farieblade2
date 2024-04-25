using System;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnIsCollection : MonoBehaviour
{
    [SerializeField] private Image chanceBar;
    [SerializeField] private TextMeshProUGUI textChance;
    [SerializeField] private TextMeshProUGUI powerNeed;
    [SerializeField] private Animator animator;
    public static int PowerNeedAnIs = 0;
    public static int PowerAnIs = 0;
    public static int ChanceAnIs = 0;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Image BG;
    [SerializeField] private Image BG2;
    void Awake()
    {
        SetPower();
    }
    public void SetPower()
    {
        powerNeed.text = Convert.ToString(PowerAnIs) + " / " + Convert.ToString(PowerNeedAnIs);
        animator.SetTrigger("on");
        ChanceAnIs = PowerAnIs * 100 / PowerNeedAnIs;
        if (ChanceAnIs > 100) ChanceAnIs = 100;
        textChance.text = Convert.ToString(ChanceAnIs) + "%";
        if (ChanceAnIs < 40) textChance.color = new(255, 0, 0);
        else if (ChanceAnIs > 39 && ChanceAnIs < 70) textChance.color = new(255, 255, 0);
        else if (ChanceAnIs > 69) textChance.color = new(0, 255, 0);
        chanceBar.fillAmount = Convert.ToSingle(ChanceAnIs) / 100;
    }
    public void SetPower2()
    {
        powerNeed.text = Convert.ToString(PowerAnIs) + " / " + Convert.ToString(PowerNeedAnIs);
        textChance.text = "0%";
        chanceBar.fillAmount = 0;
    }
    private void OnEnable()
    {
        if(AnIs.CurrentLoc < 9)
        {
            BG.sprite = sprites[0];
            BG2.sprite = sprites[0];
        }
        else if (AnIs.CurrentLoc > 8 && AnIs.CurrentLoc < 18)
        {
            BG.sprite = sprites[1];
            BG2.sprite = sprites[1];
        }
        else
        {
            BG.sprite = sprites[2];
            BG2.sprite = sprites[2];
        }
    }
}
