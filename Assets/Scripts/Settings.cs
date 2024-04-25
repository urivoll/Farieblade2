using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private Scrollbar barSound;
    [SerializeField] private Scrollbar barMusic;
    [SerializeField] private Scrollbar barVoice;
    [SerializeField] private Scrollbar barAmb;

    public void SetLanguage(int num)
    {
        if(PlayerData.language != num)
        {
            PlayerPrefs.SetInt("language", num);
            PlayerData.language = PlayerPrefs.GetInt("language");
            PlayerData.afterFight = 1;
            SceneManager.LoadScene(0);
        }
    }
    private void OnEnable()
    {
        barSound.value = Sound.soundLevel;
        barMusic.value = Sound.musicLevel;
        barVoice.value = Sound.voiceLevel;
        barAmb.value = Sound.ambLevel;
    }
    public void SetSound()
    {
        Sound.soundLevel = barSound.value;
        PlayerPrefs.SetFloat("sound", Sound.soundLevel);
        Sound.soundMixer.audioMixer.SetFloat("soundLevel", Mathf.Lerp(-40, 0, barSound.value));
    }
    public void SetMusic()
    {
        Sound.musicLevel = barMusic.value;
        PlayerPrefs.SetFloat("music", Sound.musicLevel);
        Sound.musicMixer.audioMixer.SetFloat("musicLevel", Mathf.Lerp(-40, 0, barMusic.value));
    }
    public void SetAmb()
    {
        Sound.ambLevel = barAmb.value;
        PlayerPrefs.SetFloat("amb", Sound.ambLevel);
        Sound.ambMixer.audioMixer.SetFloat("ambLevel", Mathf.Lerp(-40, 0, barAmb.value));
    }
    public void SetVoice()
    {
        Sound.voiceLevel = barVoice.value;
        PlayerPrefs.SetFloat("voice", Sound.voiceLevel);
        Sound.voiceMixer.audioMixer.SetFloat("voiceLevel", Mathf.Lerp(-40, 0, barVoice.value));
    }
}
