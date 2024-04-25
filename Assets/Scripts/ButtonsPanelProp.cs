using UnityEngine;
using UnityEngine.UI;

public class ButtonsPanelProp : MonoBehaviour
{
    [SerializeField] private GameObject[] Buttons;
    public GameObject CurrentButton;
    [SerializeField] private GameObject Avatar;
    public void SetButton(int i)
    {
        CurrentButton.GetComponent<Animator>().SetTrigger("off");
        CurrentButton = Buttons[i];
        if (CurrentButton == Buttons[1]) Avatar.SetActive(false);
        else Avatar.SetActive(true);
    }
    private void OnEnable()
    {
        CurrentButton = Buttons[0];
        CurrentButton.GetComponent<Button>().interactable = false;
        CurrentButton.GetComponent<Animator>().SetTrigger("on");
    }
}
