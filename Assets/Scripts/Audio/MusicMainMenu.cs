using UnityEngine;

public class MusicMainMenu : MonoBehaviour
{
    public static int musicIndex = 0;
    public AudioClip[] playlist;  // Массив аудиоклипов для плейлиста
    public AudioSource audioSource;  // Компонент AudioSource для воспроизведения аудио
    private bool play;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Start2();
    }
    public void Start2()
    {
        InvokeRepeating("PlayNextTrack", 0f, 3f);
        if (musicIndex == 0)
        {
            PlayTrack(musicIndex);
            musicIndex = 1;
        }
        else if (musicIndex == 1)
        {
            PlayTrack(musicIndex);
            musicIndex = 0;
        }
        play = true;
    }
    public void Stop()
    {
        play = false;
        audioSource.Stop();
    }
    private void PlayNextTrack()
    {
        if (play)
        {
            if (!audioSource.isPlaying)
            {
                NextTrack();
            }
        }
    }

    void PlayTrack(int trackIndex)
    {
        if (trackIndex >= 0 && trackIndex < playlist.Length)
        {
            audioSource.clip = playlist[trackIndex];
            audioSource.Play();
        }
    }

    void NextTrack()
    {
        musicIndex = (musicIndex + 1) % playlist.Length;
        PlayTrack(musicIndex);
    }
}
