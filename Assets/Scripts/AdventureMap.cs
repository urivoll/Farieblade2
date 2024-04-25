using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureMap : MonoBehaviour
{
    [SerializeField] private GameObject[] map;

    public void SetMap()
    {
        if(Campany.campanyProgress < 11)
        {
            map[0].SetActive(true);
        }
        else if (Campany.campanyProgress > 10 && Campany.campanyProgress < 21)
        {
            map[1].SetActive(true);
        }
        else if (Campany.campanyProgress > 20 && Campany.campanyProgress < 31)
        {
            map[2].SetActive(true);
        }
        else if (Campany.campanyProgress > 30 && Campany.campanyProgress < 41)
        {
            map[3].SetActive(true);
        }
        else if (Campany.campanyProgress > 40 && Campany.campanyProgress < 51)
        {
            map[4].SetActive(true);
        }
        else if (Campany.campanyProgress > 50 && Campany.campanyProgress < 61)
        {
            map[5].SetActive(true);
        }
        else if (Campany.campanyProgress > 60 && Campany.campanyProgress < 71)
        {
            map[6].SetActive(true);
        }
    }
}
