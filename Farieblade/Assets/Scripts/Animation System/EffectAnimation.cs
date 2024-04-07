using Spine;
using Spine.Unity;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EffectAnimation : AbstractAnimation
{
    [SerializeField] private AnimationReferenceAsset _death;
    public string localState = null;

    private void Start()
    {
        SetAnimation(_death, false, 1f, "bullet");
    }
}