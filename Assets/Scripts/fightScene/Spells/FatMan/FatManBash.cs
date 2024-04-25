using System.Collections.Generic;
using UnityEngine;
public class FatManBash : AbstractSpell
{
    [SerializeField] private GameObject Effect2;
    private GameObject BashEffect;
    void Start()
    {
        if (transform.parent.gameObject.name == "Debuffs")
        {
            BashEffect = Instantiate(Effect2, parentUnit.PathBulletTarget.position, Quaternion.identity);
            if (PlayerData.language == 0)
            {
                nameText = "Stun";
                SType = "Debuff";
                description = $"This character is stunned. The character skips the next turn.";
            }
            else
            {
                nameText = "Оглушение";
                SType = "Проклятье";
                description = $"Существо оглушено, оно пропускает следующий ход.";
            }
        }
    }
    public override void EndDebuff()
    {
        Destroy(BashEffect);
    }
    public override void PeriodicMethod(Dictionary<string, int> inpData)
    {
        parentUnit.Animation.TryGetAnimation("hit");
        parentUnit.CharacterState.ChangeParalize(true);
        Destroy(BashEffect);
        Destroy(gameObject);
    }
}
