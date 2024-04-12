using System;
using System.Collections.Generic;
using UnityEngine;

public class TwinsConnect2 : AbstractSpell
{
    [HideInInspector] public bool done = false;
    [HideInInspector] public GameObject child;
    void Start()
    {
        if (transform.parent.gameObject.name == "Debuffs")
        {
            if (PlayerData.language == 0)
            {
                nameText = "Spiritual connection";
                SType = "Buff";
                description = $"When Twins ally takes damage, it is shared equally with the Gemini. Gemini takes water damage.";
            }
            else
            {
                nameText = "Духовная связь";
                SType = "Усиливающее заклинание";
                description = $"Когда союзник Близнецов получает урон, он распределяется поровну с Близнецами. Близнецы получают урон водой.";
            }
        }
    }
    public override void EndDebuff()
    {
        if(done == false && child != null)
        {
            ChangeChild();
        }
    }
    public void ChangeChild()
    {
        child.GetComponent<TwinsConnect>().done = true;
        Destroy(child);
    }
}
