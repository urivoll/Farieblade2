using Spine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnIsMap : MonoBehaviour
{
    public static GameObject[] circle;
    [SerializeField] public GameObject[] circlePrefub;
    public GameObject[] Locations;
    [SerializeField] private GameObject[] anIsClosed;
    private void Awake()
    {
        circle = circlePrefub;
    }
    private void OnEnable()
    {
        SetCirclesOnMap();
        InvokeRepeating("SetCirclesOnMap", 1f, 1f);
    }
    public void SetCirclesOnMap()
    {
        for (int i = 0; i < Campany.anIsTimeNeed.Length; i++)
        {
            if (Campany.anIsTimeNeed[i] != 0 && Campany.anIsTimeNeed[i] != -1)
            {
                circle[i].SetActive(true);
                AnIsLoc anisloc = Locations[Campany.anIsLocation[i]].GetComponent<AnIsLoc>();
                if (DateTimeServer.serverTime >= Campany.anIsTimeNeed[i])
                {
                    Campany.anIsTimeNeed[i] = -1;
                    StickerManager.ChangeStick(Campany.anIsLocation[i] + 27, 1);
                    anisloc.onAnIsNow.SetActive(false);
                    anisloc.onAnIsDone.SetActive(true);
                }
                else
                {
                    int TimeDifference = Campany.anIsTimeNeed[i] - DateTimeServer.serverTime;
                    anisloc.onAnIsNow.SetActive(true);
                    anisloc.onAnIsDone.SetActive(false);
                    if (TimeDifference < 3600 && TimeDifference > 60)
                        anisloc.textTime.text = Convert.ToString(Convert.ToInt32(TimeDifference / 60) + "m");
                    else if (TimeDifference <= 60)
                        anisloc.textTime.text = Convert.ToString(TimeDifference + "s");
                    else
                        anisloc.textTime.text = Convert.ToString(Convert.ToInt32(TimeDifference / 60 / 60) + "h");
                
                }
            }
        }
    }
    public void LocationsClose()
    {
        for (int i = 0; i < anIsClosed.Length; i++)
        {
            if(Campany.anIsProgress >= i) anIsClosed[i].SetActive(false);
        }
    }
}
