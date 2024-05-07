using UnityEngine;

public class AfterFightPlacePicker : MonoBehaviour
{
    public static int afterFight = 1;
    public static GameObject currentPlace;

    [SerializeField] private GameObject tavern;
    [SerializeField] private GameObject city;
    [SerializeField] private GameObject collections;
    [SerializeField] private GameObject adventure;
    [SerializeField] private GameObject multiplayer;
    public void AfterFightScene()
    {
        if (afterFight == 1)
        {
            currentPlace = city;
            city.SetActive(true);
        }
        else if (afterFight == 2)
        {
            currentPlace = adventure;
            adventure.SetActive(true);
        }
        else if (afterFight == 3)
        {
            currentPlace = collections;
            collections.SetActive(true);
        }
        else if (afterFight == 4)
        {
            currentPlace = city;
            tavern.SetActive(true);
        }
        else if (afterFight == 23)
        {
            currentPlace = multiplayer;
            multiplayer.GetComponent<Multiplayer>().after = true;
            multiplayer.SetActive(true);
            multiplayer.GetComponent<Multiplayer>().AfterFight();
        }
    }
    public void SetLayer()
    {
        currentPlace.SetActive(true);
    }
    public void SetLayer3()
    {
        currentPlace.SetActive(false);
    }
    public void SetLayer2(GameObject obj)
    {
        currentPlace = obj;
    }
}
