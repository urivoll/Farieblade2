using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartTraning : MonoBehaviour
{
    [SerializeField] private GameObject book;
    [SerializeField] private GameObject map;
    [SerializeField] private GameObject GoldAF;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject duelistGolem;
    [SerializeField] private bool click = false;
    [SerializeField] private GameObject button;
    [SerializeField] private Image blackImg;
    [SerializeField] private Animator black;
    [SerializeField] private TextMeshProUGUI textButton;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip voiceJump;
    [SerializeField] private AudioClip breath;
    [SerializeField] private AudioClip breath2;
    [SerializeField] private AudioClip swish;
    [SerializeField] private AudioClip hit;
    [SerializeField] private AudioClip jump2;
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip fall;
    [SerializeField] private AudioClip end;
    [SerializeField] private AudioSource audioSource2;
    [SerializeField] private AudioSource audioSource3;
    [SerializeField] private GameObject black2;
    [SerializeField] private Animator slowmo;
    [SerializeField] private GameObject slowmo2;
    public IEnumerator StartGame()
    {
        book.SetActive(true);
        yield return new WaitForSeconds(6f);
    }
    public IEnumerator StartGame2()
    {
        yield return new WaitForSeconds(0.1f);
        slowmo2.SetActive(true);
        audioSource.PlayOneShot(breath);
        yield return new WaitForSeconds(2.35f);
        black.SetTrigger("deep");
        yield return new WaitForSeconds(0.25f);
        duelistGolem.transform.Find("Model").GetComponent<UnitAnimation>().SetCaracterState("spell");
        audioSource.PlayOneShot(breath2);
        yield return new WaitForSeconds(2.75f);
        black.SetTrigger("deep");
        yield return new WaitForSeconds(0.25f);
        duelistGolem.transform.Find("Model").GetComponent<UnitAnimation>().SetCaracterState("death");
        button.SetActive(true);
        while (click == false) yield return null;
        button.SetActive(false);
        click = false;
        duelistGolem.transform.Find("Model").GetComponent<UnitAnimation>().SetCaracterState("hit");
        audioSource.PlayOneShot(jump);
        slowmo.SetTrigger("on");
        audioSource2.Stop();

        animator.SetTrigger("middle");
        yield return new WaitForSeconds(2f);
        audioSource2.Play();
        
        slowmo.SetTrigger("off");
        audioSource.PlayOneShot(jump2);
        duelistGolem.transform.Find("Model").GetComponent<UnitAnimation>().SetCaracterState("idle");
        yield return new WaitForSeconds(3f);
        if (PlayerData.language == 0) textButton.text = "Neutralize";
        else if (PlayerData.language == 1) textButton.text = "Нейтрализовать";

        button.SetActive(true);
        while (click == false) yield return null;
        button.SetActive(false);
        duelistGolem.transform.Find("Model").GetComponent<UnitAnimation>().SetCaracterState("attack");
        yield return new WaitForSeconds(1.2f);
        audioSource.PlayOneShot(swish);
        yield return new WaitForSeconds(0.1f);
        audioSource.PlayOneShot(hit);
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("end");
        audioSource.PlayOneShot(fall);
        yield return new WaitForSeconds(1f);
        audioSource.PlayOneShot(end);
        audioSource2.Stop();
        audioSource3.Stop();
        black2.SetActive(true);
        PlayerData.traning = 1;
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(0);
    }
    public void Click1()
    {
        click = true;
    }
}
