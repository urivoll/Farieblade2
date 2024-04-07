using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlideDamage : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshProUGUI textImputDamage;
    [SerializeField] private Image type;
    [SerializeField] private Sprite water;
    [SerializeField] private Sprite fire;
    [SerializeField] private Sprite physical;
    [SerializeField] private Sprite air;
    [SerializeField] private Sprite earth;
    [SerializeField] private Sprite lvlup;
    [SerializeField] private Sprite death;
    [SerializeField] private Sprite light2;
    private void Start()
    {
        Invoke("DestroyObj", 1.3f);
        if (PlayerData.traning == 0) Destroy(gameObject);
    }
    public void UpdateSlideDamage(float inpDamage, int type2 = -1)
    {
        if (inpDamage == -1)
        {
            if (PlayerData.language == 0) textImputDamage.text = "Miss";
            else if (PlayerData.language == 1) textImputDamage.text = "Промах";
            textImputDamage.color = new(255, 255, 255);
            type.sprite = lvlup;
        }
        else if (type2 == 3)
        {
            textImputDamage.color = new(50, 150, 50);
            textImputDamage.text = Convert.ToString(Convert.ToInt32(inpDamage));
            type.sprite = earth;
        }
        else if (type2 == 4)
        {
            textImputDamage.color = new(255, 255, 255);
            textImputDamage.text = Convert.ToString(Convert.ToInt32(inpDamage));
            type.sprite = physical;
        }
        else if (type2 == 0)
        {
            textImputDamage.color = new(100, 255, 255);
            textImputDamage.text = Convert.ToString(Convert.ToInt32(inpDamage));
            type.sprite = air;
        }
        else if (type2 == 5)
        {
            textImputDamage.color = new(255, 120, 0);
            textImputDamage.text = Convert.ToString(Convert.ToInt32(inpDamage));
            type.sprite = fire;
        }
        else if (type2 == 6)
        {
            textImputDamage.color = new(0, 145, 255);
            textImputDamage.text = Convert.ToString(Convert.ToInt32(inpDamage));
            type.sprite = light2;
        }
        else if (type2 == 1)
        {
            textImputDamage.color = new(0, 210, 255);
            textImputDamage.text = Convert.ToString(Convert.ToInt32(inpDamage));
            type.sprite = water;
        }
        else if (type2 == 2)
        {
            textImputDamage.color = new(0, 197, 139);
            textImputDamage.text = Convert.ToString(Convert.ToInt32(inpDamage));
            type.sprite = death;
        }
        else if (type2 == -1)
        {
            textImputDamage.color = new(0, 255, 0);
            textImputDamage.text = "+" + Convert.ToString(Convert.ToInt32(inpDamage));
            type.sprite = lvlup;
        }
        animator.SetTrigger("Alarm");

    }
    private void DestroyObj() => Destroy(gameObject);
}
