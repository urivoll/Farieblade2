using UnityEngine;
public class BattleSound : MonoBehaviour
{
    public static AudioClip[] soundClip;
    public static AudioClip[] weaponClip;
    public static AudioClip[] swishClip;
    [SerializeField] private AudioClip[] soundPrefub;
    [SerializeField] private AudioClip[] weaponPrefub;
    [SerializeField] private AudioClip[] swishPrefub;
    public static AudioSource sound;
    private void Start()
    {
        soundClip = soundPrefub;
        weaponClip = weaponPrefub;
        swishClip = swishPrefub;
        sound = GetComponent<AudioSource>();
    }
}
