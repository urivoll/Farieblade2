using System.Collections;
using UnityEngine;

public class MusicFight : MonoBehaviour
{
    public static AudioSource StartMusic;
    [SerializeField] private AudioClip[] fight;
    public void Start()
    {
        StartMusic = GetComponent<AudioSource>();
    }
    public void Start2()
    {
        StartMusic.clip = fight[Random.Range(0, fight.Length)];
        StartMusic.Play();
    }
}
