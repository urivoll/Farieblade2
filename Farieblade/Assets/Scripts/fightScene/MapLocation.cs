using UnityEngine;

public class MapLocation : MonoBehaviour
{
    public static int place;
    [SerializeField] private GameObject[] maps;
    [SerializeField] private GameObject _mushrooms;
    [SerializeField] private GameObject _forest;
    [SerializeField] private GameObject _undead;
    [SerializeField] private GameObject _grass;
    [SerializeField] private GameObject _hell;
    [SerializeField] private GameObject _snow;
    [SerializeField] private GameObject _desert;
    [SerializeField] private GameObject _dungeon;
    public void SetBackground()
    {
        if(Energy.mode == 0)
        {
            if (Campany.battleField == 0) maps[0].SetActive(true);
            else if (Campany.battleField == 1) maps[1].SetActive(true);
            else if (Campany.battleField == 2) maps[2].SetActive(true);
            else if (Campany.battleField == 3) maps[3].SetActive(true);
            else if (Campany.battleField == 4) maps[4].SetActive(true);
            else if (Campany.battleField == 5) maps[5].SetActive(true);
            else if (Campany.battleField == 6) maps[6].SetActive(true);
            else if (Campany.battleField == 7) maps[7].SetActive(true);
        }
        else
        {
            maps[place].SetActive(true);
        }
    }
}
