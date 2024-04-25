using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AuraLightWizard : Aura
{
    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioClip clip2;
    [SerializeField] private GameObject effect;
    public override IEnumerator GetAura(Dictionary<string, int> inpData)
    {
        parentUnit.Animation.TryGetAnimation("passive");
        BattleSound.sound.PlayOneShot(clip);
        yield return new WaitForSeconds(0.3f);
        BattleSound.sound.PlayOneShot(clip2);
        for (int i = 0; i < inpData["count"]; i++)
        {
            Instantiate(effect, _characterPlacement.CirclesMap[inpData["side"], inpData[$"place{i}"]].ChildCharacter.PathBulletTarget.position, Quaternion.identity);
            _characterPlacement.CirclesMap[inpData["side"], inpData[$"place{i}"]].ChildCharacter.Energy.SetEnergy(inpData[$"energy{i}"]);
        }
        yield return new WaitForSeconds(0.3f);
        Turns.finishEndEvent = true;
    }
}
