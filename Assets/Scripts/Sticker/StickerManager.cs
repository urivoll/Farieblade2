using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class StickerManager : MonoBehaviour
{
    public static GameObject[] stickersStat;
    public static TextMeshProUGUI[] textCountStickStat;
    [SerializeField] private GameObject[] stickers;
    [SerializeField] private TextMeshProUGUI[] textCountStick;
    public static int[] things = new int[54];
    public static Action<int, int> ChangeSticker;
    public static Action<int> StartSticker;
    private void Start()
    {
        stickersStat = stickers;
        textCountStickStat = textCountStick;
    }
    public void SetStikers()
    {
        PlayerPrefs.SetInt("StickerId0", PlayerData.freeDf);
        for (int i = 0; i < stickers.Length; i++)
        {
            things[i] = PlayerPrefs.GetInt($"StickerId{i}", 0);
            StartSticker?.Invoke(i);
            Count(i);
        }
    }
    public static void ChangeStick(int i, int num)
    {
        things[i] = num;
        ChangeSticker?.Invoke(i, num);
        PlayerPrefs.SetInt($"StickerId{i}", things[i]);
        Count(i);
    }
    private static void Count(int i)
    {
        if (i == 0)
            textCountStickStat[i].text = things[i].ToString();
        if (things[i] == 1)
        {
            stickersStat[i].SetActive(true);
        }
        else
        {
            stickersStat[i].SetActive(false);
        }
    }
}
