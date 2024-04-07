using UnityEngine;

public class ReturnMap : MonoBehaviour
{
    [SerializeField] private GameObject circle12;
    [SerializeField] private GameObject circle13;
    [SerializeField] private GameObject circle15;

    public void CheckTraning()
    {
        if (PlayerData.traning == 11)
        {
            PlayerData.traning = 12;
            circle12.SetActive(true);
        }
        else if (PlayerData.traning == 12)
        {
            circle12.SetActive(false);
            PlayerData.traning = 13;
            circle13.SetActive(true);
        }
        else if (PlayerData.traning == 13)
        {
            circle13.SetActive(false);
            PlayerData.traning = 14;
            Camera.main.GetComponent<PanelPropertisMainMenu>().coach.GetComponent<Coach>().CoachStartGet();
        }
        else if (PlayerData.traning == 15)
        {
            circle15.SetActive(true);
            PlayerData.traning = 16;
        }
        else if (PlayerData.traning == 16)
        {
            circle15.SetActive(false);
        }
    }
}
