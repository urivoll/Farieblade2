using Spine.Unity;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/CharacterAnimation")]
public class AnimationAssets : ScriptableObject
{
    [SerializeField] private AnimationReferenceAsset _hit;
    [SerializeField] private AnimationReferenceAsset _death;
    [SerializeField] private AnimationReferenceAsset _spell;
    [SerializeField] private AnimationReferenceAsset _spell2;
    [SerializeField] private AnimationReferenceAsset _spell3;
    [SerializeField] private AnimationReferenceAsset _mode;
    [SerializeField] private AnimationReferenceAsset _passive;
}
