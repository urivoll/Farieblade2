using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskFloatNote : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textText;
    [SerializeField] private TextMeshProUGUI textNow;
    [SerializeField] private TextMeshProUGUI textNeed;
    [SerializeField] private int[] Need;
    [SerializeField] private StringArray[] text;
    [SerializeField] private Image bar;
    [SerializeField] private Image img;
    [SerializeField] private Sprite[] sprites;
    private Coroutine cor = null;
    public void SetValues(int i)
    {
        if (cor != null) StopCoroutine(cor);
        cor = StartCoroutine(Disable());
        img.sprite = sprites[i];
        textText.text = text[i].intArray[PlayerData.language];
        textNow.GetComponent<Animator>().SetTrigger("on");
        textNow.text = TaskManager.Daily[i].ToString();
        textNeed.text = Need[i].ToString();
        bar.fillAmount = Convert.ToSingle(TaskManager.Daily[i]) * 100f / Need[i] / 100f;
    }
    private IEnumerator Disable()
    {
        yield return new WaitForSeconds(2f);
        GetComponent<Animator>().SetTrigger("off");
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(0.2f);
        cor = null;
        gameObject.SetActive(false);
    }
}
