using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public static AudioClip click1;
    public static AudioClip click2;
    public static AudioClip endTurn;
    public AudioClip click1Prefub;
    public AudioClip click2Prefub;
    public AudioClip endTurnPrefub;
    void Start()
    {
        click1 = click1Prefub;
        click2 = click2Prefub;
        endTurn = endTurnPrefub;
    }
}
