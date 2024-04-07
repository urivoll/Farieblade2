using System;
using UnityEngine;

public class CollectionTraning : MonoBehaviour
{
    
    private void Start()
    {
        if (PlayerData.traning == 3)
        {
            PlayerData.traning = 4;
            try
            {
                Camera.main.GetComponent<PanelPropertisMainMenu>().coach.CoachStartGet();
            }
            catch (Exception ex)
            {
                print(ex.ToString());
            }
        }
    }
}
