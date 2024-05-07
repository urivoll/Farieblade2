using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickerTab : MonoBehaviour
{
    [SerializeField] private int[] things;
    [SerializeField] private int howMany = 0;
    private void Start()
    {
            StickerManager.ChangeSticker += SetStick;
            StickerManager.StartSticker += SetStick2;
    }
    private void OnDestroy()
    {
        StickerManager.ChangeSticker -= SetStick;
        StickerManager.StartSticker -= SetStick2;
    }
    private void SetStick(int index, int num)
    {
        bool have = false;
        for (int i = 0; i < things.Length; i++)
        {
            if(things[i] == index)
            {
                if(num == 1)
                    howMany++;
                else
                    howMany--;
                have = true;
            }
        }
        Show(have);
    }
    private void SetStick2(int index)
    {
        for (int i = 0; i < things.Length; i++)
        {
            if(things[i] == index)
            {
                if(StickerManager.things[index] == 1) howMany++;
            }
        }
        Show(true);
    }

    private void Show(bool have)
    {
        if (have == true)
        {
            if (howMany == 0) transform.Find("sticker").gameObject.SetActive(false);
            else transform.Find("sticker").gameObject.SetActive(true);
        }
    }
}
