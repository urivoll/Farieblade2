using UnityEngine;
using Random = UnityEngine.Random;
public class BattleVoice : MonoBehaviour
{
    [SerializeField] private AudioArray[] voiceHit;
    [SerializeField] private AudioArray[] voiceStrike;
    public static AudioSource voiceSource;
    private void Start() => voiceSource = GetComponent<AudioSource>();
    public void HitVoices(int index, bool alive)
    {
        if (alive) voiceSource.PlayOneShot(voiceHit[index].audioArray[Random.Range(0, 3)]);
        else voiceSource.PlayOneShot(voiceHit[index].audioArray[3]);
    }
    public void StrikeVoices(int index) 
    {
        if (voiceStrike[index].audioArray.Length > 0) 
            voiceSource.PlayOneShot(voiceStrike[index].audioArray[Random.Range(0, voiceStrike[index].audioArray.Length)]);
    }
}
