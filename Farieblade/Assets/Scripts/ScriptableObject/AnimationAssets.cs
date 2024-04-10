using Spine.Unity;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/CharacterAnimation")]
public class AnimationAssets : ScriptableObject
{
    [SerializeField] private AnimationReferenceAsset _attack;
    [SerializeField] private AnimationReferenceAsset _idle;
    [SerializeField] private AnimationReferenceAsset _hit;
    [SerializeField] private AnimationReferenceAsset _death;
    [SerializeField] private AnimationReferenceAsset _spell;
    [SerializeField] private AnimationReferenceAsset _spell2;
    [SerializeField] private AnimationReferenceAsset _mode;
    [SerializeField] private AnimationReferenceAsset _passive;

    public AnimationReferenceAsset Attack => _attack;
    public AnimationReferenceAsset Idle => _idle;
    public AnimationReferenceAsset Hit => _hit;
    public AnimationReferenceAsset Death => _death;
    public AnimationReferenceAsset Spell => _spell;
    public AnimationReferenceAsset Spell2 => _spell2;
    public AnimationReferenceAsset Mode => _mode;
    public AnimationReferenceAsset Passive => _passive;
}
